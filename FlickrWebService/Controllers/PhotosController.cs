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

        [HttpGet]
        public ActionResult<List<Photo>> GetBis() =>
            _photoService.Get();

        //[HttpGet("photoSearch")]
        //public ActionResult<Photo> GetOnePhoto()
        //{
        //    _photoService.GetSearch();            
        //    return null;
        //}

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