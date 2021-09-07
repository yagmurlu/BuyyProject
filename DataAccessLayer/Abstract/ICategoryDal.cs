using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    //CRUD işlemleri
    public interface ICategoryDal:IRepository<Category>
    {
        double MostExpensive(Category c);
    }
}
