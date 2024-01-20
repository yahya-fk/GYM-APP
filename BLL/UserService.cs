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
        UserRepos userRepos = new UserRepos();
        public UserListVM ReadNormal(int id) { 
            var User = new UserListVM();
            var user = new User();
            user=userRepos.Read(id);
            User.Id = id.ToString();
            User.Tel = user.Tel;
            User.Nom = user.Nom;
            User.Email = user.Email;
            User.Prenom=user.Prenom;
            User.IsAuth = user.IsAuth;
            User.Prenom = user.Prenom;

            return User;
        }
        public bool CheckEmail(String Email)
        {

            if (userRepos.Read(Email) != null)
            {
                return false;
            }
            return true;
        }
        public int GetUserIdByEmail(String Email)
        {
            User user = userRepos.Read(Email);
            return user.Id;
        }
       public void UserAdd(int id, int isAuth,int isAdmin, string nom, string prenom, string email, string tel, string img, string motPasse) { 
            User user = new User( id, isAuth,  isAdmin,  nom,  prenom,  email,  tel,  img,  motPasse);
            userRepos.Create(user);
        }
        public void UserAdd( int isAuth,int isAdmin, string nom, string prenom, string email, string tel, string img, string motPasse) {
            DateTime currentDate = DateTime.Now;
            int year = currentDate.Year%100 ;
            int month = currentDate.Month;
            int day = currentDate.Day;
            int hour = currentDate.Hour;
            int minute = currentDate.Minute;
            int second = currentDate.Second;
            int UserId = (year * 1000000) + (month * 10000) + (day * 100) + (hour * 100) + minute * 10 + second;
            Console.WriteLine(UserId);
            User user = new User( UserId,isAuth,  isAdmin,  nom,  prenom,  email,  tel,  img,  motPasse);
            userRepos.Create(user);
        }

        public User UserLogin(Models.Login.LoginVM userAuth)
        {
            User user = new User();
            user = userRepos.Read(userAuth.Email);
            if (user == null)
            {
                return null;
            }
            else
            {
                if (user.MotPasse == userAuth.MotdePasse && user.IsAuth==1)
                {
                    return user;

                }
                else
                {
                    return null;
                }
            }
        }
    }
}
