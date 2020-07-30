using System;
using System.ComponentModel.DataAnnotations;

namespace MusicWorld.InputModels
{
    public class ArtistCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
