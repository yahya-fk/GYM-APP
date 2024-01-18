using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class SubscriptionRepos
    {
        public void UpdateSubscriptionStatus()
        {
            using (var myDbContext = new MyDbContext())
            {
                var subscriptions = myDbContext.Subscriptions
                    .Where(x => x.SubExpiredDate < DateTime.Now && x.SubStatus != "Expired")
                    .ToList();

                foreach (var subscription in subscriptions)
                {
                    subscription.SubStatus = "Expired";
                }

                myDbContext.SaveChanges();
            }
        }
        public void Create(Subscription entity)
        {
            MyDbContext myDbContext = new MyDbContext();
            myDbContext.Subscriptions.Add(entity);
            myDbContext.SaveChanges();

        }
        public Subscription Read(int id)
        {
            MyDbContext myDbContext = new MyDbContext();
            return myDbContext.Subscriptions.Find(id);
        }
        public Subscription ReadbyUserId(int UserId)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                return myDbContext.Subscriptions.FirstOrDefault(u => u.UserId == UserId);
            }
        }
        public List<Subscription> GetAll()
        {
            MyDbContext myDbContext = new MyDbContext();

            return myDbContext.Subscriptions.ToList();
        }
        public void Update(Subscription updatedEntity)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                var existingEntity = Read(updatedEntity.SubId);
                if (existingEntity != null)
                {
                    existingEntity.UserId = updatedEntity.UserId;
                    existingEntity.SubExpiredDate = updatedEntity.SubExpiredDate;
                    existingEntity.SubStatus = updatedEntity.SubStatus;
                    existingEntity.SubDate = updatedEntity.SubDate;
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
                    myDbContext.Subscriptions.Remove(entityToDelete);
                    myDbContext.SaveChanges();
                }
            }
        }
    }
}
