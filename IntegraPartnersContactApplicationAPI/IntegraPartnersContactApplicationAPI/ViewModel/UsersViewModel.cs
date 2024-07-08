using System.ComponentModel.DataAnnotations;

namespace IntegraPartnersContactApplicationAPI.ViewModel
{
    public class UsersViewModel
    {
    
        public int UserID { get; set; }
        public string ? Username { get; set; }
        public string ? FirstName { get; set; }
        public string ? LastName { get; set; }
        public string Email { get; set; } = "";
        public string ? UserStatus { get; set; }
        public string ? Department { get; set; }
    }
}
