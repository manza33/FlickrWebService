﻿using FlickrWebService.Models;
using FlickrWebService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static FlickrWebService.Models.JsonPhotos;

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

        // photos
        [HttpGet]
        public ActionResult<List<PhotoF>> GetBis() =>
            _photoService.Get();

        // photos/50125876112
        [HttpGet("{id}")]
        public ActionResult<PhotoF> Get(string id)
        {
            var photo = _photoService.GetBis(id);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        // photos/addPhoto
        [HttpPost("addPhoto")]
        public ActionResult<PhotoF> CreateOnePhoto([FromBody] PhotoF photo)
        {
            _photoService.CreateOne(photo);
            return photo;
        }

        // photos/addFromJson
        [HttpGet("addFromJson")]
        public ActionResult<Rootobject> CreateManyPhotos() =>
            _photoService.CreateMany("https://www.flickr.com/services/rest/?method=flickr.photos.search&api_key=8cdc0ed72eb971dfca9b3d3edcdfe764&tags=fourmi&format=json&nojsoncallback=1");

        // Ajoute une photo en dur
        // photos/photoAdd
        [HttpGet("AddDur")]
        public ActionResult<PhotoF> Create()
        {
            _photoService.CreateBis();

            return null;
        }
    }
}




//[HttpGet("photoSearch")]
//public ActionResult<Photo> GetOnePhoto()
//{
//    _photoService.GetSearch();            
//    return null;
//}


//[HttpGet]
//public ActionResult<string> Get()
//{
//    _photoService.Get();
//    return null;
//}