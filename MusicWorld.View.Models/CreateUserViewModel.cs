using System;
using System.ComponentModel.DataAnnotations;

namespace MusicWorld.View.Models
{
    public class CreateUserViewModel
    { 
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
