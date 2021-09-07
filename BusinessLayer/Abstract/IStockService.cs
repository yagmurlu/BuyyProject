﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IStockService
    {
        List<Stock> GetList();
        void StockAddBL(Stock stock);
        Stock GetById(int id);
        void StockDelete(Stock stock);
        void StockUpdate(Stock stock);
    }
}
