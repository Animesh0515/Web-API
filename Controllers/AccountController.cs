
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Models;
using static WebAPI.Models.ResponseModel;

namespace WebAPI.Controllers
{
    public class AccountController : ApiController
    {
        Utility utility = new Utility();
        //public static int User_Id;


        [Route("api/Account/login")]
        [HttpPost]
        public string login([FromBody] LoginRequestModel value)
        {
            LoginRequestModel loginRequest = new LoginRequestModel();
            ValidatedLogin validatedLogin = new ValidatedLogin();
            validatedLogin.validate = false;
            //bool Validate = true;
            //DataTable dt = new DataTable();
            List<LoginRequestModel> loginrequestlst = utility.login();
            if (loginrequestlst != null)
            {
                if (value != null)
                    loginRequest = loginrequestlst.Where(x => x.Username == value.Username && x.Password == value.Password).FirstOrDefault();

                if (loginRequest != null)
                {
                    WebApiApplication.User_Id= loginRequest.User_ID;
                    validatedLogin.validate = true;
                    //bool Result = validatedLogin.validate;
                    //return Request.CreateResponse(HttpStatusCode.OK, validatedLogin.validate);
                    //return result;
                    return JsonConvert.SerializeObject(validatedLogin);
                    // result = result.Trim();


                    //dt.DataSource=
                }
                else
                {
                    //return Request.CreateResponse(HttpStatusCode.NotFound, validatedLogin.validate);

                    //return "Not OK";
                    // Validate = false;
                    return JsonConvert.SerializeObject(validatedLogin);
                    //Validate = false;
                    //return Validate;

                }
            }
            else
            {
                //return Request.CreateResponse(HttpStatusCode.NotFound, validatedLogin.validate);

                //return "Not OK";
                // Validate = false;
                return JsonConvert.SerializeObject(validatedLogin);
                //return false;
            }
        }
        [Route("api/Account/Signup")]
        [HttpPost]
        public string Signup([FromBody] UserModel value)
        {
            
            ValidateSignup validateSignup = new ValidateSignup();
             bool invalidmodel = value.GetType()
                 .GetProperties() //get all properties on object
                 .Select(pi => pi.GetValue(value)) //get value for the propery
                 .Any(x => x == null  || x=="");
            //|| Convert.ToInt64(x) == 0
            
            if (!invalidmodel)
            { 
                 validateSignup.Status=utility.Signup(value);
                return JsonConvert.SerializeObject(validateSignup);
                 
            }
            else
            {
                validateSignup.Status = 2;
                return JsonConvert.SerializeObject(validateSignup);


            }
        }

        
        

    }
}
