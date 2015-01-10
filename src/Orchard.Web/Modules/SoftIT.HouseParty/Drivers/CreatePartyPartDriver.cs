using Orchard.ContentManagement.Drivers;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Drivers
{
    public class CreatePartyPartDriver : ContentPartDriver<CreatePartyPart>
    {
        protected override string Prefix
        {
            get
            {
                return "SoftIT.HouseParty.Models.CreatePartyPart";
            }
        }

        protected override DriverResult Display(CreatePartyPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_CreateParty", () => shapeHelper.Parts_CreateParty());
        }
    }
}