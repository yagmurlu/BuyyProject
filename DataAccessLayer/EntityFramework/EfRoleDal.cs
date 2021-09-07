using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repositories;

namespace DataAccessLayer.EntityFramework
{
    public class EfRoleDal : GenericRepository<Role>, IRoleDal
    {
    }
}
