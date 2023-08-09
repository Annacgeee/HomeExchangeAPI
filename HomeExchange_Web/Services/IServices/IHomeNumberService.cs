using HomeExchange_Web.Models.Dto;

namespace HomeExchange_Web.Services.IServices
{
    public interface IHomeNumberService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(HomeNumberCreateDTO dto);
        Task<T> UpdateAsync<T>(HomeNumberUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
