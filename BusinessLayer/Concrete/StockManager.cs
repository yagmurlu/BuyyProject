using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class StockManager : IStockService
    {
        IStockDal _stockDal;

        public StockManager(IStockDal stockDal)
        {
            _stockDal = stockDal;
        }

        public Stock GetById(int id)
        {
            return _stockDal.Get(x => x.StockID == id);
        }

        public List<Stock> GetList()
        {
            return _stockDal.List();
        }

        public void StockAddBL(Stock stock)
        {
            _stockDal.Insert(stock);
        }

        public void StockDelete(Stock stock)
        {
            _stockDal.Delete(stock);
        }

        public void StockUpdate(Stock stock)
        {
            _stockDal.Update(stock);
        }
    }
}
