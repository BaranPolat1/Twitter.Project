
using FinalProject.DataAccess.Context;
using FinalProject.DataAccess.KernelRepository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DataAccess.KernelRepository.Concrete
{
    public class EntityRepositoryEF<T> : IEntityRepository<T> where T : class
    {
        public DbContext db;
        protected DbSet<T> table;

        public EntityRepositoryEF(DbContext _db)
        {
            db = _db;
            table = db.Set<T>();
        }
      
        public void Add(T item)
        {
           
         table.Add(item);
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            return table.Any(exp);
        }

        public void Delete(T item)
        {
            table.Remove(item);
        }

        public  T Find(Expression<Func<T, bool>> exp)
        {
            return table.Where(exp).FirstOrDefault();
        }

        public  ICollection<T> FindByList(Expression<Func<T, bool>> exp)
        {
            return  table.Where(exp).ToList();
        }

        public ICollection<T> GetAll()
        {
            return  table.ToList();
        }

        public  T GetById(Guid id)
        {

            return table.Find(id);
        }

        public void Update(T item)
        {
             table.Update(item);
        }
    }
}
