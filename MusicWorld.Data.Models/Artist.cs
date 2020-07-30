using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicWorld.Data.Models
{
    public class Artist
    {
        public Artist()
        {
            this.Songs = new List<Song>();
            this.Albums = new List<Album>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        [MaxLength(64, ErrorMessage = "The name must be up to 64 characters")]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }


        [Required]
        [MaxLength(255, ErrorMessage = "The description must be up to 64 characters")]
        public string Description { get; set; }

        public ICollection<Album> Albums { get; set; }

        public ICollection<Song> Songs  { get; set; }
    }
}
