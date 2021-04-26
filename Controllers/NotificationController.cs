using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class NotificationController : ApiController
    {
        Utility utility = new Utility();

        [Route("api/Notification/GetNotification")]
        [HttpGet]
        public string GetNotification()
        {
            List<NotificationModel> notifications = new List<NotificationModel>();
            notifications = utility.getNotification();
            return JsonConvert.SerializeObject(notifications);
        }
    }
}