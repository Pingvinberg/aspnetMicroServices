﻿using Discount.API.Entities;

namespace Discount.API.Repositories
{
    public interface IDiscountRepository
    {
        Task<List<Coupon>> GetDiscounts();
        Task<Coupon> GetDiscount(string productName);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> RemoveDiscount(string productName);
    }
}
