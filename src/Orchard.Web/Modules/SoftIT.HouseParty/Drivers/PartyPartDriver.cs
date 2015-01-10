using Orchard.ContentManagement.Drivers;
using SoftIT.HouseParty.Constants;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Drivers
{
    public class PartyPartDriver : ContentPartDriver<PartyPart>
    {
        protected override string Prefix
        {
            get
            {
                return "SoftIT.HouseParty.Models.PartyPart";
            }
        }

        protected override DriverResult Display(PartyPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Party", () => shapeHelper.Parts_Party());
        }

        protected override DriverResult Editor(PartyPart part, dynamic shapeHelper)
        {
            var user = part.Organizer.ContentItem.Get(typeof(HousePartyUserPart)) as HousePartyUserPart;

            if (string.IsNullOrWhiteSpace(part.Country))
                part.Country = user.Country;

            if (string.IsNullOrWhiteSpace(part.State))
                part.State = user.State;

            if (string.IsNullOrWhiteSpace(part.City))
                part.City = user.City;

            if (string.IsNullOrWhiteSpace(part.Address))
                part.Address = user.Address;

            return ContentShape("Parts_Party_Edit", () => shapeHelper.EditorTemplate(
                TemplateName: "Parts/Party",
                Model: part,
                Prefix: Prefix));
        }

        protected override DriverResult Editor(PartyPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}