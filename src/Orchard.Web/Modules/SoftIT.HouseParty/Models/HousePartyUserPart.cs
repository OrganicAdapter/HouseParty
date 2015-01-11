using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Models
{
    public class HousePartyUserPart : ContentPart<HousePartyUserPartRecord>
    {
        public int Likes
        {
            get { return Retrieve(x => x.Likes); }
            set { Store(x => x.Likes, value); }
        }

        public int Dislikes
        {
            get { return Retrieve(x => x.Dislikes); }
            set { Store(x => x.Dislikes, value); }
        }

        public int Money
        {
            get { return Retrieve(x => x.Money); }
            set { Store(x => x.Money, value); }
        }
    }

    public class HousePartyUserPartRecord : ContentPartRecord
    {
        public virtual int Likes { get; set; }
        public virtual int Dislikes { get; set; }
        public virtual int Money { get; set; }
    }
}