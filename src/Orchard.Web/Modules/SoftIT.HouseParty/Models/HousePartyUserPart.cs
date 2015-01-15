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

        private readonly LazyField<IEnumerable<InvitationRecord>> _invitationsRecordsField = new LazyField<IEnumerable<InvitationRecord>>();
        internal LazyField<IEnumerable<InvitationRecord>> InvitationsRecordsField { get { return _invitationsRecordsField; } }
        public IEnumerable<InvitationRecord> InvitationsRecords { get { return _invitationsRecordsField.Value; } }

        private readonly LazyField<IEnumerable<PartyPart>> _partiesField = new LazyField<IEnumerable<PartyPart>>();
        internal LazyField<IEnumerable<PartyPart>> PartiesField { get { return _partiesField; } }
        public IEnumerable<PartyPart> Parties { get { return _partiesField.Value; } }

        private readonly LazyField<IEnumerable<InvitationRecord>> _partiesInvitedField = new LazyField<IEnumerable<InvitationRecord>>();
        internal LazyField<IEnumerable<InvitationRecord>> PartiesInvitedField { get { return _partiesInvitedField; } }
        public IEnumerable<InvitationRecord> PartiesInvited { get { return _partiesInvitedField.Value; } }

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