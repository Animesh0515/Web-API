
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
                    loginRequest = loginrequestlst.Where(x => x.Username == value.Username && x.Password == Base64Encode(value.Password)).FirstOrDefault();

                if (loginRequest != null)
                {
                    WebApiApplication.User_Id= loginRequest.User_ID;
                    validatedLogin.validate = true;
                    //bool Result = validatedLogin.validate;
                    //return Request.CreateResponse(HttpStatusCode.OK, validatedLogin.validate);
                    //return result;
                    return JsonConvert.SerializeObject(new ValidatedLogin
                    {
                        validate = true,
                        token=TokenGenerator.GenerateToken(value.Username)
                    }) ;
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

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }


        [AuthenticationFilter]
        [Route("api/Account/ChangeCredentail")]
        [HttpPost]
        public string ChangeCredentail(UserCredentail credentail)
        {
            ChangeCredentialResponse response = new ChangeCredentialResponse();
            response.changed = utility.ChangeCredential(credentail);
            return JsonConvert.SerializeObject(response);
        }

    }
}
