using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Alternate { get; set; }

        public string Description { get; set; }

        public string Url { get; set; } 

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int UploadedById { get; set; }

        public User UploadedBy { get; set; }
    }
}
