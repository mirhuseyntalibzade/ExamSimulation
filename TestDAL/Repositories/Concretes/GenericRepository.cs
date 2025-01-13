using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestCORE.Models;
using TestDAL.Contexts;
using TestDAL.Repositories.Abstractions;

namespace TestDAL.Repositories.Concretes;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
{

    readonly AppDbContext _context;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    DbSet<T> Table => _context.Set<T>();

    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public async Task<ICollection<T>> GetAllAsync()
    {
        return await Table.ToListAsync();
    }

    public async Task<T> GetByConditionAsync(Expression<Func<T, bool>> condition)
    {
        return await Table.FirstOrDefaultAsync(condition);
    }

    public async Task<T> GetbyIdAsync(int Id)
    {
        return await Table.FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        Table.Update(entity);
    }
}
