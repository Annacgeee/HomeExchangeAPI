using System.Linq.Expressions;
using HomeExchangeAPI.Models;

namespace HomeExchangeAPI.Repository.IRepository
{
    public interface IHomeNumberRepository : IRepository<HomeNumber>
    {

        Task<HomeNumber> UpdateAsync(HomeNumber entity);

    }
}