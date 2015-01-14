using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using Orchard.Core.Common.Utilities;
using Orchard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Models
{
    public class HousePartyUserPart : ContentPart<HousePartyUserPartRecord>
    {
        private readonly LazyField<IEnumerable<FriendRequestRecord>> _friendRequestsRecordsField = new LazyField<IEnumerable<FriendRequestRecord>>();
        internal LazyField<IEnumerable<FriendRequestRecord>> FriendRequestsRecordsField { get { return _friendRequestsRecordsField; } }
        public IEnumerable<FriendRequestRecord> FriendRequestsRecords { get { return _friendRequestsRecordsField.Value; } }

        private readonly LazyField<IEnumerable<FriendRecord>> _friendsRecordsField = new LazyField<IEnumerable<FriendRecord>>();
        internal LazyField<IEnumerable<FriendRecord>> FriendsRecordsField { get { return _friendsRecordsField; } }
        public IEnumerable<FriendRecord> FriendsRecords { get { return _friendsRecordsField.Value; } }

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