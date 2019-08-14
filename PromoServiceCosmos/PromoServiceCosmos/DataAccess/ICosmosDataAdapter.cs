using System.Collections.Generic;
using System.Threading.Tasks;

using PromoServiceCosmos.Models;

namespace PromoServiceCosmos.DataAccess
{
    public interface ICosmosDataAdapter
    {
     /*   Task<bool> CreateDatabase(string name);
        Task<bool> CreateCollection(string dbName, string name);*/
        Task<ProductPromo> CreateDocument(string dbName, string name, ProductPromo productpromo);
        Task<ProductPromo> DeleteUserAsync(string dbName, string name, string id);

        Task<dynamic> GetData(string dbName, string name);
        //Task<dynamic> GetData(string dbName, string name, ProductPromo productpromo);
        Task<ProductPromo> updateDocumentAsync(string dbName, string name, ProductPromo productpromo);
        Task<dynamic> GetDataById(string dbName, string name, string id);


        Task<bool> UpdateDocumentAsyncCon(string dbName, string name, ProductPromo productPromoCondition);


        Task<bool> CreateDocumentCondition(string dbName, string name, ProductPromo productPromoCondition);

        Task<bool> CreateAction(string dbName, string name, ProductPromo productPromo);

         Task<bool> UpdateActionDocumentAsync(string dbName, string name, ProductPromo promo);
        Task<ProductPromoCondition> DeleteUserAsyncCondition(string dbName, string name, string index);
        Task<bool> updateDocumentAsyncResponse(string dbName, string name, ProductPromo promo);
    }
}