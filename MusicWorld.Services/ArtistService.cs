using MusicWorld.Data;
using MusicWorld.Data.Models;
using MusicWorld.InputModels;
using MusicWorld.Services.Contracts;
using MusicWorld.Services.MusicWorld.Services.Models;
using System.Linq;

namespace MusicWorld.Services
{
    public class ArtistService : IArtistService
    {
        private readonly MusicWorldContext context;

        public ArtistService(MusicWorldContext context)
        {
            this.context = context;
        }

        public string Create(ArtistServiceModel artistService)
        {
            var artist = new Artist()
            {
                Name = artistService.Name,
                Image = artistService.Image,
                Description = artistService.Description
            };

            context.Artists.Add(artist);
            context.SaveChanges();

            return artist.Id;
        }

        public Artist GetArtistsById(string artistId)
        {
            return context.Artists.FirstOrDefault(x => x.Id == artistId);
        }

        public string Update(ArtistServiceModel artistService,string id)
        {
            var a = context.Artists.FirstOrDefault(x => x.Id == id);

            a.Name = artistService.Name;
                a.Image = artistService.Image;
            a.Description = artistService.Description;


            context.Artists.Update(a);
            context.SaveChanges();

            return a.Id;
        }


        public bool Delete(string id)
        {
            var artist = context.Artists.FirstOrDefault(x => x.Id == id);

            if (artist != null)
            {
                context.Artists.Remove(artist);
                context.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}
