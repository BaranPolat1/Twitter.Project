using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FinalProject.DataAccess.KernelRepository.Abstraction
{
    public interface IEntityRepository<T>
    {
        bool Any(Expression<Func<T, bool>> exp);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        T GetById(Guid id);
        T Find(Expression<Func<T, bool>> exp);
        ICollection<T> GetAll();
       

        ICollection<T> FindByList(Expression<Func<T, bool>> exp);
    }
}
