using BuisnessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessObject.Interface
{
    public interface ICalculationDAO
    {
        List<ItemMasterDataWithUnitprice> GetMasterDataWithUnitprice();

        List<Promotion> GetPromotions(); 
    }
}
