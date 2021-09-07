using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.Json;
using System.IO;
using EntityLayer.Concrete;

namespace DataAccessLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.Concrete.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected FileStream fs;
        protected StreamWriter wr;

        protected override void Seed(DataAccessLayer.Concrete.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            string jsonContext = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"/../../Roles.json");
            fs = File.Open("seed_" + DateTime.Now.ToString("ddMMyy-hhmm") + ".log", FileMode.OpenOrCreate);
            wr = new StreamWriter(fs, Encoding.UTF8);
            RolePermissionDTO data = JsonSerializer.Deserialize<RolePermissionDTO>(jsonContext);
            wr.WriteLine("name:" + data.Roles[0].Name);
            wr.Flush();
            UpdatePermissions(data.Permissions, context);
            UpdateRoles(data.Roles, context);

            InsertDummyData(context);

            wr.Close();
            fs.Close();


        }

        private void UpdatePermissions(List<Permission> jsonPermissions, DataAccessLayer.Concrete.Context context)
        {
            List<Permission> pl = context.Permissions.ToList();
            if (pl.Count > 0)
            {
                Stack<Permission> dontDestroy = new Stack<Permission>();
                for (int i = 0; i < pl.Count; i++)
                {
                    Permission onDb = pl[i];
                    //remove all permissions already existing
                    for (int j = 0; j < jsonPermissions.Count; j++)
                    {
                        Permission newPerm = jsonPermissions[j];
                        if (newPerm.PermissionName.Equals(onDb.PermissionName))
                        {
                            jsonPermissions.RemoveAt(j);
                            if (!dontDestroy.Contains(onDb)) dontDestroy.Push(onDb);
                            j--;
                        }
                    }
                }
                //Remove permissions that no more exist
                for (int i = 0; i < pl.Count; i++)
                {
                    Permission onDb = pl[i];
                    if (dontDestroy.Contains(onDb)) continue;
                    context.Permissions.Remove(onDb);
                    context.SaveChanges();
                }
            }

            //Add new migrations
            foreach (Permission p in jsonPermissions)
            {
                try
                {
                    Permission pdb = context.Permissions.SingleOrDefault(perm => perm.PermissionName == p.PermissionName);
                    if (pdb == null)
                    {
                        wr.WriteLine("Permission added: " + p.PermissionName);
                        wr.Flush();
                        context.Permissions.Add(p);
                        context.SaveChanges();
                    }

                }
                catch (InvalidOperationException e)
                {
                    continue;
                }
            }

        }

        private void UpdateRoles(List<RoleDTO> jsonRoles, DataAccessLayer.Concrete.Context context)
        {
            List<Role> rl = context.Roles.ToList();
            List<Role> rolesToBeAdded = new List<Role>();
            if (rl.Count > 0)
            {
                Stack<Role> toUpdate = new Stack<Role>();
                for (int i = 0; i < rl.Count; i++)
                {
                    Role onDb = rl[i];
                    bool exist = false;
                    for (int j = 0; j < jsonRoles.Count; j++)
                    {
                        RoleDTO onJson = jsonRoles[j];
                        if (onDb.RoleName.Equals(onJson.RoleName))
                        {
                            onJson.Id = onDb.Id;
                            if (!exist)
                            {
                                rl.RemoveAt(i);
                                i--;
                                exist = true;
                            }
                            else
                            {
                                toUpdate.Pop();
                            }
                            toUpdate.Push(onJson.ToRole(context));
                            jsonRoles.RemoveAt(j);

                            j--;
                        }
                    }
                }

                //Delete old roles
                foreach (Role r in rl)
                {
                    context.Roles.Remove(r);
                    context.SaveChanges();
                }

                while (toUpdate.Count > 0)
                {
                    rolesToBeAdded.Add(toUpdate.Pop());
                }
            }

            foreach (RoleDTO r in jsonRoles)
            {
                Role roletoSave = r.ToRole(context);

                rolesToBeAdded.Add(roletoSave);
            }

            foreach (Role r in rolesToBeAdded)
            {
                wr.WriteLine("This role has " + r.RolesPermissions.Count + " permissions.");
                wr.Flush();
                context.Roles.AddOrUpdate(r);
                context.SaveChanges();
                wr.WriteLine("This role has id: " + r.Id);
                wr.Flush();

            }

        }

        private void InsertDummyData(DataAccessLayer.Concrete.Context context)
        {
            // These will Inserted to Database when update-database:
            List<Stock> stocksToBeInserted = new List<Stock>();

            wr.WriteLine("--- Dummy Data Insertion ---");
            wr.Flush();

            foreach (var c in new Category[] {
            new Category
            {
                CategoryID = 1,
                CategoryName = "Üst Giyim",
                CategoryStatus = true,
            },
            new Category
            {
                CategoryID = 2,
                CategoryName = "Alt Giyim",
                CategoryStatus = true,
            },
            new Category
            {
                CategoryID = 3,
                CategoryName = "İç Giyim",
                CategoryStatus = true,
            },
            new Category
            {
                CategoryID = 4,
                CategoryName = "Çocuk ve Bebek",
                CategoryStatus = true,
            },
            new Category
            {
                CategoryID = 5,
                CategoryName = "Aksesuar",
                CategoryStatus = true,
            },
            })
            {
                wr.WriteLine("Category Inserted [" + c.CategoryID + "|" + c.CategoryName + "|" + c.CategoryStatus + "].");
                wr.Flush();

                context.Categories.AddOrUpdate(c);
                context.SaveChanges();
            }

            foreach (var p in new Product[] {
            new Product {
                ProductID = 1,
                Name = "Klasik Yaka Pamuklu Uzun Kollu Gömlek",
                CategoryID = 1, // TODO: Category Insert
                ProductStatus = true,
                ProductGender = "Erkek",
                ProductPrice = 47.99,
                ProductDescription =
                "Klasik Yaka Pamuklu Uzun Kollu Gömlek" +
                "\nMevcut Renkler: Açık Mavi" +
                "\nMevcut Boyutlar: S - M - L - XL",
            },
            new Product {
                ProductID = 2,
                Name = "Bisiklet Yaka Dokulu Kumaş Regular Fit Triko Kazak",
                CategoryID = 1,
                ProductStatus = true,
                ProductGender = "Erkek",
                ProductPrice = 63.99,
                ProductDescription =
                 "Bisiklet Yaka Dokulu Kumaş Regular Fit Triko Kazak" +
                 "\nMevcut Renkler: Gül" +
                 "\nMevcut Boyutlar: XS - S - M - XL"
            },
            new Product {
                ProductID = 3,
                Name = "Pamuklu Slim Fit Islemeli Kapüsonlu Kazak",
                CategoryID = 1,
                ProductStatus = true,
                ProductGender = "Erkek",
                ProductPrice = 47.99,
                ProductDescription =
                 "Pamuklu Slim Fit Islemeli Kapüsonlu Kazak - Siyah Çizgili" +
                 "\nMevcut Renkler: Siyah, Bordo" +
                 "\nMevcut Boyutlar: M - L - XL - XXL"
            },
            new Product {
                ProductID = 4,
                Name = "Boxer Seti Pamuklu",
                CategoryID = 3,
                ProductStatus = true,
                ProductGender = "Erkek",
                ProductPrice = 48.99,
                ProductDescription =
                 "Boxer Seti Pamuklu" +
                 "\nMevcut Renkler: Mavi, Beyaz, Gri, Siyah" +
                 "\nMevcut Boyutlar: XS - S  - M - L " + 
                 "\n~\"Sahibinden Temizzz Kullanilmis\""
            },
            new Product {
                ProductID = 5,
                Name = "Kısa Kollu Yazılı Baskılı Tişört",
                CategoryID = 1,
                ProductStatus = true,
                ProductGender = "Kadın",
                ProductPrice = 32.49,
                ProductDescription =
                 "Kısa Kollu Yazılı Baskılı Tişört" +
                 "\nMevcut Renkler: Ekru" +
                 "\nMevcut Boyutlar: S - M - L - XL "
            },
            new Product {
                ProductID = 6,
                Name = "Kısa Kollu Gömlek Kareli Pamuklu",
                CategoryID = 1,
                ProductStatus = true,
                ProductGender = "Kadın",
                ProductPrice = 69.99,
                ProductDescription =
                 "Kısa Kollu Gömlek Kareli Pamuklu" +
                 "\nMevcut Renkler: Turkuaz Kareli" +
                 "\nMevcut Boyutlar: XS - S - M - L - XL "
            },
            new Product {
                ProductID = 7,
                Name = "Çiçekli Etek Mini",
                CategoryID = 2,
                ProductStatus = true,
                ProductGender = "Kadın",
                ProductPrice = 79.99,
                ProductDescription =
                 "Çiçekli Etek Mini" +
                 "\nMevcut Renkler: Mavi" +
                 "\nMevcut Boyutlar: XS - S - M - L - XL "
            },
            new Product {
                ProductID = 8,
                Name = "Batman Şort",
                CategoryID = 4,
                ProductStatus = true,
                ProductGender = "Erkek",
                ProductPrice = 39.99,
                ProductDescription =
                 "Çocuk - Batman Şort Lisanslı Baskılı Belden Bağlamalı Pamuklu" +
                 "\nMevcut Renkler: Siyah" +
                 "\nMevcut Boyutlar: XS - S - M"
            },
            new Product {
                ProductID = 9,
                Name = "Tül Detaylı Etek",
                CategoryID = 4,
                ProductStatus = true,
                ProductGender = "Kadın",
                ProductPrice = 39.99,
                ProductDescription =
                 "Çocuk - Tül Detaylı Etek" +
                 "\nMevcut Renkler: Pembe" +
                 "\nMevcut Boyutlar: XS - S - M"
            },
            new Product {
                ProductID = 10,
                Name = "Deri Detaylı Çanta Nakışlı",
                CategoryID = 5,
                ProductStatus = true,
                ProductGender = "Kadın",
                ProductPrice = 29.99,
                ProductDescription =
                 "Deri Detaylı Çanta Nakışlı" +
                 "\nMevcut Renkler: Bej"
            },
            new Product {
                ProductID = 11,
                Name = "Desenli Tulum Pamuklu",
                CategoryID = 4,
                ProductStatus = true,
                ProductGender = "Kadın",
                ProductPrice = 34.99,
                ProductDescription =
                 "Çocuk - Desenli Tulum Pamuklu" +
                 "\nMevcut Renkler: Mavi" +
                 "\nMevcut Boyutlar: XS - S - M"
            },
            new Product {
                ProductID = 12,
                Name = "Spor Ayakkabı",
                CategoryID = 5,
                ProductStatus = true,
                ProductGender = "Unisex",
                ProductPrice = 105.99,
                ProductDescription =
                 "Spor Ayakkabı" +
                 "\nMevcut Renkler: Beyaz" +
                 "\nMevcut Boyutlar: XS - S - M - L - XL"
            },
            new Product {
                ProductID = 13,
                Name = "Düğme Detaylı Jean Pantolon",
                CategoryID = 2,
                ProductStatus = true,
                ProductGender = "Erkek",
                ProductPrice = 139.99,
                ProductDescription =
                 "Düğme Detaylı Jean Pantolon" +
                 "\nMevcut Renkler: İndigo" +
                 "\nMevcut Boyutlar: S - M - L"
            },
            }) 
            {
                wr.WriteLine("Product Inserted [" + p.ProductID + "|" + p.Name + " in " + p.CategoryID + "].");
                wr.Flush();

                context.Products.AddOrUpdate(p);
                context.SaveChanges();
            }

            #region [ User Insetion ]

            wr.WriteLine("Inserting Dummy Users.");
            wr.Flush();

            User
                user1 = new User(1, "123", "admin1", "hka@gmail.com"),
                user2 = new User(2, "123", "user1", "hku@gmail.com");
            List<Role> rl = context.Roles.ToList();
            user1.Role = rl.ElementAt(0);
            user2.Role = rl.ElementAt(1);

            context.Users.AddOrUpdate(user1);
            context.Users.AddOrUpdate(user2);
            context.SaveChanges();

            #endregion

            foreach (var i in new Image[] {
            new Image
            {
                Id = 1,
                ProductId = 1,
                Title = "urun_img_template_1_1",
                Alternate = "Urun 1",
                Description = "Urun 1",
                Url = "/Content/img/urun1-1.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 2,
                ProductId = 1,
                Title = "urun_img_template_1_2",
                Alternate = "Urun 1",
                Description = "Urun 1",
                Url = "/Content/img/urun1-2.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 3,
                ProductId = 1,
                Title = "urun_img_template_1_3",
                Alternate = "Urun 1",
                Description = "Urun 1",
                Url = "/Content/img/urun1-3.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 4,
                ProductId = 2,
                Title = "urun_img_template_2_1",
                Alternate = "Urun 2",
                Description = "Urun 2",
                Url = "/Content/img/urun2-1.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 5,
                ProductId = 2,
                Title = "urun_img_template_2_2",
                Alternate = "Urun 2",
                Description = "Urun 2",
                Url = "/Content/img/urun2-2.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 6,
                ProductId = 2,
                Title = "urun_img_template_2_3",
                Alternate = "Urun 2",
                Description = "Urun 2",
                Url = "/Content/img/urun2-3.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 7,
                ProductId = 3,
                Title = "urun_img_template_3_1",
                Alternate = "Urun 3",
                Description = "Urun 3",
                Url = "/Content/img/urun3-1.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 8,
                ProductId = 3,
                Title = "urun_img_template_3_2",
                Alternate = "Urun 3",
                Description = "Urun 3",
                Url = "/Content/img/urun3-2.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 9,
                ProductId = 3,
                Title = "urun_img_template_3_3",
                Alternate = "Urun 3",
                Description = "Urun 3",
                Url = "/Content/img/urun3-3.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 10,
                ProductId = 4,
                Title = "urun_img_template_4_1",
                Alternate = "Urun 4",
                Description = "Urun 4",
                Url = "/Content/img/urun4-1.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 11,
                ProductId = 4,
                Title = "urun_img_template_4_2",
                Alternate = "Urun 4",
                Description = "Urun 4",
                Url = "/Content/img/urun4-2.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 12,
                ProductId = 5,
                Title = "urun_img_template_5_1",
                Alternate = "Urun 5",
                Description = "Urun 5",
                Url = "/Content/img/urun5-1.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 13,
                ProductId = 5,
                Title = "urun_img_template_5_2",
                Alternate = "Urun 5",
                Description = "Urun 5",
                Url = "/Content/img/urun5-2.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 15,
                ProductId = 6,
                Title = "urun_img_template_6_1",
                Alternate = "Urun 6",
                Description = "Urun 6",
                Url = "/Content/img/urun6-1.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 16,
                ProductId = 6,
                Title = "urun_img_template_6_2",
                Alternate = "Urun 6",
                Description = "Urun 6",
                Url = "/Content/img/urun6-2.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 17,
                ProductId = 6,
                Title = "urun_img_template_6_3",
                Alternate = "Urun 6",
                Description = "Urun 6",
                Url = "/Content/img/urun6-3.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 18,
                ProductId = 7,
                Title = "urun_img_template_7_1",
                Alternate = "Urun 7",
                Description = "Urun 7",
                Url = "/Content/img/urun7-1.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 19,
                ProductId = 7,
                Title = "urun_img_template_7_2",
                Alternate = "Urun 7",
                Description = "Urun 7",
                Url = "/Content/img/urun7-2.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 20,
                ProductId = 7,
                Title = "urun_img_template_7_3",
                Alternate = "Urun 7",
                Description = "Urun 7",
                Url = "/Content/img/urun7-3.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 21,
                ProductId = 8,
                Title = "urun_img_template_8_1",
                Alternate = "Urun 8",
                Description = "Urun 8",
                Url = "/Content/img/urun8-1.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 22,
                ProductId = 8,
                Title = "urun_img_template_8_2",
                Alternate = "Urun 8",
                Description = "Urun 8",
                Url = "/Content/img/urun8-2.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 23,
                ProductId = 8,
                Title = "urun_img_template_8_3",
                Alternate = "Urun 8",
                Description = "Urun 8",
                Url = "/Content/img/urun8-3.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 24,
                ProductId = 9,
                Title = "urun_img_template_9_1",
                Alternate = "Urun 9",
                Description = "Urun 9",
                Url = "/Content/img/urun9-1.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 25,
                ProductId = 9,
                Title = "urun_img_template_9_2",
                Alternate = "Urun 9",
                Description = "Urun 9",
                Url = "/Content/img/urun9-2.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 26,
                ProductId = 9,
                Title = "urun_img_template_9_3",
                Alternate = "Urun 9",
                Description = "Urun 9",
                Url = "/Content/img/urun9-3.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 27,
                ProductId = 10,
                Title = "urun_img_template_10_1",
                Alternate = "Urun 10",
                Description = "Urun 10",
                Url = "/Content/img/urun10-1.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 28,
                ProductId = 10,
                Title = "urun_img_template_10_2",
                Alternate = "Urun 10",
                Description = "Urun 10",
                Url = "/Content/img/urun10-2.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 29,
                ProductId = 10,
                Title = "urun_img_template_10_3",
                Alternate = "Urun 10",
                Description = "Urun 10",
                Url = "/Content/img/urun10-3.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 30,
                ProductId = 11,
                Title = "urun_img_template_11_1",
                Alternate = "Urun 11",
                Description = "Urun 11",
                Url = "/Content/img/urun11-1.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 31,
                ProductId = 11,
                Title = "urun_img_template_11_2",
                Alternate = "Urun 11",
                Description = "Urun 11",
                Url = "/Content/img/urun11-2.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 32,
                ProductId = 11,
                Title = "urun_img_template_11_3",
                Alternate = "Urun 11",
                Description = "Urun 11",
                Url = "/Content/img/urun11-3.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 33,
                ProductId = 12,
                Title = "urun_img_template_12_1",
                Alternate = "Urun 12",
                Description = "Urun 12",
                Url = "/Content/img/urun12-1.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 34,
                ProductId = 12,
                Title = "urun_img_template_12_2",
                Alternate = "Urun 12",
                Description = "Urun 12",
                Url = "/Content/img/urun12-2.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 35,
                ProductId = 12,
                Title = "urun_img_template_12_3",
                Alternate = "Urun 12",
                Description = "Urun 12",
                Url = "/Content/img/urun12-3.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 36,
                ProductId = 13,
                Title = "urun_img_template_13_1",
                Alternate = "Urun 13",
                Description = "Urun 13",
                Url = "/Content/img/urun13-1.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 37,
                ProductId = 13,
                Title = "urun_img_template_13_2",
                Alternate = "Urun 13",
                Description = "Urun 13",
                Url = "/Content/img/urun13-2.jpg",
                UploadedById = 1,
            },
            new Image
            {
                Id = 38,
                ProductId = 13,
                Title = "urun_img_template_13_3",
                Alternate = "Urun 13",
                Description = "Urun 13",
                Url = "/Content/img/urun13-3.jpg",
                UploadedById = 1,
            },
            })
            {
                wr.WriteLine("Image Inserted [" + i.Id + "|" + i.Title + " of " + i.ProductId + "].");
                wr.Flush();

                context.Images.AddOrUpdate(i);
                context.SaveChanges();
            }

            foreach (var s in new Stock[] {
            new Stock {
                StockID = 1,
                ProductId = 1,
                StockQuantity = 20,
            },
            new Stock {
                StockID = 2,
                ProductId = 2,
                StockQuantity = 2,
            },
            new Stock {
                StockID = 3,
                ProductId = 3,
                StockQuantity = 200,
            },
            new Stock {
                StockID = 4,
                ProductId = 4,
                StockQuantity = 33,
            }
            })
            {
                wr.WriteLine("Stock Inserted [" + s.StockID + "|" + s.StockQuantity + " of " + s.ProductId + "].");
                wr.Flush();

                context.Stocks.AddOrUpdate(s);
                context.SaveChanges();
            }


        }

    }
}
