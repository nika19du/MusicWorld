using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicWorld.Data.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public User()
        {
            this.Catalogs = new List<Catalog>();
        }

        public string Username { get; set; }

        public string Password { get; set; }
        public string FirstName { get; set; }
         
        public string LastName { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Catalog> Catalogs { get; set; }
    }
}
