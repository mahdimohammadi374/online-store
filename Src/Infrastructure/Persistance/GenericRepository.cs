using Application.Contracts;
using Application.Contracts.Specification;
using Domain.Entities.Base;
using Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistance
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DataBaseContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DataBaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return await Task.FromResult(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken token)
        {
           return await _dbSet.AnyAsync(predicate, token);
        }

        public async Task<bool> AnyAsync(CancellationToken token)
        {
            return await _dbSet.AnyAsync( token);
        }

        public async void Delete(T entity)
        {
           entity.IsDeleted = true;
            await UpdateAsync(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(x=>x.Id==id, cancellationToken);
        }


        public Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }


        public async Task<T> GetEntityWithSpec(ISpecification<T> specification,CancellationToken cancellationToken)
        {
            var result= await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
            return result;
        }

        public async Task<IReadOnlyList<T>> ListAsyncSpec(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            return await ApplySpecification(specification).ToListAsync(cancellationToken);
        }


        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), specification);
        }

        public Task<int> CountAsyncSpec(ISpecification<T> specification, CancellationToken cancellationToken)
        {

            return ApplySpecification(specification).CountAsync(cancellationToken);
        }
    }

}
