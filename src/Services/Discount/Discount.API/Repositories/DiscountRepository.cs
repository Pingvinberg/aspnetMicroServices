using Dapper;
using Discount.API.Entities;
using Discount.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IOptions<DiscountDbSettings> _options;
        public DiscountRepository(IOptions<DiscountDbSettings> discountDbSettings)
        {
            _options = discountDbSettings;
        }


        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_options.Value.ConnectionString);

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName =@ProductName", new { ProductName = productName });
            if(coupon == null)
            {
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            }
            return coupon;
        }

        public async Task<List<Coupon>> GetDiscounts()
        {
            using var connection = new NpgsqlConnection(_options.Value.ConnectionString);
            var coupons = await connection.QueryAsync<Coupon>("SELECT * FROM Coupon");
            if(coupons == null)
            {
                return null;                
            }
            return coupons.ToList();
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_options.Value.ConnectionString);

            var affected = await connection.ExecuteAsync("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@Productname, @Description, @Amount)", 
                new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount
                });

            if(affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_options.Value.ConnectionString);

            var affected = await connection.ExecuteAsync("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RemoveDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_options.Value.ConnectionString);

            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName =@ProductName",
                new { Productname = productName });
            if(affected == 0)
            {
                return false;
            }
            return true;           
        }
    }
}
