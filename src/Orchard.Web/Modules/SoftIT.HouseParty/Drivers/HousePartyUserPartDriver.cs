using Orchard.ContentManagement.Drivers;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Drivers
{
    public class HousePartyUserPartDriver : ContentPartDriver<HousePartyUserPart>
    {
        protected override string Prefix
        {
            get
            {
                return "SoftIT.HouseParty.Models.HousePartyUserPart";
            }
        }

        protected override DriverResult Display(HousePartyUserPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_HousePartyUser", () => shapeHelper.Parts_HousePartyUser());
        }
    }
}