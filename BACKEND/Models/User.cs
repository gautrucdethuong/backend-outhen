﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BACKEND.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }


        [Required]
        [RegularExpression("^[a-z0-9_-]{3,16}$", ErrorMessage = "Fullname at least 2 words.")]
        public string username { get; set; }

        [Required]
        //[JsonIgnore]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Password must be between 4 and 8 digits long and include at least one numeric digit.")]
        public string password { get; set; }

        [Required]
        [RegularExpression("^[a-z A-Z]+$", ErrorMessage = "Fullname at least 2 words.")]
        public string fullname { get; set; }
       
        [Required]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage ="Username incorrect format.")]
        public string email  { get; set; }

        [MaxLength(15)]
        [Required]
        [RegularExpression("^\\d{10}$|^\\d{11}$", ErrorMessage = "Phone incorrect format.")]
        public string phone { get; set; }        

    }
}
