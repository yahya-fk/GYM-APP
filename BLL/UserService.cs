using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;
using DAL.Repos;
using Models;
using Models.User;
namespace BLL
{
    public class UserService
    {
        public UserListVM ReadNormal(int id) { 
            var User = new UserListVM();
            var userRepos = new UserRepos();
            var user = new User();
            user=userRepos.Read(id);
            User.Id = id;
            User.Tel = user.Tel;
            User.Nom = user.Nom;
            User.Email = user.Email;
            User.Prenom=user.Prenom;
            return User;
        }

        public bool UserLogin(int id,string email, string password)
        {
            UserRepos userRepos = new UserRepos();
            User user = new User();
             user= userRepos.Read(id);
            if (user == null) { 
                return false;
            }
            else
            {
                if (user.Email == email)
                {
                    if (user.MotPasse == password)
                    {
                        return true;
                    }
                    else { return false; }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
