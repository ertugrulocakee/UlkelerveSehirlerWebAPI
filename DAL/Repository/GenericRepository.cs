using DAL.Abstract;
using DAL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericDAL<T> where T : class
    {
        public void Add(T item)
        {
            using var c = new Context();
            c.Add(item);
            c.SaveChanges();    

        }

        public List<T> GetAll()
        {

            using var c = new Context();
            return c.Set<T>().ToList(); 

        }

        public List<T> GetbyFilter(Expression<Func<T, bool>> filter)
        {

            using var c = new Context();
            return c.Set<T>().Where(filter).ToList();

        }

        public T GetT(int index)
        {

            using var c = new Context();
            return c.Set<T>().Find(index);

        }

        public void Remove(T item)
        {
            
            using var c = new Context();    
            c.Remove(item);
            c.SaveChanges();

        }

        public void Update(T item)
        {
            using var c = new Context();
            c.Update(item);
            c.SaveChanges();
        }
    }
}
