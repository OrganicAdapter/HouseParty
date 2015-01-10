using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Constants
{
    public static class PartyConstants
    {
        public static IEnumerable<string> PublicTypes = new[]
        {
            "Public",
            "Restricted",
            "Private"
        };

        public static IEnumerable<string> SupplyTypes = new[]
        {
            "Fix",
            "Checklist",
            "Ordered",
            "Self-service"
        };
    }
}