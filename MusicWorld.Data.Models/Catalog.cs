using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicWorld.Data.Models
{
    public class Catalog
    {
        public Catalog()
        {
            this.Songs = new List<Song>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [MaxLength(64,ErrorMessage ="The name must be up to 64 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "The description must be up to 64 characters")]
        public string Description { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}
