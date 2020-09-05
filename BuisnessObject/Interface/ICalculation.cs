using BuisnessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessObject.Interface
{
    public interface ICalculation
    {
        double CalulateTotal(List<Items> lstItems);
    }
}
