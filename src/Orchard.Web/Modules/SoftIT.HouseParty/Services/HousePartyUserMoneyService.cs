using Orchard;
using SoftIT.HouseParty.Constants;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Services
{
    public interface IHousePartyUserMoneyService : IDependency
    {
        int GetAllPrice(PartyPart party);
    }

    public class HousePartyUserMoneyService : IHousePartyUserMoneyService
    {
        public int GetAllPrice(PartyPart party)
        {
            var summedMoney = ServicePrices.Party;

            if (party.Income > 0)
                summedMoney += ServicePrices.IncomePrice;
            if (!party.Visibility)
                summedMoney += ServicePrices.PrivatePartyPrice;
            if (party.PublicType.Equals("Restricted"))
                summedMoney += ServicePrices.RestrictedPartyPrice;
            if (party.PublicType.Equals("Exclusive"))
                summedMoney += ServicePrices.ExclusivePartyPrice;
            if (party.FoodType.Equals("Checklist"))
                summedMoney += ServicePrices.ChecklistPrice;
            if (party.FoodType.Equals("Ordered"))
                summedMoney += ServicePrices.OrderedPrice;
            if (party.FoodType.Equals("Self-service"))
                summedMoney += ServicePrices.SelfServicePrice;
            if (party.DrinkType.Equals("Checklist"))
                summedMoney += ServicePrices.ChecklistPrice;
            if (party.DrinkType.Equals("Ordered"))
                summedMoney += ServicePrices.OrderedPrice;
            if (party.DrinkType.Equals("Self-service"))
                summedMoney += ServicePrices.SelfServicePrice;

            return summedMoney;
        }
    }
}