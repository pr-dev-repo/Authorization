using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OIDCIdentityProvider
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string labelText = Request.IsAuthenticated ? "Some hidden resource only Authenticated Users can see"
                : "You are not authenticated!";

            var label = new Label
            {
                Text = labelText
            };

            var mainContent = (ContentPlaceHolder)Page.Form.FindControl("MainContent");
            mainContent.Controls.Add(label);

        }
    }
}