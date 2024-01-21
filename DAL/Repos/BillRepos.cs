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
                return myDbContext.Bills.FirstOrDefault(u => u.SubId == SubID);
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
                var existingEntity = Read(updatedEntity.BillId);

                if (existingEntity != null)
                {

                    existingEntity.BillMethod = updatedEntity.BillMethod;
                    existingEntity.BillDuration = updatedEntity.BillDuration;
                    existingEntity.BillStatus = updatedEntity.BillStatus;
                    existingEntity.SubId = updatedEntity.SubId;
                    existingEntity.SubType = updatedEntity.SubType;

                    myDbContext.SaveChanges();
                }
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
