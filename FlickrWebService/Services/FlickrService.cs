using FlickrWebService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlickrWebService.Services
{
    public class FlickrService
    {
        private const string TextJson =
        @"photos: {
            page: 1,
            pages: 64,
            perpage: 100,
            total: '6360',
            photo: [
{
id: '50125876112',
owner: '28043554@N06',
secret: 'ea7f908cfe',
server: '65535',
farm: 66,
title: 'DSC_4208_DxO_pn - fourmi - Ant - Camponotus cruentatus',
ispublic: 1,
isfriend: 0,
isfamily: 0
},
{
id: '50117633951',
owner: '134761878@N07',
secret: 'f5c26f06d9',
server: '65535',
farm: 66,
title: 'camponotus pennsylvanicus',
ispublic: 1,
isfriend: 0,
isfamily: 0
},
            {
                id: '50113694107',
                owner: '52495017@N00',
                secret: 'e59e526c8c',
                server: '65535',
                farm: 66,
                title: 'Which should yield to the other?',
                ispublic: 1,
                isfriend: 0,
                isfamily: 0
            }
        }";
        private readonly ILogger<FlickrService> _logger;

        public FlickrService(ILogger<FlickrService> logger)
        {
            _logger = logger;
        }

        public FlickrService()
        {
        }


        async Task<PhotoF> GetPhotoAsync(string ApiUrl)
        {
            //string ApiUrl = "https://www.flickr.com/services/rest/?method=flickr.photos.search&api_key=8cdc0ed72eb971dfca9b3d3edcdfe764&tags=fourmi&format=json&nojsoncallback=1";

            HttpClient client = new HttpClient();
            PhotoF photo = null;
            HttpResponseMessage response = await client.GetAsync(ApiUrl);
            if (response.IsSuccessStatusCode)
            {
                photo = await response.Content.ReadAsAsync<PhotoF>();
            }
            return photo;
        }

        //public void GetPictures()
        //{
        //    Flickr flickr = new Flickr("3d7fc422f071ad835019b6191897139f");
        //    //flickr.ApiKey = "3d7fc422f071ad835019b6191897139f";

        //    var options = new PhotoSearchOptions { Tags = "colorful", PerPage = 20, Page = 1 };
        //    PhotoCollection photos = flickr.PhotosSearch(options);

        //    foreach (Photo photo in photos)
        //    {
        //        _logger.LogInformation($"Photo {photo.PhotoId} has title {photo.Title}");
        //        //Console.WriteLine($"Photo {photo.PhotoId} has title {photo.Title}");
        //    }
        //}

    }
}
