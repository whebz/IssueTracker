using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
namespace IssueTracker.Web.Security
{
    public class CustomPrincipal : Model.ViewModel.AccountViewModel, IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (AccountTypeId == role)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }
        public string Role { get; set; }
    }

    public class CustomPrincipalSerializeModel : Model.ViewModel.AccountViewModel
    {
        public string Role { get; set; }
    }
}