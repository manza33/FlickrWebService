using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlickrWebService.Models
{
    public class JsonPhotos
    {

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
            public string id { get; set; }
            public string owner { get; set; }
            public string secret { get; set; }
            public string server { get; set; }
            public int farm { get; set; }
            public string title { get; set; }
            public int ispublic { get; set; }
            public int isfriend { get; set; }
            public int isfamily { get; set; }
        }

    }
}
