using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicWorld.Data;
using MusicWorld.Services.Contracts;
using MusicWorld.Services.MusicWorld.Services.Models;
using MusicWorld.View.Models;

namespace MusicWorld.Controllers
{
    public class ArtistsController : Controller
    {
        private IArtistService artistService;
        private ICloudinaryService cloudinaryService;
        private MusicWorldContext context;
        public ArtistsController(IArtistService artistService, ICloudinaryService cloudinaryService, MusicWorldContext context)
        {
            this.artistService = artistService;
            this.cloudinaryService = cloudinaryService;
            this.context = context;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateArtistViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateArtistViewModel artistViewModel)
        {
            string pictureUrl =await this.cloudinaryService.UploadPictureAsync(artistViewModel.Image, artistViewModel.Name);

            ArtistServiceModel artist= new ArtistServiceModel
            {
                
                Name = artistViewModel.Name,
                Description = artistViewModel.Description,
                Image = pictureUrl
            };

            artistService.Create(artist);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            return View(context.Artists.ToList());
        }

        public IActionResult Details(string id)
        {
            var artist = artistService.GetArtistsById(id);
            //TO DO: PUT A SONGS 
            return View(artist);
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View(new CreateArtistViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Update(CreateArtistViewModel serviceModel)
        { 
            string pictureUrl = await this.cloudinaryService.UploadPictureAsync(serviceModel.Image, serviceModel.Name);

            var id = context.Artists.FirstOrDefault(x => x.Name.ToLower() == serviceModel.Name.ToLower());
            if (id != null)
            {
                ArtistServiceModel artist = new ArtistServiceModel
                {
                    Name = serviceModel.Name,
                    Description = serviceModel.Description,
                    Image = pictureUrl
                };

                artistService.Update(artist, id.Id);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound();//RedirectToAction("All", "Artists");
            }
        }

        public IActionResult Delete(string id)
        {
            var isTrue = artistService.Delete(id);

            if (isTrue==true)
            {
                return RedirectToAction("Index", "Home");
            }
            else return RedirectToAction("All", "Artists");
        }

    }
} 