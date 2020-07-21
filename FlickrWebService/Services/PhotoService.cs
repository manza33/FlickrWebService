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

namespace FlickrWebService.Services
{
    public class PhotoService
    {
        //private readonly IMongoCollection<BsonDocument> _photobis;
        private readonly IMongoCollection<Photo> _photo;

        private readonly ILogger<PhotoService> _logger;


        public PhotoService(Models.FlickrDatabaseSettings settings, ILogger<PhotoService> logger)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _logger = logger;

            //_photobis = database.GetCollection<BsonDocument>(settings.PhotosCollectionName);
            _photo = database.GetCollection<Photo>(settings.PhotosCollectionName);

        }

        //public async void Get()
        //{
        //    using (IAsyncCursor<BsonDocument> cursor = await _photo.FindAsync(new BsonDocument()))
        //    {                
        //        while (await cursor.MoveNextAsync())
        //        {
        //            IEnumerable<BsonDocument> batch = cursor.Current;
        //            foreach (BsonDocument document in batch)
        //            {
        //                _logger.LogInformation($"Doc : {document}");
        //            }
        //        }
        //    }
        //    //var list = _photo.Find(photo => true).ToJson();
        //    //_logger.LogInformation(list);
        //    //return list;
        //}

        public List<Photo> Get() =>
            _photo.Find(book => true).ToList();


        //public async void GetSearch() {

        //    using (IAsyncCursor<BsonDocument> cursor = await _photoBis.FindAsync(new BsonDocument("Owner", "28043554@N06")))
        //    {
        //        while (await cursor.MoveNextAsync())
        //        {
        //            IEnumerable<BsonDocument> batch = cursor.Current;
        //            foreach (BsonDocument document in batch)
        //            {
        //                _logger.LogInformation($"Doc : {document}");
        //            }
        //        }
        //    }

        //}

        public Photo GetBis(string id) =>
            _photo.Find<Photo>(photo => photo.Id == id).FirstOrDefault();


        //public async void Create()
        //{
        //    var document = new BsonDocument
        //    {
        //      {"firstname", BsonValue.Create("Peter")},
        //      {"lastname", new BsonString("Mbanugo")},
        //      { "subjects", new BsonArray(new[] {"English", "Mathematics", "Physics"}) },
        //      { "class", "JSS 3" },
        //      { "age", 45}
        //    };
        //    await _photobis.InsertOneAsync(document);

        //    ///return null;
        //}


        public Photo CreateBis()
        {
            var photo = new Photo
            (id: "50117633951", owner: "134761878@N07", secret: "f5c26f06d9", server: "65535", farm: 66, title: "camponotus pennsylvanicus", ispublic: 1, isfriend: 0, isfamily: 0);
            _photo.InsertOne(photo);
            return photo;
        }

        //    public void Update(string id, Photo photoIn) =>
        //        _photo.ReplaceOne(photo => photo.Id == id, photoIn);

        //    public void Remove(Photo photoIn) =>
        //        _photo.DeleteOne(photo => photo.Id == photoIn.Id);

        //    public void Remove(string id) =>
        //        _photo.DeleteOne(photo => photo.Id == id);


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


    }
}
