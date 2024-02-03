using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.User
{
    public class UserEditVM
    {    
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public string MotDePasse { get; set; } = string.Empty;
        public UserEditVM()
        {
        }
        public UserEditVM(UserListVM userVM)
        {
            int.TryParse(userVM.Id, out int userId);

            Id = userId;
            Nom=userVM.Nom; 
            Prenom=userVM.Prenom;
            Email=userVM.Email;
            Tel=userVM.Tel;
            Img=userVM.Img;
            MotDePasse=userVM.MotDePasse;
        }

       

        public UserEditVM(int id, string nom, string prenom, string email, string tel, string img, string motDePasse)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Email = email;
            Tel = tel;
            Img = img;
            MotDePasse = motDePasse;
        }
    }
}
