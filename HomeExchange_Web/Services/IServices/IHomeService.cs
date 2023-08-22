using HomeExchange_Web.Models.Dto;

namespace HomeExchange_Web.Services.IServices
{
    public interface IHomeService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id,string token);
        Task<T> CreateAsync<T>(HomeCreateDTO dto,string token);
        Task<T> UpdateAsync<T>(HomeUpdateDTO dto,string token);
        Task<T> DeleteAsync<T>(int id,string token);
    }
}
