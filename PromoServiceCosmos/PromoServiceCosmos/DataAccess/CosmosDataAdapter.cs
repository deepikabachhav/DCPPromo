using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PromoServiceCosmos.DataAccess.Utility;
using PromoServiceCosmos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PromoServiceCosmos.DataAccess
{
    public class CosmosDataAdapter : ICosmosDataAdapter
    {
        private readonly DocumentClient _client;
        private readonly string _accountUrl;
        private readonly string _primarykey;
      //  public static string correlationalId = "X-CORRELATION";
        ProductPromo productpromo = null;
        

        //ResponseCode resposecode;

        public CosmosDataAdapter(
         ICosmosConnection connection,
         IConfiguration config)
        {

            _accountUrl = config.GetValue<string>("Cosmos:AccountURL");
            _primarykey = config.GetValue<string>("Cosmos:AuthKey");
            _client = new DocumentClient(new Uri(_accountUrl), _primarykey);
        }




       /* public async Task<bool> CreateDatabase(string name)
        {
            try
            {
                await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = name });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> CreateCollection(string dbName, string name)
        {
            try
            {
                await _client.CreateDocumentCollectionIfNotExistsAsync
                 (UriFactory.CreateDatabaseUri(dbName), new DocumentCollection { Id = name });
                return true;
            }
            catch
            {
                return false;
            }
        }

      */
        public async Task<ProductPromo> CreateDocument(string dbName, string name, ProductPromo productpromo)
        {
            try
            {
              var result =  await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), productpromo);
                return (dynamic)result.Resource;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ProductPromo> DeleteUserAsync(string dbName, string name, string id)
        {
            try
            {
                var collectionUri = UriFactory.CreateDocumentUri(dbName, name, id);

                var result = await _client.DeleteDocumentAsync(collectionUri);

                return (dynamic)result.Resource;
            }
            catch (DocumentClientException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       
         public async Task<dynamic> GetData(string dbName, string name)
        {
            try
            {

                var response = await _client.ReadDocumentFeedAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), new FeedOptions { MaxItemCount = 10 });
                List<ProductPromo> productproList = new List<ProductPromo>();
                //(ProductPromo)(dynamic)response.Resource;

                
                foreach (Document doc in response)
                {
                    ProductPromo productpro = (ProductPromo)(dynamic)doc;
                    productproList.Add(productpro);
                }
                return productproList;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


       // var response = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, "POCO1"));



        public async Task<dynamic> GetDataById(string dbName, string name,string id)
        {

        try { 
               
                ResourceResponse<Document> response = await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(dbName, name, id));
               
                    ProductPromo productpro = (ProductPromo)(dynamic)response.Resource;
               
                return productpro;
            }
            catch(Exception ex) {
                return false;
            }
        }

        public async Task<ProductPromo> updateDocumentAsync(string dbName, string name, ProductPromo productpromo)
        {
            try
            {


                var response = await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(dbName, name, productpromo.Id), productpromo);

                return (dynamic)response.Resource;
            }
            catch
            {
                return null;
            }

        }


        public async Task<bool> CreateDocumentCondition(string dbName, string name, ProductPromo productPromoCondition1)
        {
            try
            {

                await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), productPromoCondition1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateDocumentAsyncCon(string dbName, string name, ProductPromo productPromoCondition)
        {
           
                try
                {
                   
                    await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), productPromoCondition);
                    return true;
                }
                catch
                {
                    return false;
                }  
        }

        public async Task<bool> CreateAction(string dbName, string name, ProductPromo productPromo)
        {
            try
            {
                await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), productPromo);
                return true;
            }
            catch
            {
                return false;
            }
        }



        public async Task<bool> UpdateActionDocumentAsync(string dbName, string name, ProductPromo productPromo)
        {
            try
            {
                await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), productPromo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProductPromoCondition> DeleteUserAsyncCondition(string dbName, string name, string index)
        {
            try
            {
                var collectionUri = UriFactory.CreateDocumentUri(dbName, name, index);

                var result = await _client.DeleteDocumentAsync(collectionUri);

                return (dynamic)result.Resource;
            }
            catch (DocumentClientException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> updateDocumentAsyncResponse(string dbName, string name, ProductPromo promo)
        {
                try
                {
                    var collectionUri = UriFactory.CreateDocumentUri(dbName, name, promo.Id);

                    var result = await _client.DeleteDocumentAsync(collectionUri);

                    return (dynamic)result.Resource;
                }
                catch (DocumentClientException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
                catch (Exception ex)
                {

                throw ex;
                }
        }
    }
}
