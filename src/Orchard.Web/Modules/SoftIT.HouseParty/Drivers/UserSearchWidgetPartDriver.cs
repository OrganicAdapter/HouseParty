using Orchard.ContentManagement.Drivers;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Drivers
{
    public class UserSearchWidgetPartDriver : ContentPartDriver<UserSearchWidgetPart>
    {
        protected override string Prefix
        {
            get
            {
                return "SoftIT.HouseParty.Models.UserSearchWidgetPart";
            }
        }

        protected override DriverResult Display(UserSearchWidgetPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_UserSearchWidget", () => shapeHelper.Parts_UserSearchWidget());
        }
    }
}