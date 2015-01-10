using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;
using Orchard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Models
{
    public class EditHousePartyUserPart : ContentPart
    {
        private readonly LazyField<HousePartyUserPart> _userField = new LazyField<HousePartyUserPart>();
        public LazyField<HousePartyUserPart> UserField { get { return _userField; } }
        public HousePartyUserPart User { get { return _userField.Value; } }
    }
}