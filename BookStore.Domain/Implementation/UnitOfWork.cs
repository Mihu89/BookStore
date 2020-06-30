using BookStore.Domain.Entities;
using BookStore.Domain.Implementation.Repo;
using BookStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Implementation
{
    public class UnitOfWork<TEntity> where TEntity : class
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private bool disposed = false;

        private GenericRepository<TEntity> _genericRepository;

        public GenericRepository<TEntity> GenericRepository
        {
            get
            {
                return _genericRepository ?? new GenericRepository<TEntity>(_context);

            }
        }
        public UnitOfWork(GenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
