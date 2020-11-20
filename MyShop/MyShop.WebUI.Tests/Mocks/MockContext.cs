using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.WebUI.Tests.Mocks
{
    public class MockContext<T> : IRepository<T> where T:BaseEntity
    {
        List<T> items;
        string className;

        public MockContext()
        {
            items=new List<T>();
        }
        public void Commit()
        {
            return;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T tTOUpdated = items.Find(i => i.Id == t.Id);

            if (tTOUpdated != null)
            {
                tTOUpdated = t;
            }
            else
            {
                throw new Exception(className + "NotFound");
            }
        }
        public T Find(string Id)
        {
            T t = items.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + "Not Found");
            }
        }
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }
        public void Delete(string Id)
        {
            T tToDelete = items.Find(i => i.Id == Id);

            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + "NotFound");
            }
        }
    }
}
