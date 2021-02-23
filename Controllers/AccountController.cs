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


namespace WebAPI.Controllers
{
    public class AccountController : ApiController
    {
        [Route("api/Account/")]
        [HttpPost]
        public string login([FromBody] Account value)
        {
            ValidatedLogin validatedLogin = new ValidatedLogin();
            validatedLogin.validate = false;
            //bool Validate = true;
            //DataTable dt = new DataTable();
             Utility utility = new Utility();
            List<Account> lstaccount = utility.login();
            if (lstaccount != null)
            {
                Account account = new Account();
                if (value != null)
                    account = lstaccount.Where(x => x.Username == value.Username && x.Password == value.Password).FirstOrDefault();
                
                if (account != null)
                {

                    validatedLogin.validate = true;
                    
                    //return result;
                   return JsonConvert.SerializeObject(validatedLogin);
                    // result = result.Trim();
                    
                   
                    //dt.DataSource=
                }
                else
                {
                    //return "Not OK";
                   // Validate = false;
                    return JsonConvert.SerializeObject(validatedLogin);
                    //Validate = false;
                    //return Validate;

                }
            }
            else
            {
                //return "Not OK";
               // Validate = false;
                return JsonConvert.SerializeObject(validatedLogin);
                //return false;
            }
        }
        
    }
    
}
