using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class CalendarController: ApiController
    {
        Utility utility = new Utility();

        [Route("api/Calendar/GetData")]
        
        [HttpPost]
        public List<CalendarDataResponseModel> GetData([FromBody]int  Weekday)
        {
            List<CalendarDataResponseModel> datalst = new List<CalendarDataResponseModel>();
            datalst = utility.GetData(Weekday);
            return datalst;
        }
    }
}