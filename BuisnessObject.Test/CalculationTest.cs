using BuisnessEntities;
using BuisnessObject;
using BuisnessObject.Test.MockedObject;
using DataAccessObject.Interface;
using NUnit.Framework;
using System.Collections.Generic;

namespace BuisnessObject.Test
{
    public class CalculationTest:MockedClass
    {
        private Calculation calculation;             

        public List<Items> Items;

        public CalculationTest()
        {
            calculation = new Calculation(MockCalculationDAO.Object);
        }

        [Test]
        public void Test1()
        {
            //Arrange
            var data  = ArrangeDataForTestOne();

            //Act
            var output = calculation.CalulateTotal(data);

            //Assert
            Assert.True(output==100);
        }

        [Test]
        public void Test2()
        {
            //Arrange
            var data = ArrangeDataForTestTwo();

            //Act
            var output = calculation.CalulateTotal(data);

            //Assert
            Assert.True(output == 370);
        }

        [Test]
        public void Test3()
        {
            //Arrange
            var data = ArrangeDataForTestThree();

            //Act
            var output = calculation.CalulateTotal(data);

            //Assert
            Assert.True(output == 280);
        }

        public void mockDAO()
        {
            var Promotion = new List<Promotion>();
            Promotion.Add(new Promotion() { SKUNAME = "A", PromotionQuantity = 3,promotionType= PromotionType.Individual,
            PromotionPrice= 130});
            Promotion.Add(new Promotion()
            {
                SKUNAME = "B",
                PromotionQuantity = 2,
                promotionType = PromotionType.Individual,
                PromotionPrice = 45
            });
            Promotion.Add(new Promotion()
            {
                SKUNAME = "C,D",
                PromotionQuantity = 1,
                promotionType = PromotionType.Combine,
                PromotionPrice = 30
            });
            MockCalculationDAO.Setup(x => x.GetPromotions()).Returns(Promotion);

            var itemwithPrice = new List<ItemMasterDataWithUnitprice>();
            itemwithPrice.Add(new ItemMasterDataWithUnitprice()
            {
               SKUNAME="A",
               UnitPrice=50
            });
            itemwithPrice.Add(new ItemMasterDataWithUnitprice()
            {
                SKUNAME = "B",
                UnitPrice = 30
            });
            itemwithPrice.Add(new ItemMasterDataWithUnitprice()
            {
                SKUNAME = "C",
                UnitPrice = 20
            });
            itemwithPrice.Add(new ItemMasterDataWithUnitprice()
            {
                SKUNAME = "D",
                UnitPrice = 15
            });
            MockCalculationDAO.Setup(x => x.GetMasterDataWithUnitprice()).Returns(itemwithPrice);


        }

        private List<Items> ArrangeDataForTestOne()
        {
            mockDAO();
            List<Items> data = new List<Items>();
            Items item = new Items();
            item.SKUName = "A";
            item.Quantity = 1;
            data.Add(item);

            item = new Items();
            item.SKUName = "B";
            item.Quantity = 1;
            data.Add(item);

            item = new Items();
            item.SKUName = "C";
            item.Quantity = 1;
            data.Add(item);
            return data;
        }

        private List<Items> ArrangeDataForTestTwo()
        {
            mockDAO();
            List<Items> data = new List<Items>();
            Items item = new Items();
            item.SKUName = "A";
            item.Quantity = 5;
            data.Add(item);

            item = new Items();
            item.SKUName = "B";
            item.Quantity = 5;
            data.Add(item);

            item = new Items();
            item.SKUName = "C";
            item.Quantity = 1;
            data.Add(item);
            return data;
        }

        private List<Items> ArrangeDataForTestThree()
        {
            mockDAO();
            List<Items> data = new List<Items>();
            Items item = new Items();
            item.SKUName = "A";
            item.Quantity = 3;
            data.Add(item);

            item = new Items();
            item.SKUName = "B";
            item.Quantity = 5;
            data.Add(item);

            item = new Items();
            item.SKUName = "C";
            item.Quantity = 1;
            data.Add(item);

            item = new Items();
            item.SKUName = "D";
            item.Quantity = 1;
            data.Add(item);
            return data;
        }


    }
}