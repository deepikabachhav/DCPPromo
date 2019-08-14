using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PromoServiceCosmos.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProductPromoOperator
    {
        [EnumMember(Value = "PPC_EQ")]
        PPC_EQ,
        [EnumMember(Value = "PPC_GT")]
        PPC_GT,
        [EnumMember(Value = "PPC_GTE")]
        PPC_GTE,
        [EnumMember(Value = "PPC_LT")]
        PPC_LT,
        [EnumMember(Value = "PPC_LTE")]
        PPC_LTE,
        [EnumMember(Value = "PPC_NEQ")]
        PPC_NEQ
    }
}
