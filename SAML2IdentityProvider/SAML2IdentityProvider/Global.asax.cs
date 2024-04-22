using Saml;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace SAML2IdentityProvider
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.HttpMethod == WebRequestMethods.Http.Post && Request?.Form["SAMLResponse"] != null)
            {
                SAMLConsume(Request?.Form["SAMLResponse"]); // begin SAML assertion
            }
        }
        /// <summary>
        /// Validate SAML 2.0 assertion
        /// </summary>
        /// <param name="token">SAML Response from IDP</param>
        private void SAMLConsume(string token)
        {
            // get the path to the certificate file
            string certFilePath = Server.MapPath(ConfigurationManager.AppSettings["SAMLCertLocation"]);
            // read the certificate from the file
            string samlCertificate = File.ReadAllText(certFilePath);
            // we start the assertion ...
            Saml.Response samlResponse = new Response(samlCertificate, token);

            if (samlResponse.IsValid())
            {
                string username = string.Empty, email, firstname, lastname;
                try
                {
                    username = samlResponse.GetNameID();
                    email = samlResponse.GetEmail();
                    firstname = samlResponse.GetFirstName();
                    lastname = samlResponse.GetLastName();
                }
                catch (Exception ex)
                {
                    //insert error handling code
                    //no, really, please do
                }

                //user has been authenticated, put your code here, like set a cookie or something...
                FormsAuthentication.SetAuthCookie(username, false);
                Response.Redirect("~/About.aspx");
            }
            else
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }
    }
}