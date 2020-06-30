using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BookStore.Domain.Implementation.Repo
{
    public class GenericRepository<TEntity> where TEntity: class
    {
        internal ApplicationDbContext _context;
        internal DbSet<TEntity> _dbSet;


        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _dbSet = _context.Set<TEntity>();
        }


        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProp = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var property in includeProp.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }



            return query.ToList();
        }
        //validation is done before this call
        public virtual TEntity GetById(int id)
        {
            return _dbSet.Find(id);
            //return _dbSet.FirstOrDefault(x=>x.Id==id);
        }
        public virtual TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }
        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public virtual void Delete(object id)
        {
            var entityToDel = _dbSet.Find(id);
            _dbSet.Remove(entityToDel);
        }
        public virtual void Delete(TEntity entityToDel)
        {
            if (_context.Entry(entityToDel).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDel);
            }
            _dbSet.Remove(entityToDel);
        }
        public virtual void BulkDelete(TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public virtual TEntity FirstOrDefault()
        {
            return _dbSet.FirstOrDefault();
        }

    }
}
