using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlickrWebService.Models
{
    public class JsonPhotos
    {
        //TODO diviser en 3 classes
        // ASTUSE JSON => Classe :
        //Copier les data de ton fichier JSON et les coller en tant que nouvelle classe de sérialisation sous Visual Studio
        //(Edition > Collage spécial > Coller le code JSON en tant que classe).
        //Source : https://openclassrooms.com/forum/sujet/recuperer-les-donnees-d-un-fichier-json
        public class Rootobject
        {
            public Photos photos { get; set; }
            public string stat { get; set; }
        }

        public class Photos
        {
            public int page { get; set; }
            public int pages { get; set; }
            public int perpage { get; set; }
            public string total { get; set; }
            public Photo[] photo { get; set; }
        }

        public class Photo
        {
            public Photo(string id, string owner, string secret, string server, int farm, string title, int ispublic, int isfriend, int isfamily)
            {
                this.Id = id;
                this.owner = owner;
                this.secret = secret;
                this.server = server;
                this.farm = farm;
                this.title = title;
                this.ispublic = ispublic;
                this.isfriend = isfriend;
                this.isfamily = isfamily;
            }

            public Photo()
            {
            }

            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string _id { get; set; }
            [BsonElement("Id")]
            public string Id { get; set; }
            [BsonElement("Owner")]
            public string owner { get; set; }
            [BsonElement("Secret")]
            public string secret { get; set; }
            [BsonElement("Server")]
            public string server { get; set; }
            [BsonElement("Farm")]
            public int farm { get; set; }
            [BsonElement("Title")]
            public string title { get; set; }
            [BsonElement("Ispublic")]
            public int ispublic { get; set; }
            [BsonElement("Isfriend")]
            public int isfriend { get; set; }
            [BsonElement("Isfamily")]
            public int isfamily { get; set; }
        }
    }
}
