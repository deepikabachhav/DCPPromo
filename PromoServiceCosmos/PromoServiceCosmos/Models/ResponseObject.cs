using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace PromoServiceCosmos.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    
    public class ResponseObject 
    {
       [JsonProperty(PropertyName = "correlationId")]

        public string correlationalId { get; set; }
        public int statusCode { get; set; }
        public string statusReason { get; set; }
        public bool success { get; set; }
        //  public string Id { get; set; }


       // public List<messages> messages { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ProductPromo promotion { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ProductPromo> promotions { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
       // [JsonProperty(PropertyName = "promotionId")]
        public string promotionId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ProductPromoCondition> conditions { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ProductPromoCondition condition { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    /*public class messages
    {
        public string Key { get; set; }

    }*/
}
