using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoServiceCosmos.DataAccess;
using PromoServiceCosmos.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace PromoServiceCosmos.Controllers
{
    [Route("/v1/promotions")]
    
    [ApiController]

  
    public class CosmosController : ControllerBase
    {
        ProductPromo productpromo = null;
        List<ProductPromo> promotions = null;
     //   public static string correlationalId = "X_CORRELATION";
        ICosmosDataAdapter _adapter;
        public CosmosController(ICosmosDataAdapter adapter)
        {
            _adapter = adapter;
        }

        /*[HttpGet("createdb")]
        public async Task<IActionResult> CreateDatabase()
        {
            var result = await _adapter.CreateDatabase("PromoDatabase");
            return Ok(result);
        }

        [HttpGet("createcollection")]
        public async Task<IActionResult> CreateCollection()
        {
            var result = await _adapter.CreateCollection("PromoDatabase", "PromoCollection");
            return Ok(result);
        }*/

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] ProductPromo productpromo)
        {
            ResponseObject responseobject = new ResponseObject();
            try
            {
                var result = await _adapter.CreateDocument("PromoDatabase", "PromoCollection", productpromo);
                productpromo = result;
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 201;
                responseobject.statusReason = "Created";
                responseobject.success = true;
                responseobject.promotionId = result.Id;
                return StatusCode(StatusCodes.Status201Created, responseobject);
            }
            catch (Exception ex)
            {
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 400;
                responseobject.statusReason = "Bad Request";
                responseobject.success = false;
                return StatusCode(StatusCodes.Status400BadRequest, responseobject);
            }
         
        }


        /* [HttpDelete("{id}")]
         public async Task<IActionResult> Delete(string id)
         {
             ResponseObject responseobject = new ResponseObject();
             try
             {
                 var result = await _adapter.DeleteUserAsync("PromoDatabase", "PromoCollection", id);
                  responseobject.correlationalId = Guid.NewGuid().ToString();
                     responseobject.statusCode = 202;
                     responseobject.statusReason = "Accepted";
                     responseobject.success = true;
                     return Ok(responseobject);

             }
             catch (Exception ex)
             {
                 responseobject.correlationalId = Guid.NewGuid().ToString();
                 responseobject.statusCode = 500;
                 responseobject.statusReason = "Internal Server Error";
                 responseobject.success = false;
                 return StatusCode(StatusCodes.Status500InternalServerError, responseobject);
             }
         }*/

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {  
            ResponseObject responseobject = new ResponseObject();
            try
            {
                List<ProductPromo> promos = await _adapter.GetData("PromoDatabase", "PromoCollection");
                string indexValue = "";
                if (Request.Headers.ContainsKey("ids"))
                {
                    indexValue = Request.Headers["ids"].First();
                    string[] indexValuesArray = indexValue.Split(",");
                    foreach (var promo in promos.ToList())
                    {
                        bool present = indexValuesArray.Contains(promo.Id);
                        if (present)
                        {
                            var result = await _adapter.updateDocumentAsyncResponse("PromoDatabase", "PromoCollection", promo);
                            promos.Remove(promo);
                        }
                    }
                }
               // responseobject.promotions = promos;
                List<ProductPromo> promotions = responseobject.promotions;
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 202;
                responseobject.statusReason = "Accepted";
                responseobject.success = true;
                return Ok(responseobject);
            }
            catch (Exception ex)
            {
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 500;
                responseobject.statusReason = "Internal Server Error";
                responseobject.success = false;
                return StatusCode(StatusCodes.Status500InternalServerError, responseobject);
            }
        }

        /*
                [HttpGet]
                public async Task<IActionResult> Get()
                {
                    //List<ProductPromo> 
                    //var  result = await _adapter.GetData("PromoDatabase", "PromoCollection");
                    var result = await _adapter.GetData("PromoDatabase", "PromoCollection");
                    //new System.Linq.SystemCore_EnumerableDebugView<object>(result).Items
                   ResponseObject responseobject = new ResponseObject();
                    // promotions = result;
                     responseobject.correlationalId = Guid.NewGuid().ToString();
                     responseobject.statusCode = 200;
                     responseobject.statusReason = "OK";
                     responseobject.success = true;
                   responseobject.promotions = result ;
                    return Ok(responseobject);
                }*/



        /* protected override void resolveCorrelationId(HttpServletRequest request)
         {
             // Look for the correlation ID in the request header
             String correlationId = request.getHeader(HEADER_FIELD_CORRELATION_ID);
             if (StringUtils.isBlank(correlationId))
             {
                 // If the request header does not have the correlation ID generate a new one for
                 // this request
                 correlationId = UUID.randomUUID().toString();
             }
             setCorrelationId(correlationId);
         }*/



        /* [HttpGet("{id}")]
         public async Task<IActionResult> Get(string id)
         {

             ResponseObject responseobject = new ResponseObject();

             try
             {
                 var result = await _adapter.GetDataById("PromoDatabase", "PromoCollection", id);

                 productpromo = result;
                 responseobject.correlationalId = Guid.NewGuid().ToString();
                 responseobject.statusCode = 200;
                 responseobject.statusReason = "OK";
                 responseobject.success = true;

                 responseobject.promotion = productpromo;
              return Ok(responseobject);

             }
             catch (Exception ex)
             {
                // ResponseObject responseobject = new ResponseObject();
                 responseobject.correlationalId = Guid.NewGuid().ToString();
                 responseobject.statusCode = 500;
                 responseobject.statusReason = "Internal Server Error";
                 responseobject.success = false;

                 return StatusCode(StatusCodes.Status500InternalServerError, responseobject);

             }
         }*/

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ResponseObject responseobject = new ResponseObject();

            try
            {
                List<ProductPromo> promos = await _adapter.GetData("PromoDatabase", "PromoCollection");
                List<ProductPromo> promotions = new List<ProductPromo>();
                string indexValue = "";
                if (Request.Headers.ContainsKey("ids"))
                {
                    indexValue = Request.Headers["ids"].First();
                    string[] ndexValuesArray = indexValue.Split(",");
                    foreach (var promo in promos.ToList())
                    {
                        bool present = ndexValuesArray.Contains(promo.Id);
                        if (present)
                        {
                            promotions.Add(promo);
                        }
                    }
                }
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 200;
                responseobject.statusReason = "OK";
                responseobject.success = true;
                responseobject.promotions = promotions;
                return Ok(responseobject);
            }
            catch (Exception ex)
            {
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 500;
                responseobject.statusReason = "Internal Server Error";
                responseobject.success = false;
                return StatusCode(StatusCodes.Status500InternalServerError, responseobject);
            }
        }
       

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductPromo productpromo)
        {
            ResponseObject responseobject = new ResponseObject();
            try
            {
                var result = await _adapter.updateDocumentAsync("PromoDatabase", "PromoCollection", productpromo);
              //  productpromo = result;
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 202;
                responseobject.statusReason = "Accepted";
                responseobject.success = true;
               // responseobject.promotionId = result.Id;
                return StatusCode(StatusCodes.Status202Accepted, responseobject);
            }
            catch (Exception ex)
            {
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 400;
                responseobject.statusReason = "Bad Request";
                responseobject.success = false;
                return StatusCode(StatusCodes.Status400BadRequest, responseobject);
            }
        }

        [HttpPost("{id}/conditions")]
        public async Task<IActionResult> CreateDocumentCondition(string id, [FromBody] ProductPromoCondition productPromoCondition)
        {
         
            ResponseObject responseobject = new ResponseObject();
            try
            {
                ProductPromo productPromo = await _adapter.GetDataById("PromoDatabase", "PromoCollection", id);
                if (productPromo.conditions == null)
                {
                    List<ProductPromoCondition> newList = new List<ProductPromoCondition>();
                    newList.Add(productPromoCondition); 
                    productPromo.conditions = newList;  
                }
                else
                {
                    productPromo.conditions.Add(productPromoCondition);
                }
                var result = await _adapter.CreateDocumentCondition("PromoDatabase", "PromoCollection", productPromo);
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 201;
                responseobject.statusReason = "Created";
                responseobject.success = true;
                return StatusCode(StatusCodes.Status201Created, responseobject);
            }
            catch (Exception ex)
            {
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 400;
                responseobject.statusReason = "Bad Request";
                responseobject.success = false;
                return StatusCode(StatusCodes.Status400BadRequest, responseobject);
            }
        }


        [HttpPut("{id}/conditions/{index}")]
        public async Task<IActionResult> UpdateConditions(string index, string id, [FromBody] ProductPromoCondition productPromoCondition)
        {
            ResponseObject responseobject = new ResponseObject();
            try
            {
                ProductPromo promo = await _adapter.GetDataById("PromoDatabase", "PromoCollection", id);
                List<ProductPromoCondition> productPromoConditionObj = promo.conditions;
                foreach (var item in productPromoConditionObj)
                {
                    if(item.index == index)
                    {
                        item.parameter = productPromoCondition.parameter;
                        item.promoOperator = productPromoCondition.promoOperator;
                        item.conditionValue = productPromoCondition.conditionValue;
                        item.otherValue = productPromoCondition.otherValue;
                    }
                }
                var result = await _adapter.UpdateDocumentAsyncCon("PromoDatabase", "PromoCollection", promo);
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 202;
                responseobject.statusReason = "Accepted";
                responseobject.success = true;
                // responseobject.promotionId = result.Id;
                return StatusCode(StatusCodes.Status202Accepted, responseobject);
            }
            catch (Exception ex)
            {
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 400;
                responseobject.statusReason = "Bad Request";
                responseobject.success = false;
                return StatusCode(StatusCodes.Status400BadRequest, responseobject);
            }
        }

        /* [HttpGet("{id}/conditions/{index}")]
         public async Task<IActionResult> GetConditionByIndex(string id, string index)
         {
             ResponseObject responseobject = new ResponseObject();
             try
             {
                 ProductPromo promo = await _adapter.GetDataById("PromoDatabase", "PromoCollection", id);
                List<ProductPromoCondition> productPromoConditionObj = promo.conditions;
                 foreach (var item in productPromoConditionObj)
                 {
                     if (item.index == index)
                     {
                         responseobject.correlationalId = Guid.NewGuid().ToString();
                         responseobject.statusCode = 200;
                         responseobject.statusReason = "OK";
                         responseobject.success = true;
                         responseobject.condition = item;
                     }
                 }
                 return Ok(responseobject);
             }
             catch (Exception ex)
             {

                 responseobject.correlationalId = Guid.NewGuid().ToString();
                 responseobject.statusCode = 500;
                 responseobject.statusReason = "Internal Server Error";
                 responseobject.success = false;
                 return StatusCode(StatusCodes.Status500InternalServerError, responseobject);
             }
         }
 */

        [HttpGet("{id}/conditions")]
        public async Task<IActionResult> GetConditionsByIndex(string id)
        {
            ResponseObject responseobject = new ResponseObject();
            try
            {
                ProductPromo promo = await _adapter.GetDataById("PromoDatabase", "PromoCollection", id);
                List<ProductPromoCondition> productPromoConditionObj = promo.conditions;
                List<ProductPromoCondition> conditions = new List<ProductPromoCondition>();
                string indexValue = "";
                if (Request.Headers.ContainsKey("indexes"))
                {
                    indexValue = Request.Headers["indexes"].First();
                    string[] ndexValuesArray = indexValue.Split(",");
                    foreach (var item in productPromoConditionObj.ToList())
                        {
                            bool present = ndexValuesArray.Contains(item.index);
                            if (present)
                             {
                                    conditions.Add(item);
                            }
                        }
                }
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 200;
                responseobject.statusReason = "OK";
                responseobject.success = true;
                responseobject.conditions = conditions;
                return Ok(responseobject);
            }
            catch (Exception ex)
            {
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 500;
                responseobject.statusReason = "Internal Server Error";
                responseobject.success = false;
                return StatusCode(StatusCodes.Status500InternalServerError, responseobject);
            }
        }


        /*
                [HttpDelete("{id}/conditions/{index}")]
                public async Task<IActionResult> DeleteConditionByIndex(string id, string index)
                {
                    ResponseObject responseobject = new ResponseObject();
                    try
                    {

                        ProductPromo promo = await _adapter.GetDataById("PromoDatabase", "PromoCollection", id);

                        List<ProductPromoCondition> productPromoConditionObj = promo.conditions;

                        foreach (var item in productPromoConditionObj.ToList())
                        {
                            if (item.index == index)
                            {
                                productPromoConditionObj.Remove(item);
                            }
                        }
                        var result = await _adapter.UpdateDocumentAsyncCon("PromoDatabase", "PromoCollection", promo);
                        responseobject.correlationalId = Guid.NewGuid().ToString();
                        responseobject.statusCode = 200;
                        responseobject.statusReason = "OK";
                        responseobject.success = true;
                        return Ok(responseobject);

                    }
                    catch (Exception ex)
                    {
                        responseobject.correlationalId = Guid.NewGuid().ToString();
                        responseobject.statusCode = 500;
                        responseobject.statusReason = "Internal Server Error";
                        responseobject.success = false;
                        return StatusCode(StatusCodes.Status500InternalServerError, responseobject);
                    }

                }*/

        /*
                [HttpGet("{id}/conditions")]
                public async Task<IActionResult> GetCondition(string id)
                {
                    ResponseObject responseobject = new ResponseObject();
                    try
                    {
                        ProductPromo promo = await _adapter.GetDataById("PromoDatabase", "PromoCollection", id);
                         var result = promo.conditions;                   
                                responseobject.correlationalId = Guid.NewGuid().ToString();
                                responseobject.statusCode = 200;
                                responseobject.statusReason = "OK";
                                responseobject.success = true;
                                responseobject.conditions = result;
                                 return Ok(responseobject);
                    }
                    catch (Exception ex)
                    {
                        responseobject.correlationalId = Guid.NewGuid().ToString();
                        responseobject.statusCode = 500;
                        responseobject.statusReason = "Internal Server Error";
                        responseobject.success = false;
                        return StatusCode(StatusCodes.Status500InternalServerError, responseobject);
                    }
                }
        */

        [HttpDelete("{id}/conditions")]
        public async Task<IActionResult> DeleteConditionsByIndex(string id)
        {
            ResponseObject responseobject = new ResponseObject();
            try
            {
                ProductPromo promo = await _adapter.GetDataById("PromoDatabase", "PromoCollection", id);
                List<ProductPromoCondition> productPromoConditionObj = promo.conditions;
                string indexValue = "";
                if (Request.Headers.ContainsKey("indexes"))
                {
                    indexValue = Request.Headers["indexes"].First();
                    string[] indexValuesArray = indexValue.Split(",");
                    foreach (var item in productPromoConditionObj.ToList())
                    {
                        bool present = indexValuesArray.Contains(item.index);
                        if (present)
                        {
                            productPromoConditionObj.Remove(item);
                        }
                    }
                }
                var result = await _adapter.UpdateDocumentAsyncCon("PromoDatabase", "PromoCollection", promo);
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 200;
                responseobject.statusReason = "OK";
                responseobject.success = true;
                return Ok(responseobject);
            }
            catch (Exception ex)
            {
                responseobject.correlationalId = Guid.NewGuid().ToString();
                responseobject.statusCode = 500;
                responseobject.statusReason = "Internal Server Error";
                responseobject.success = false;
                return StatusCode(StatusCodes.Status500InternalServerError, responseobject);
            }
        }


        /* [HttpDelete("{id}/conditions")]
         public async Task<IActionResult> DeleteConditions(string id)
         {
             ResponseObject responseobject = new ResponseObject();
             try
             {
                 ProductPromo promo = await _adapter.GetDataById("PromoDatabase", "PromoCollection", id);
                 promo.conditions.Clear();
                 var result = await _adapter.UpdateDocumentAsyncCon("PromoDatabase", "PromoCollection", promo);
                 responseobject.correlationalId = Guid.NewGuid().ToString();
                 responseobject.statusCode = 202;
                 responseobject.statusReason = "Accepted";
                 responseobject.success = true;
                 return Ok(responseobject);

             }
             catch (Exception ex)
             {
                 responseobject.correlationalId = Guid.NewGuid().ToString();
                 responseobject.statusCode = 500;
                 responseobject.statusReason = "Internal Server Error";
                 responseobject.success = false;
                 return StatusCode(StatusCodes.Status500InternalServerError, responseobject);
             }
         }*/


        [HttpPost("{id}/actions")]
        public async Task<IActionResult> CreateActions(string id, [FromBody] ProductPromoAction productPromoAction)
        {
            ProductPromo productPromo = await _adapter.GetDataById("PromoDatabase", "PromoCollection", id);
            productPromo.actions = productPromoAction;
            var result = await _adapter.CreateAction("PromoDatabase", "PromoCollection", productPromo);
            return Ok(result);
        }



        [HttpPut("{id}/actions/")]
        public async Task<IActionResult> UpdateActions(string id, [FromBody] ProductPromoAction productPromoAction)
        {
            ProductPromo promo = await _adapter.GetDataById("PromoDatabase", "PromoCollection", id);
            var productPromoActionInstance = promo.actions;



            if (productPromoActionInstance != null)
            {
                productPromoActionInstance.type = productPromoAction.type;
                productPromoActionInstance.quantity = productPromoAction.quantity;
                productPromoActionInstance.amount = productPromoAction.amount;
                productPromoActionInstance.productId = productPromoAction.productId;
                productPromoActionInstance.catalogId = productPromoAction.catalogId;
            }
            else
            {
                return NotFound();
            }
            var result = await _adapter.UpdateActionDocumentAsync("PromoDatabase", "PromoCollection", promo);
            return Ok(result);
        }


    }
}
