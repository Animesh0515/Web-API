using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace WebAPI
{
    //     Defines a filter that performs authentication.
    public class AuthenticationFilter : AuthorizeAttribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get { return false; } }
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            string authParameter = "";
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;
            string[] UserToken = null;
            if(authorization==null)
            {
                context.ErrorResult = new AuthenticationFaliureResult("Missing Authorization Header", request);
                return;
            }
            if( authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFaliureResult("Invalid Authorization Schema", request);
                return;
            }
            UserToken = authorization.Parameter.Split(':');
            string Token = UserToken[0];
            string Username = UserToken[1];

            if (String.IsNullOrEmpty(Token))
            {
                context.ErrorResult = new AuthenticationFaliureResult("Missing Token", request);
                return;
            }
            string validUserName = TokenGenerator.ValidateToken(Token);
            if(Username != validUserName)
            {
                context.ErrorResult = new AuthenticationFaliureResult("Ivnvalid Token for User ", request);
                return;
            }
            context.Principal = TokenGenerator.GetPrincipal(Token);

        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);
            if(result.StatusCode==HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", "realm-localhost"));
            }
            context.Result = new ResponseMessageResult(result);
        }
        public class AuthenticationFaliureResult : IHttpActionResult
        {
            public string ReasonPhrase;
            public HttpRequestMessage Request { get; set; }

            public AuthenticationFaliureResult(string reasonPhrase, HttpRequestMessage request)
            {
                ReasonPhrase = reasonPhrase;
                Request = request;
            }
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute());
            }
            public  HttpResponseMessage Execute()
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                responseMessage.RequestMessage = Request;
                responseMessage.ReasonPhrase = ReasonPhrase;
                return responseMessage;
            }
        }
    }
}