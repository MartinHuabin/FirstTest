using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Canada2DCode.Models;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Canada2DCode.DataBaseEngine;


namespace Canada2DCode.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //using (AuthRepository _repo = new AuthRepository())
            //{
            //    IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

            //    if (user == null)
            //    {
            //        context.SetError("invalid_grant", "The user name or password is incorrect.");
            //        return;
            //    }
            //}
            AuthenticationProperties properties = new AuthenticationProperties();
            using (Canada2DCodeEntities ctx = new Canada2DCodeEntities())
            {
                try
                {
                    var user = ctx.Customers.Where(t => t.Email == context.UserName && t.Password == context.Password).FirstOrDefault();
                    if (user == null)
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect.");
                        return;
                    }

                    properties = CreateProperties(new TokenData()
                    {
                        userName = user.FirstName + " " + user.LastName,
                        customerEmail = user.Email,
                        customerId = user.CustomerId
                    });
                }
                catch (Exception ex)
                {

                    var ii = ex.Message;
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;

                }
                ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                ClaimsIdentity cookiesIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);

                //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                //identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                //identity.AddClaim(new Claim(ClaimTypes.Role, user.CustomerId.ToString()));

                //identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));



                //context.Validated(identity);
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                string key = property.Key;
                switch (key)
                {
                    case ".expires":
                        key = "expires";
                        break;
                    case ".issued":
                        key = "issued";
                        break;
                }
                context.AdditionalResponseParameters.Add(key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
        public static AuthenticationProperties CreateProperties(TokenData tokenData)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", tokenData.userName },
                { "customerEmail" , tokenData.customerEmail},
                { "customerId", tokenData.customerId.ToString() }
            };
            return new AuthenticationProperties(data);
        }

    }
    public class TokenData
    {
        public string userName { get; set; }
        public string customerEmail { get; set; }
        public int customerId { get; set; }
    }
}