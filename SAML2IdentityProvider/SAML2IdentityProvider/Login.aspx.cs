using Saml;
using System;
using System.Configuration;

namespace SAML2IdentityProvider
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var samlEndpoint = ConfigurationManager.AppSettings["SamlEndpoint"];
            var appEntityId = ConfigurationManager.AppSettings["AppEntityId"];
            var assertionConsumerUrl = ConfigurationManager.AppSettings["AssertionConsumerUrl"];

            var request = new AuthRequest(appEntityId, assertionConsumerUrl);
            //now send the user to the SAML provider
            Response.Redirect(request.GetRedirectUrl(samlEndpoint));
        }
    }
}