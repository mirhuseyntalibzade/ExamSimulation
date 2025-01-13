using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestCORE.Models;

namespace TestDAL.Repositories.Abstractions
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        Task AddAsync(T entity);
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetbyIdAsync(int Id);
        Task<T> GetByConditionAsync(Expression<Func<T, bool>> condition);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}
