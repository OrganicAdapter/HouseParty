using Orchard.ContentManagement.Drivers;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Drivers
{
    public class EditHousePartyUserPartDriver : ContentPartDriver<EditHousePartyUserPart>
    {
        protected override string Prefix
        {
            get
            {
                return "SoftIT.HouseParty.Models.EditHousePartyUserPart";
            }
        }

        protected override DriverResult Display(EditHousePartyUserPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_EditHousePartyUser", () => shapeHelper.Parts_EditHousePartyUser());
        }
    }
}