using System.Linq.Expressions;
using HomeExchangeAPI.Data;
using HomeExchangeAPI.Models;
using HomeExchangeAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HomeExchangeAPI.Repository
{
    public class HomeNumberRepository : Repository<HomeNumber>, IHomeNumberRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeNumberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       

        public async Task<HomeNumber> UpdateAsync(HomeNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.HomeNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}