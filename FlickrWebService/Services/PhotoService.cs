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

        public List<jsonPhoto> Get() =>
            _photo.Find(book => true).ToList();

        public jsonPhoto GetBis(string id) =>
            _photo.Find<jsonPhoto>(photo => photo.Id == id).FirstOrDefault();

        public jsonPhoto CreateBis()
        {

            var photo = new jsonPhoto
            (
                id: "50117633951",
                owner: "134761878@N07",
                secret: "f5c26f06d9",
                server: "65535",
                farm: 66,
                title: "camponotus pennsylvanicus",
                ispublic: 1,
                isfriend: 0,
                isfamily: 0
            );
            _photo.InsertOne(photo);
            return photo;
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

        public Rootobject CreateMany(string uri)
        {
            var json = GetJson(uri);
            Rootobject ListPhotos = JsonSerializer.Deserialize<Rootobject>(json);

            foreach (var photo in ListPhotos.photos.photo)
            {
                var phot = new jsonPhoto
                (
                    id: photo.Id,
                    owner: photo.owner,
                    secret: photo.secret,
                    server: photo.server,
                    farm: photo.farm,
                    title: photo.title,
                    ispublic: photo.ispublic,
                    isfriend: photo.isfriend,
                    isfamily: photo.isfamily
                );

                _photo.InsertOne(phot);
            }

            return ListPhotos;
        }
    }
}

//    public void Update(string id, Photo photoIn) =>
//        _photo.ReplaceOne(photo => photo.Id == id, photoIn);

//    public void Remove(Photo photoIn) =>
//        _photo.DeleteOne(photo => photo.Id == photoIn.Id);

//    public void Remove(string id) =>
//        _photo.DeleteOne(photo => photo.Id == id);


