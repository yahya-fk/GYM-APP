using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class BillRepos
    {
        public void Create(Bill entity)
        {
            MyDbContext myDbContext = new MyDbContext();
            myDbContext.Bills.Add(entity);
            myDbContext.SaveChanges();

        }
        public Bill Read(int id)
        {
            MyDbContext myDbContext = new MyDbContext();
            return myDbContext.Bills.Find(id);
        }
        public Bill ReadbySubId(int SubID)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                return myDbContext.Bills
                .Where(u => u.SubId == SubID)
                .OrderByDescending(u => u.BillDate)
                .FirstOrDefault();
            }
        }
        public List<Bill> GetAll()
        {
            MyDbContext myDbContext = new MyDbContext();

            return myDbContext.Bills.ToList();
        }
        public void Update(Bill updatedEntity)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                myDbContext.Update(updatedEntity);
                myDbContext.SaveChanges(true);
              
            }
        }

        public void Delete(int id)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                var entityToDelete = Read(id);
                if (entityToDelete != null)
                {
                    myDbContext.Bills.Remove(entityToDelete);
                    myDbContext.SaveChanges();
                }
            }
        }
       
    }
}
