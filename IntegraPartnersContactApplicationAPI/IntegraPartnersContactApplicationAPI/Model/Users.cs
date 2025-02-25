﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegraPartnersContactApplicationAPI.Model
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ? user_id { get; set; }
        public string? user_name { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string email { get; set; } = "";
        public string? user_status { get; set; }
        public string? department { get; set; }
    }
}
