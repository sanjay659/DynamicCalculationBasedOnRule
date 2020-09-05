using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessEntities
{
    public class Promotion
    {
        public string SKUNAME { get; set; }

        public long PromotionQuantity { get; set; }

        public long PromotionPrice { get; set; }

        public PromotionType promotionType { get; set; }

        public bool IsDeleted { get; set; }


    }
    public enum PromotionType
    {
        Individual = 1,
        Combine = 2
    }
}
