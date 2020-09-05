using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnessEntities;
using BuisnessObject.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DynamicCalculationBasedOnRule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculateController : Controller
    {
        private readonly ICalculation _calculation;

        public CalculateController(ICalculation calculation)
        {
            _calculation = calculation;
            
        }

        [HttpPost]
        [Route("Total")]
        public double CalculateTotal(List<Items> lstItems)
        {
            return _calculation.CalulateTotal(lstItems);
            
        }

        
    }
}
