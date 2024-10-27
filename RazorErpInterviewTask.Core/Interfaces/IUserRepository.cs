using RazorErpInterviewTask.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RazorErpInterviewTask.Core.Interfaces
{
    public interface IUserRepository<T, U, V> where T : class where U : class where V : class
    {
        Task<IEnumerable<T>> GetAllAsync(string role, string company);
        Task<T> GetByIdAsync(int id, string role, string company);
        Task<int> AddAsync(U entity);
        Task<int> UpdateAsync(int id, U entity);
        Task<int> DeleteAsync(int id);
        Task<string> Auth(V entity);
    }
}

