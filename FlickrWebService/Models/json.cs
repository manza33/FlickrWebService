using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlickrWebService.Models
{
    public class json
    {
        public string TextJson { get; set; } =
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
    }
}
