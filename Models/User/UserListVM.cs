using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.User
{
    public class UserListVM
    {
        public string Id { get; set; }
        public int IsAuth { get; set; }
        public int IsAdmin { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;

        public string SubStatus { get; set; } = string.Empty;
        public string SubExpiredDate { get; set; } = string.Empty;
        public string MotDePasse { get; set; } = string.Empty;

    }
}
