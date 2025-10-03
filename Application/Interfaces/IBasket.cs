using Domain.Domains;

namespace Application.Interfaces
{
    public interface IBasket
    {
        Task< IEnumerable<Basket>> GetAllAsync();
        Task<Basket> GetByIdAsync(int id);
        Task AddAsync(Basket basket);
        Task UpdateAsync(Basket basket);
        Task DeleteAsync(int id);
        void UpdateAmount(int id, int amount);
        decimal GetTotal(int id);
    }
}
