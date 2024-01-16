using DAL.Entity;
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
            using (MyDbContext myDbContext = new MyDbContext())
            {
                var existingEntity = Read(updatedEntity.Id);
                if (existingEntity != null)
                {
                    existingEntity.IsAdmin = updatedEntity.IsAdmin;
                    existingEntity.Nom = updatedEntity.Nom;
                    existingEntity.Prenom = updatedEntity.Prenom;
                    existingEntity.Email = updatedEntity.Email;
                    existingEntity.Tel = updatedEntity.Tel;
                    existingEntity.Img = updatedEntity.Img;
                    existingEntity.MotPasse = updatedEntity.MotPasse;

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
                    myDbContext.Users.Remove(entityToDelete);
                    myDbContext.SaveChanges();
                }
            }
        }
    }
}

