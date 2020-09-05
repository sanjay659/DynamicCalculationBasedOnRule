using BuisnessEntities;
using DataAccessObject.Interface;
using System;
using System.Collections.Generic;

namespace DataAccessObject
{
    public class CalculationDAO : ICalculationDAO
    {
        public List<ItemMasterDataWithUnitprice> GetMasterDataWithUnitprice()
        {
            List<ItemMasterDataWithUnitprice> itemMasterDataWithUnitprices = new List<ItemMasterDataWithUnitprice>();
            ItemMasterDataWithUnitprice itemMasterDataWithUnitprice = new ItemMasterDataWithUnitprice();

            itemMasterDataWithUnitprice.SKUNAME = "A";
            itemMasterDataWithUnitprice.UnitPrice = 50;
            itemMasterDataWithUnitprices.Add(itemMasterDataWithUnitprice);


            itemMasterDataWithUnitprice = new ItemMasterDataWithUnitprice();
            itemMasterDataWithUnitprice.SKUNAME = "B";
            itemMasterDataWithUnitprice.UnitPrice = 30;
            itemMasterDataWithUnitprices.Add(itemMasterDataWithUnitprice);

            itemMasterDataWithUnitprice = new ItemMasterDataWithUnitprice();
            itemMasterDataWithUnitprice.SKUNAME = "C";
            itemMasterDataWithUnitprice.UnitPrice = 20;
            itemMasterDataWithUnitprices.Add(itemMasterDataWithUnitprice);

            itemMasterDataWithUnitprice = new ItemMasterDataWithUnitprice();
            itemMasterDataWithUnitprice.SKUNAME = "D";
            itemMasterDataWithUnitprice.UnitPrice = 15;
            itemMasterDataWithUnitprices.Add(itemMasterDataWithUnitprice);


            return itemMasterDataWithUnitprices;
        }

        public List<Promotion> GetPromotions()
        {
            List<Promotion> promotions = new List<Promotion>();
            Promotion promotion = new Promotion();

            promotion.SKUNAME = "A";
            promotion.PromotionQuantity = 3;
            promotion.PromotionPrice = 130;
            promotion.promotionType = PromotionType.Individual;

            promotions.Add(promotion);

            promotion = new Promotion();
            promotion.SKUNAME = "B";
            promotion.PromotionQuantity = 2;
            promotion.PromotionPrice = 45;
            promotion.promotionType = PromotionType.Individual;

            promotions.Add(promotion);

            promotion = new Promotion();
            promotion.SKUNAME = "C,D";
            promotion.PromotionQuantity = 1;
            promotion.PromotionPrice = 30;
            promotion.promotionType = PromotionType.Combine;

            promotions.Add(promotion);

            return promotions;
        }
    }
}
