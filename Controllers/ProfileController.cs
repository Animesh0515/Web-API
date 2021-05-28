using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using static WebAPI.Models.ResponseModel;

namespace WebAPI.Controllers
{
    [AuthenticationFilter]
    public class ProfileController : ApiController
    {
        Utility utility = new Utility();


        [Route("api/Profile/UpdateImage")]
        [HttpPost]
        public string UpdateImage([FromBody] string Imageurl)
        {
            bool update = utility.UpdateImage(Imageurl);
            return JsonConvert.SerializeObject(update);
        }



        [Route("api/Profile/GetUserDetails")]
        [HttpGet]
        public string GetUserDetails()
        {
            UserProfileModel userResponse = utility.GetUserDetails(WebApiApplication.User_Id);
            if (userResponse != null)
            {
                return JsonConvert.SerializeObject(userResponse);
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }

        }

        [Route("api/Profile/UpdateUserDetails")]
        [HttpPost]
        public string UpdateUserDetails([FromBody] UserProfileModel userProfile)
        {
            ProfileUpdateResponse updateResponse = new ProfileUpdateResponse();

            if (userProfile != null)
            {
                updateResponse.updated = utility.UpdateUserDetails(userProfile);
                return JsonConvert.SerializeObject(updateResponse);

            }
            else
            {
                updateResponse.updated = false;
                return JsonConvert.SerializeObject(updateResponse);
            }
        }
    }
}