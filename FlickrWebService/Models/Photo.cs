using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace FlickrWebService.Models
{
    public class Photo
    {
        public Photo(string id, string owner, string secret, string server, int farm, string title, int ispublic, int isfriend, int isfamily)
        {
            Id = id;
            Owner = owner;
            Secret = secret;
            Server = server;
            Farm = farm;
            Title = title;
            Ispublic = ispublic;
            Isfriend = isfriend;
            Isfamily = isfamily;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string Id { get; set; }
        public string Owner { get; set; }
        public string Secret { get; set; }
        public string Server { get; set; }
        public int Farm { get; set; }
        public string Title { get; set; }
        public int Ispublic { get; set; }
        public int Isfriend { get; set; }
        public int Isfamily { get; set; }
    }
}
