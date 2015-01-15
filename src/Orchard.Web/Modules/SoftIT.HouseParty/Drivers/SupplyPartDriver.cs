using Orchard.ContentManagement.Drivers;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Drivers
{
    public class SupplyPartDriver : ContentPartDriver<SupplyPart>
    {
        protected override string Prefix
        {
            get
            {
                return "SoftIT.HouseParty.Models.SupplyPart";
            }
        }

        protected override DriverResult Display(SupplyPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Supply", () => shapeHelper.Parts_Supply());
        }

        protected override DriverResult Editor(SupplyPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Supply_Edit", () => shapeHelper.EditorTemplate(
                TemplateName: "Parts/Supply",
                Model: part,
                Prefix: Prefix));
        }

        protected override DriverResult Editor(SupplyPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}