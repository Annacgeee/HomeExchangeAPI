using System.Linq.Expressions;
using HomeExchangeAPI.Models;

namespace HomeExchangeAPI.Repository.IRepository
{
    public interface IHomeRepository : IRepository<Home>
    {

        Task<Home> UpdateAsync(Home entity);

    }
}