using Orchard.ContentManagement.Drivers;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Drivers
{
    public class NewPartyWidgetPartDriver : ContentPartDriver<NewPartyWidgetPart>
    {
        protected override string Prefix
        {
            get
            {
                return "SoftIT.HouseParty.Models.NewPartyWidgetPart";
            }
        }

        protected override DriverResult Display(NewPartyWidgetPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_NewPartyWidget", () => shapeHelper.Parts_NewPartyWidget());
        }
    }
}