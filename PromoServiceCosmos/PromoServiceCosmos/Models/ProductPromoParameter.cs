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
    public enum ProductPromoParameter
    {
        [EnumMember(Value = "PPIP_NEW_ACCT")]
        PPIP_NEW_ACCT,
        [EnumMember(Value = "PPIP_ORDER_TOTAL")]
        PPIP_ORDER_TOTAL,
        [EnumMember(Value = "PPIP_ORST_HIST_ORDER")]
        PPIP_ORST_HIST_ORDER,
        [EnumMember(Value = "PPIP_PARTY_CLASS")]
        PPIP_PARTY_CLASS,
        [EnumMember(Value = "PPIP_PARTY_GRP_MEM")]
        PPIP_PARTY_GRP_MEM,
        [EnumMember(Value = "PPIP_PARTY_ID")]
        PPIP_PARTY_ID,
        [EnumMember(Value = "PPIP_PRODUCT_AMOUNT")]
        PPIP_PRODUCT_AMOUNT,
        [EnumMember(Value = "PPIP_PRODUCT_QUANTITY")]
        PPIP_PRODUCT_QUANTITY,
        [EnumMember(Value = "PPIP_PRODUCT_TOTAL")]
        PPIP_PRODUCT_TOTAL,
        [EnumMember(Value = "PPIP_ROLE_TYPE")]
        PPIP_ROLE_TYPE
    }
}
