using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegraPartnersContactApplicationAPI.ViewModel
{
    public class UserViewModel
    {
        [BindProperty(Name = "UserID", SupportsGet = true)]

        public int ? UserID { get; set; }
        [BindProperty(Name = "Username", SupportsGet = true)]
        public string ? Username { get; set; }
        [BindProperty(Name = "FirstName", SupportsGet = true)]
        public string ? FirstName { get; set; }
        [BindProperty(Name = "LastName", SupportsGet = true)]
        public string ? LastName { get; set; }
        [BindProperty(Name = "Email", SupportsGet = true)]
        public string Email { get; set; } = "";
        [BindProperty(Name = "UserStatus", SupportsGet = true)]
        public string ? UserStatus { get; set; }
        [BindProperty(Name = "Department", SupportsGet = true)]
        public string ? Department { get; set; }
    }
}
