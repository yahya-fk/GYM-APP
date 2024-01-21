using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class UserRepos
    {
        public void Create(User entity)
        {
            MyDbContext myDbContext = new MyDbContext();
            myDbContext.Users.Add(entity);
            myDbContext.SaveChanges();
        }
        public User Read(int id)
        {
            MyDbContext myDbContext = new MyDbContext();
            return myDbContext.Users.Find(id);
        }
        public User Read(string email)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                return myDbContext.Users.FirstOrDefault(u => u.Email == email);
            }
        }
        public List<User> GetAll()
        {
            MyDbContext myDbContext = new MyDbContext();

            return myDbContext.Users.ToList();
        }
        public void Update(User updatedEntity)
        {
            using (var dbContext = new MyDbContext())
            {
                dbContext.Update(updatedEntity);
                dbContext.SaveChanges();
            }
        }


        public void Delete(int id)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                var entityToDelete = Read(id);
                if (entityToDelete != null)
                {
                    myDbContext.Users.Remove(entityToDelete);
                    myDbContext.SaveChanges();
                }
            }
        }
    }
}

