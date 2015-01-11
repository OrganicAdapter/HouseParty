using Orchard;
using Orchard.ContentManagement.Drivers;
using Orchard.Core.Common.Models;
using Orchard.Localization;
using SoftIT.ExtendedUsers.Models;
using SoftIT.HouseParty.Constants;
using SoftIT.HouseParty.Models;
using SoftIT.HouseParty.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Drivers
{
    public class PartyPartDriver : ContentPartDriver<PartyPart>
    {
        private readonly IHousePartyUserMoneyService _housePartyUserMoneyService;
        private readonly IOrchardServices _orchardServices;

        protected override string Prefix
        {
            get
            {
                return "SoftIT.HouseParty.Models.PartyPart";
            }
        }

        public Localizer T { get; set; }

        public PartyPartDriver(IHousePartyUserMoneyService housePartyUserMoneyService, IOrchardServices orchardServices)
        {
            _housePartyUserMoneyService = housePartyUserMoneyService;
            _orchardServices = orchardServices;

            T = NullLocalizer.Instance;
        }

        protected override DriverResult Display(PartyPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Party", () => shapeHelper.Parts_Party());
        }

        protected override DriverResult Editor(PartyPart part, dynamic shapeHelper)
        {
            var commonPart = part.ContentItem.Get(typeof(CommonPart)) as CommonPart;
            var owner = commonPart.Owner.ContentItem.Get(typeof(ExtendedUserPart)) as ExtendedUserPart;

            if (!string.IsNullOrWhiteSpace(owner.Country))
            {
                part.Country = owner.Country;
            }
            if (!string.IsNullOrWhiteSpace(owner.State))
            {
                part.State = owner.State;
            }
            if (!string.IsNullOrWhiteSpace(owner.City))
            {
                part.City = owner.City;
            }
            if (!string.IsNullOrWhiteSpace(owner.Address))
            {
                part.Address = owner.Address;
            }

            return ContentShape("Parts_Party_Edit", () => shapeHelper.EditorTemplate(
                TemplateName: "Parts/Party",
                Model: part,
                Prefix: Prefix));
        }

        protected override DriverResult Editor(PartyPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            if (part.Income < 0)
                updater.AddModelError("InvalidIncomeValue", T("Income must be larger than or equal to 0."));

            var housePartyUser = _orchardServices.WorkContext.CurrentUser.ContentItem.Get(typeof(HousePartyUserPart)) as HousePartyUserPart;

            var summedMoney = _housePartyUserMoneyService.GetAllPrice(part);

            if (summedMoney > housePartyUser.Money)
                updater.AddModelError("NotEnoughMoney", T("You don't have enough PP to organize this party."));
            else
                housePartyUser.Money -= summedMoney;

            return Editor(part, shapeHelper);
        }
    }
}