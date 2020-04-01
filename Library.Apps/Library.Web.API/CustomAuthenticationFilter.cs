using Library.Web.API.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
namespace Library.Web.API
{
    public class CustomAuthenticationFilter : AuthorizeAttribute
        //, IAuthenticationFilter
    {
        //public bool AllowMultiple
        //{
        //    get { return false; }
        //}

        //public async Task AuthenticateAsyn(HttpAuthenticationContext context, CancellationToken cancellationToken)
        //{
        //    string authParam = string.Empty;
        //    HttpRequestMessage req = context.Request;
        //    AuthenticationHeaderValue authorization = req.Headers.Authorization;
        //    if(authorization == null)
        //    {
        //        context.ErrorResult = new AuthenticationFailureResult(reasonPhrase: "Missing Authorization Header", req);
        //    }

        //    if(authorization.Scheme != "Bearer")
        //    {
        //        context.ErrorResult = new AuthenticationFailureResult(reasonPhrase: "Invalid Authorization Scheme", req);
        //    }

        //    if (string.IsNullOrEmpty(authorization.Parameter))
        //    {
        //        context.ErrorResult = new AuthenticationFailureResult(reasonPhrase: "Missing Token", req);
        //    }

        //    context.Principal = TokenManager.GetPrincipal(authorization.Parameter)


        //}

        //public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }

    //public class AuthenticationFailureResult : IHttpActionResult
    //{
    //    public string ReasonPhrase;
    //    public HttpRequestMessage Request { get; set; }

    //    public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage req)
    //    {
    //        ReasonPhrase = reasonPhrase;
    //        Request = req;
    //    }

    //    public Task<HttpResponseMessage> ExecuteAsyc(CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(Execute());
    //    }

    //    public HttpResponseMessage Execute()
    //    {
    //        HttpResponseMessage responeseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
    //        responeseMessage.RequestMessage = Request;
    //        responeseMessage.ReasonPhrase = ReasonPhrase;
    //        return responeseMessage;
    //    }


    //}
}