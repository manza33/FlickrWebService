using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using static FlickrWebService.Models.FlickrDatabaseSettings;
using FlickrWebService.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;

namespace FlickrWebService.Services
{
    public class PhotoService
    {
        private readonly IMongoCollection<BsonDocument> _photo;
        private readonly ILogger<PhotoService> _logger;


        public PhotoService(Models.FlickrDatabaseSettings settings, ILogger<PhotoService> logger)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _logger = logger;

            _photo = database.GetCollection<BsonDocument>(settings.PhotosCollectionName);
            //_logger.LogInformation($"photo : {_photo}");

        }

        public async void Get()
        {
            using (IAsyncCursor<BsonDocument> cursor = await _photo.FindAsync(new BsonDocument()))
            {                
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<BsonDocument> batch = cursor.Current;
                    foreach (BsonDocument document in batch)
                    {
                        _logger.LogInformation($"Doc : {document}");
                    }
                }
            }
            //var list = _photo.Find(photo => true).ToJson();
            //_logger.LogInformation(list);
            //return list;
        }

        public async void GetSearch() {


            using (IAsyncCursor<BsonDocument> cursor = await _photo.FindAsync(new BsonDocument("Owner", "28043554@N06")))
            {
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<BsonDocument> batch = cursor.Current;
                    foreach (BsonDocument document in batch)
                    {
                        _logger.LogInformation($"Doc : {document}");
                    }
                }
            }

        }

        public async void Create()
        {
            var document = new BsonDocument
            {
              {"firstname", BsonValue.Create("Peter")},
              {"lastname", new BsonString("Mbanugo")},
              { "subjects", new BsonArray(new[] {"English", "Mathematics", "Physics"}) },
              { "class", "JSS 3" },
              { "age", 45}
            };
            await _photo.InsertOneAsync(document);

            ///return null;
        }

        //    public void Update(string id, Photo photoIn) =>
        //        _photo.ReplaceOne(photo => photo.Id == id, photoIn);

        //    public void Remove(Photo photoIn) =>
        //        _photo.DeleteOne(photo => photo.Id == photoIn.Id);

        //    public void Remove(string id) =>
        //        _photo.DeleteOne(photo => photo.Id == id);
    }
}
