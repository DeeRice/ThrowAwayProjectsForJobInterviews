using System.ComponentModel.DataAnnotations;

namespace IntegraPartnersContactApplicationAPI.ViewModel
{
    public class Users
    {
        [Key]
        public int user_id { get; set; }
        public string ? user_name { get; set; }
        public string ? first_name { get; set; }
        public string ? last_name { get; set; }
        public string email { get; set; } = "";
        public string ? user_status { get; set; }
        public string ? Department { get; set; }
    }
}
