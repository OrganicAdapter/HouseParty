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
        private readonly LazyField<IContentQuery<ContentItem>> _partiesField = new LazyField<IContentQuery<ContentItem>>();
        public LazyField<IContentQuery<ContentItem>> PartiesField { get { return _partiesField; } }
        public IContentQuery<ContentItem> Parties { get { return _partiesField.Value; } }

        private readonly LazyField<IContentQuery<ContentItem>> _invitationsField = new LazyField<IContentQuery<ContentItem>>();
        public LazyField<IContentQuery<ContentItem>> InvitationsField { get { return _invitationsField; } }
        public IContentQuery<ContentItem> Invitations { get { return _invitationsField.Value; } }

        public IEnumerable<HousePartyUserPart> Friends { get; set; }
        public IEnumerable<HousePartyUserPart> FriendRequests { get; set; }

        public string FirstName
        {
            get { return Retrieve(x => x.FirstName); }
            set { Store(x => x.FirstName, value); }
        }

        public string LastName
        {
            get { return Retrieve(x => x.LastName); }
            set { Store(x => x.LastName, value); }
        }

        public string Country
        {
            get { return Retrieve(x => x.Country); }
            set { Store(x => x.Country, value); }
        }

        public string State
        {
            get { return Retrieve(x => x.State); }
            set { Store(x => x.State, value); }
        }

        public string City
        {
            get { return Retrieve(x => x.City); }
            set { Store(x => x.City, value); }
        }

        public string Address
        {
            get { return Retrieve(x => x.Address); }
            set { Store(x => x.Address, value); }
        }

        public double Rating
        {
            get { return Likes - Dislikes; }
        }

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
    }

    public class HousePartyUserPartRecord : ContentPartRecord
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }
        public virtual int Likes { get; set; }
        public virtual int Dislikes { get; set; }
    }
}