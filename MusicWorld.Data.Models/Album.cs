using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicWorld.Data.Models
{
    public class Album
    {
        public Album()
        {
            Songs = new List<Song>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [MaxLength(64, ErrorMessage = "The name must be up to 64 characters")]
        public string Name { get; set; }
        [DisplayName("Artist: ")]
        public string ArtistId { get; set; }

        public Artist Artist { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}
