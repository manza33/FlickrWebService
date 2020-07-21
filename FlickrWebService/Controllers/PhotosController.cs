using FlickrWebService.Models;
using FlickrWebService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FlickrWebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly PhotoService _photoService;

        public PhotosController(PhotoService photoService)
        {
            _photoService = photoService;
        }

        //[HttpGet]
        //public ActionResult<string> Get()
        //{
        //    _photoService.Get();
        //    return null;
        //}

        //photos
        [HttpGet]
        public ActionResult<List<Photo>> GetBis() =>
            _photoService.Get();

        //photos/uri
        [HttpGet("uri")]
        public ActionResult<string> GetUri() =>
            _photoService.GetJson("https://www.flickr.com/services/rest/?method=flickr.photos.search&api_key=8cdc0ed72eb971dfca9b3d3edcdfe764&tags=fourmi&format=json&nojsoncallback=1");

        //[HttpGet("photoSearch")]
        //public ActionResult<Photo> GetOnePhoto()
        //{
        //    _photoService.GetSearch();            
        //    return null;
        //}

        //photos?id=50125876112
        [HttpGet("{id}")]
        public ActionResult<Photo> Get(string id)
        {
            var photo = _photoService.GetBis(id);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        //Ajoute une photo en dur
        //photos/photoAdd
        [HttpGet("photoAdd")]
        public ActionResult<Photo> Create()
        {
            _photoService.CreateBis();

            return null;
        }

        //[HttpPut("{id:length(24)}")]
        //public IActionResult Update(string id, Photo PhotoIn)
        //{
        //    var photo = _photoService.Get(id);

        //    if (photo == null)
        //    {
        //        return NotFound();
        //    }
        //    _photoService.Update(id, PhotoIn);
        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public IActionResult Delete(string id)
        //{
        //    var photo = _photoService.Get(id);

        //    if (photo == null)
        //    {
        //        return NotFound();
        //    }

        //    _photoService.Remove(photo.Id);

        //    return NoContent();
        //}
    }
}