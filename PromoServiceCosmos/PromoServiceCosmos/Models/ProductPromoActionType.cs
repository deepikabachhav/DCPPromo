using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PromoServiceCosmos.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProductPromoActionType
    {
        [EnumMember(Value = "PROMO_GWP")]
        PROMO_GWP,
        [EnumMember(Value = "PROMO_ORDER_AMOUNT")]
        PROMO_ORDER_AMOUNT,
        [EnumMember(Value = "PROMO_ORDER_PERCENT")]
        PROMO_ORDER_PERCENT,
        [EnumMember(Value = "PROMO_PROD_AMDISC")]
        PROMO_PROD_AMDISC,
        [EnumMember(Value = "PROMO_PROD_DISCOUNT")]
        PROMO_PROD_DISCOUNT,
        [EnumMember(Value = "PROMO_PROD_PRICE")]
        PROMO_PROD_PRICE,
        [EnumMember(Value = "PROMO_PROD_SPECIAL_PRICE")]
        PROMO_PROD_SPECIAL_PRICE
    }
}
