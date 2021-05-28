using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace WebAPI.Controllers
{
    [AuthenticationFilter]
    public class GalleryController : ApiController
    {
        Utility utility = new Utility();
        // GET: Gallery
        [Route("api/Gallery/GetPhotos")]
       [HttpGet]
       public List<string> GetPhotos()
        {
            List<string> photos = new List<string>();
            photos = utility.GetPhotos();
            return photos;
        }
    }
}