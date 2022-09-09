using Catalog.API.Entities;
using Catalog.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {        
        public CatalogContext(IOptions<ProductDatabaseSettings> productDatabaseSettings)
        {
            
            var client = new MongoClient(productDatabaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(productDatabaseSettings.Value.DatabaseName);

            Products = database.GetCollection<Product>(productDatabaseSettings.Value.CollectionName);
            CatalogContextSeed.SeedData(Products);
        }
        
        public IMongoCollection<Product> Products { get; }
    }
}
