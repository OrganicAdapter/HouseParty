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

        protected override DriverResult Editor(HousePartyUserPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_HousePartyUser_Edit", () => shapeHelper.EditorTemplate(
                TemplateName: "Parts/HousePartyUser",
                Model: part,
                Prefix: Prefix));
        }

        protected override DriverResult Editor(HousePartyUserPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}