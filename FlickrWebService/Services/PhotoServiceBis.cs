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

namespace FlickrWebService.Services
{
    public class PhotoServiceBis
    {
        private readonly IMongoCollection<BsonDocument> _photo;
        private readonly ILogger<PhotoServiceBis> _logger;

        public PhotoServiceBis(Models.FlickrDatabaseSettings settings, ILogger<PhotoServiceBis> logger)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _logger = logger;

            _photo = database.GetCollection<BsonDocument>(settings.PhotosCollectionName);

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
        }

        public async void GetSearch()
        {

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
        }
    }
}
