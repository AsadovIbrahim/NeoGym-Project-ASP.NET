using DataBase.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositories.Abstracts
{
    public interface IGenericRepository<T>where T : Entity,new()
    {
        Task<ICollection<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<T?>GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
