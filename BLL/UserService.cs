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
    }
}
