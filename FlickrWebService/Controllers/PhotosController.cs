using FlickrWebService.Models;
using FlickrWebService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static FlickrWebService.Models.JsonPhotos;
using jsonPhoto = FlickrWebService.Models.JsonPhotos.Photo;


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
        public ActionResult<List<jsonPhoto>> Get() =>
            _photoService.GetAllPhotos();

        // photos/urls
        [HttpGet("urls")]
        public ActionResult<List<string>> GetByUrl() =>
            _photoService.GetAllUrls();

        // photos/50125876112
        [HttpGet("{id}")]
        public ActionResult<jsonPhoto> Get(string id)
        {
            var photo = _photoService.GetById(id);

            if (photo == null)
            {
                return NotFound();
            }
            return photo;
        }

        // photos/addPhoto
        [HttpPost("addPhoto")]
        public ActionResult<jsonPhoto> CreateOnePhoto([FromBody] jsonPhoto photo)
        {
            _photoService.CreateOne(photo);
            return photo;
        }

        // photos/addFromJson/panda
        [HttpGet("addFromJson/{tag}")]
        public ActionResult<Rootobject> CreateManyPhotos(string tag)
        {
            var listPhotosCreated = _photoService.CreateManyByTag(tag);

            if(listPhotosCreated == null){
                return BadRequest("Recherche déjà effectuée");
            }

            return listPhotosCreated;
        }
    }
}

//// photos/addFromJson
//[HttpGet("addFromJson")]
//public ActionResult<Rootobject> CreateManyPhotos() =>
//    _photoService.CreateMany("https://www.flickr.com/services/rest/?method=flickr.photos.search&api_key=8cdc0ed72eb971dfca9b3d3edcdfe764&tags=fourmi&format=json&nojsoncallback=1");

// Ajoute une photo en dur
// photos/photoAdd
//[HttpGet("AddDur")]
//public ActionResult<jsonPhoto> Create()
//{
//    _photoService.CreateBis();
//    return null;
//}

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