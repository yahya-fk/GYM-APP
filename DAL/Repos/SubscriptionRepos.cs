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
                subscriptions = myDbContext.Subscriptions
                    .Where(x => x.SubExpiredDate > DateTime.Now && x.SubStatus != "Active")
                    .ToList();

                foreach (var subscription in subscriptions)
                {
                    subscription.SubStatus = "Active";
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
        public Subscription ReadByUserAndSub(int UserId)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                return myDbContext.Subscriptions.Where(u => u.UserId == UserId )
                           .OrderByDescending(u => u.SubExpiredDate)
                           .FirstOrDefault();
            }
           
        }
        public Subscription ReadbyUserId(int UserId)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                return myDbContext.Subscriptions.OrderByDescending(u => u.SubExpiredDate).FirstOrDefault(u => u.UserId == UserId);
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
                myDbContext.Update(updatedEntity);
                 myDbContext.SaveChanges();
                

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
