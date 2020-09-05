using BuisnessEntities;
using BuisnessObject.Interface;
using DataAccessObject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BuisnessObject
{
    public class Calculation : ICalculation
    {
        private readonly ICalculationDAO _calculationDAO;
        public Calculation(ICalculationDAO calculationDAO)
        {
            _calculationDAO = calculationDAO;
        }

        public double CalulateTotal(List<Items> lstItems)
        {
            long total = 0;
            try
            {
                return total = CalculateTotal(lstItems);
            }
            catch (Exception ex)
            {
                //We can log this as per Solution design. Here just i am thrwing 
                throw ex;
            }

        }

        private long CalculateTotal(List<Items> lstItems)
        {
            long total = 0;         

            #region new type
           
            //Get promotion Value from DB/ Cache. It Can be in any format json / cxml. Kind of Rule Engine
            var Promotion = _calculationDAO.GetPromotions();

            //We are having two types of Promotion one individual and another conmbine. So filter based on Type
            var Combine = Promotion.Where(type => type.promotionType.Equals(PromotionType.Combine)).Select(name => name.SKUNAME).Distinct().ToList();

            var Individual = Promotion.Where(type => type.promotionType.Equals(PromotionType.Individual)).Select(name => name.SKUNAME).Distinct().ToList();

            //Get the individual promotion total based on internal Logic No HardCode
            if (Individual.Count() > 0)
            {
                List<Items> Individualitems = new List<Items>();

                foreach (var item in Individual)
                {
                    var itm = lstItems.Where(item1 => item1.SKUName.Equals(item, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if (itm != null)
                        Individualitems.Add(itm);
                }
                total = total + CalculateIndividualTotal(Individualitems, Promotion);
            }

            //Get the Combined promotion total based on internal Logic No HardCode
            if (Combine.Count() > 0)
            {
                total = total + CalCulateCombineTotal(lstItems, Promotion, Combine); 
            }

            
            #endregion
            return total;
        }

        private long CalculateIndividualTotal(List<Items> items, List<Promotion> promotions)
        {

            #region Individual Dynamic
            long total = 0;
            //Get Master Data And Promotion from Cache or DB
            var MasterData = _calculationDAO.GetMasterDataWithUnitprice();

            foreach (var item in items)
            {
                var PromotionData = promotions.Where(name => name.SKUNAME.Equals(item.SKUName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                ItemMasterDataWithUnitprice data = MasterData.Where(a => a.SKUNAME.Equals(item.SKUName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                int count = 0;
                if (!string.IsNullOrWhiteSpace(PromotionData.SKUNAME))
                {
                    while (item.Quantity >= PromotionData.PromotionQuantity)
                    {
                        count++;
                        item.Quantity = item.Quantity - PromotionData.PromotionQuantity;

                    }
                    if (count > 0)
                        total = total + (count * PromotionData.PromotionPrice);

                    if (item.Quantity > 0)
                        total = total + (item.Quantity * data.UnitPrice);
                }
                else
                    total = total + (item.Quantity * data.UnitPrice);


            }
            #endregion

            return total;

        }

        private long CalCulateCombineTotal(List<Items> items, List<Promotion> promotions, List<string> combinePromotion)
        {
            long total = 0;

            List<Items> Combineditems = new List<Items>();
            ItemMasterDataWithUnitprice Firstdata = new ItemMasterDataWithUnitprice();
            ItemMasterDataWithUnitprice Seconddata = new ItemMasterDataWithUnitprice();


            foreach (var item in combinePromotion)
            {
                List<string> SKUName = item.Split(',').ToList();
                var ActivePromotion = promotions.Where(a => a.SKUNAME.Equals(item, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                foreach (string names in SKUName)
                {
                    var matchitem = items.Where(item1 => item1.SKUName.Equals(names, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if (matchitem != null)
                        Combineditems.Add(matchitem);
                }

                var MasterData = _calculationDAO.GetMasterDataWithUnitprice();
                if (Combineditems.Count() > 0)
                {
                    var SKUCDetails = Combineditems.Where(itm => itm.SKUName.Equals("C", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(SKUCDetails.SKUName))
                        Firstdata = MasterData.Where(a => a.SKUNAME.Equals(SKUCDetails.SKUName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                    var SKUDDetails = items.Where(itm => itm.SKUName.Equals("D", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(SKUDDetails.SKUName))
                        Seconddata = MasterData.Where(a => a.SKUNAME.Equals(SKUDDetails.SKUName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();


                    if (ActivePromotion != null && !string.IsNullOrWhiteSpace(Firstdata.SKUNAME) && !string.IsNullOrWhiteSpace(Seconddata.SKUNAME))
                    {
                        if (SKUCDetails.Quantity > SKUDDetails.Quantity)
                        {
                            long diffrence = SKUCDetails.Quantity - SKUDDetails.Quantity;
                            total = total + (SKUDDetails.Quantity * ActivePromotion.PromotionPrice);

                            total = total + (diffrence * Firstdata.UnitPrice);
                        }
                        else if (SKUCDetails.Quantity < SKUDDetails.Quantity)
                        {
                            long diffrence = SKUDDetails.Quantity - SKUCDetails.Quantity;
                            total = total + (SKUCDetails.Quantity * ActivePromotion.PromotionPrice);

                            total = total + (diffrence * Seconddata.UnitPrice);
                        }
                        else
                        {
                            total = total + (SKUDDetails.Quantity * ActivePromotion.PromotionPrice);
                        }
                    }

                    else
                    {
                        if (!string.IsNullOrWhiteSpace(SKUCDetails.SKUName))
                            total = total + (SKUCDetails.Quantity * Firstdata.UnitPrice);

                        if (!string.IsNullOrWhiteSpace(SKUDDetails.SKUName))
                            total = total + (SKUDDetails.Quantity * Seconddata.UnitPrice);
                    }
                }

            }

            return total;
        }
    }
}
