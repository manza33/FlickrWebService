using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using static FlickrWebService.Models.FlickrDatabaseSettings;
using FlickrWebService.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Net;
using System.IO;
using static FlickrWebService.Models.JsonPhotos;
using MongoDB.Bson.IO;
using System.Text.Json;
using jsonPhoto = FlickrWebService.Models.JsonPhotos.Photo;

namespace FlickrWebService.Services
{
    public class PhotoService
    {
        private readonly IMongoCollection<jsonPhoto> _photo;
        private readonly ILogger<PhotoService> _logger;

        public PhotoService(Models.FlickrDatabaseSettings settings, ILogger<PhotoService> logger)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _logger = logger;
            _photo = database.GetCollection<jsonPhoto>(settings.PhotosCollectionName);
        }

        public List<jsonPhoto> GetAllPhotos() =>
            _photo.Find(photo => true).ToList();

        public jsonPhoto GetById(string id) =>
            _photo.Find<jsonPhoto>(photo => photo.id == id).FirstOrDefault();

        public List<string> GetAllUrls()
        {
            var listUrls = new List<string>();
            var listPhotos =_photo.Find(book => true).ToList();

            foreach (var photo in listPhotos)
            {
                listUrls.Add($"https://farm{photo.farm}.staticflickr.com/{photo.server}/{photo.id}_{photo.secret}.jpg");
            }
            return listUrls;
        }

        public jsonPhoto CreateOne(jsonPhoto photo)
        {
            _photo.InsertOne(photo);
            return photo;
        }

        public string GetJson(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public Rootobject CreateManyByTag(string tag)
        {
            var url = $"https://www.flickr.com/services/rest/?method=flickr.photos.search&api_key=8cdc0ed72eb971dfca9b3d3edcdfe764&tags={tag}&format=json&nojsoncallback=1";
            var json = GetJson(url);
            Rootobject ListPhotos = JsonSerializer.Deserialize<Rootobject>(json);

            foreach (var photo in ListPhotos.photos.photo)
            {
                _photo.InsertOne(photo);
            }

            return ListPhotos;
        }
    }
    //public Rootobject CreateMany(string uri)
    //{
    //    var json = GetJson(uri);
    //    Rootobject ListPhotos = JsonSerializer.Deserialize<Rootobject>(json);

    //    foreach (var photo in ListPhotos.photos.photo)
    //    {
    //        _photo.InsertOne(photo);
    //    }

    //    return ListPhotos;
    //}

    //public jsonPhoto CreateBis()
    //{
    //    var photo = new jsonPhoto
    //    (
    //        id: "50117633951",
    //        owner: "134761878@N07",
    //        secret: "f5c26f06d9",
    //        server: "65535",
    //        farm: 66,
    //        title: "camponotus pennsylvanicus",
    //        ispublic: 1,
    //        isfriend: 0,
    //        isfamily: 0
    //    );
    //    _photo.InsertOne(photo);
    //    return photo;
    //}
}

//    public void Update(string id, Photo photoIn) =>
//        _photo.ReplaceOne(photo => photo.Id == id, photoIn);

//    public void Remove(Photo photoIn) =>
//        _photo.DeleteOne(photo => photo.Id == photoIn.Id);

//    public void Remove(string id) =>
//        _photo.DeleteOne(photo => photo.Id == id);


