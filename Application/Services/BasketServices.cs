using Application.Interfaces;
using Domain.Domains;
using Persistence;
using Microsoft.EntityFrameworkCore;


namespace Application.Services
{
    public class BasketServices : IBasket
    {
        private readonly TheGatDBContext context;

        public BasketServices(TheGatDBContext _context)
        {
            context = _context;
        }

        public async Task AddAsync(Basket basket)
        {
            await context.Baskets.AddAsync(basket);
            await context.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(int id)
        {
            var bas = await GetByIdAsync(id);
            if (bas != null)
            {
                context.Baskets.Remove(bas);
                await context.SaveChangesAsync();
            }
           
            
        }

        public async Task<IEnumerable<Basket>> GetAllAsync()
        {
           return await context.Baskets.ToListAsync();
        }

        public async Task<Basket> GetByIdAsync(int id)
        {
            return await context.Baskets.FindAsync(id);   
        }

        public async Task UpdateAsync(Basket newbasket)
        {
            context.Baskets.Update(newbasket);
            await context.SaveChangesAsync();
        }
        public void UpdateAmount(int id, int amount)
        {
            var basketItem = context.Baskets.FirstOrDefault(x => x.Id == id);
            if (basketItem != null)
            {
                basketItem.Amount = amount;
                context.SaveChanges();
            }
        }

        public decimal GetTotal(int id)
        {
            var basketItem = context.Baskets.FirstOrDefault(x => x.Id == id);
            return basketItem == null ? 0 : basketItem.Price * basketItem.Amount;
        }
    }
}
