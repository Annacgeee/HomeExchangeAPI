using HomeExchange_Web.Models.Dto;

namespace HomeExchange_Web.Services.IServices
{
    public interface IHomeService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(HomeCreateDTO dto);
        Task<T> UpdateAsync<T>(HomeUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
