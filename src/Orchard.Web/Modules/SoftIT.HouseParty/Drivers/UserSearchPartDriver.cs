using Orchard.ContentManagement.Drivers;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Drivers
{
    public class UserSearchPartDriver : ContentPartDriver<UserSearchPart>
    {
        protected override string Prefix
        {
            get
            {
                return "SoftIT.HouseParty.Models.UserSearchPart";
            }
        }

        protected override DriverResult Display(UserSearchPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_UserSearch", () => shapeHelper.Parts_UserSearch());
        }
    }
}