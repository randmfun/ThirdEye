using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient.Account
{
    public partial class Register : System.Web.UI.Page
    {
        string[] rolesArray;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind roles to ListBox.
                var RolesListBox = RegisterUserWizardStep.ContentTemplateContainer.FindControl("RolesListBox") as ListBox;

                //rolesArray = Roles.GetAllRoles();
                //RolesListBox.DataSource = rolesArray;
                //RolesListBox.DataBind();
            }
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            var homeTown = RegisterUserWizardStep.ContentTemplateContainer.FindControl("HomeTown") as TextBox;
            var homepageUrl = RegisterUserWizardStep.ContentTemplateContainer.FindControl("HomepageUrl") as TextBox;
            var signature = RegisterUserWizardStep.ContentTemplateContainer.FindControl("Signature") as TextBox;
            /*
                  ' Update the UserProfiles record for this user           '
             * Get the UserId of the just-added user          
             * Dim newUser As MembershipUser = Membership.GetUser(NewUserWizard.UserName)          
             * Dim newUserId As Guid = CType(newUser.ProviderUserKey, Guid)           
             * ' Insert a new record into UserProfiles          
             * Dim connectionString As String = ConfigurationManager.ConnectionStrings("SecurityTutorialsConnectionString").ConnectionString           
             * Dim updateSql As String = "UPDATE UserProfiles SET HomeTown = @HomeTown, HomepageUrl= @HomepageUrl, Signature = @Signature WHERE UserId = @UserId" 
             * 
             * Using myConnection As New SqlConnection(connectionString)               
                 * myConnection.Open()               
                 * Dim myCommand As New SqlCommand(updateSql, myConnection)                
                 * myCommand.Parameters.AddWithValue("@HomeTown", HomeTown.Text.Trim())                
                 * myCommand.Parameters.AddWithValue("@HomepageUrl", HomepageUrl.Text.Trim())               
                 * myCommand.Parameters.AddWithValue("@Signature", Signature.Text.Trim())                
                 * myCommand.Parameters.AddWithValue("@UserId", newUserId)                
                 * myCommand.ExecuteNonQuery()                
                 * myConnection.Close()           
             * 
             * End Using
             */
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

    }
}
