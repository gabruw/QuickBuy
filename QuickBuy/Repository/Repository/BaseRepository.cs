using Domain.IRepository;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly QuickBuyContext _quickBuyContext;

        public BaseRepository(QuickBuyContext quickBuyContext)
        {
            _quickBuyContext = quickBuyContext;
        }

        public void Incluid(TEntity entity)
        {
            _quickBuyContext.Set<TEntity>().Add(entity);
            _quickBuyContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _quickBuyContext.Set<TEntity>().Update(entity);
            _quickBuyContext.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            _quickBuyContext.Remove(entity);
            _quickBuyContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _quickBuyContext.Set<TEntity>().ToList();
        }

        public TEntity GetbyId(long Id)
        {
            return _quickBuyContext.Set<TEntity>().Find(Id);
        }

        public void Dispose()
        {
            _quickBuyContext.Dispose();
        }
    }
}