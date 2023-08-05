using System.Linq.Expressions;
using HomeExchangeAPI.Data;
using HomeExchangeAPI.Models;
using HomeExchangeAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HomeExchangeAPI.Repository
{
    public class HomeRepository : Repository<Home>, IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       

        public async Task<Home> UpdateAsync(Home entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Homes.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}