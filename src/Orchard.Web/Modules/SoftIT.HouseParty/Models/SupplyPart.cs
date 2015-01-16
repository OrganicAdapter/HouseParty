using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.ContentManagement.Records;
using Orchard.Core.Common.Utilities;
using Orchard.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Web;

namespace SoftIT.HouseParty.Models
{
    public class SupplyPart : ContentPart<SupplyPartRecord>, ITitleAspect
    {
        private readonly LazyField<IUser> _assignedToField = new LazyField<IUser>();
        internal LazyField<IUser> AssignedToField { get { return _assignedToField; } }
        public IUser AssignedTo { get { return _assignedToField.Value; } }

        private readonly LazyField<PartyPart> _partyField = new LazyField<PartyPart>();
        internal LazyField<PartyPart> PartyField { get { return _partyField; } }
        public PartyPart Party { get { return _partyField.Value; } }

        [Required]
        public string Name
        {
            get { return Retrieve(x => x.Name); }
            set { Store(x => x.Name, value); }
        }

        [Required]
        public decimal Price
        {
            get { return Retrieve(x => x.Price); }
            set { Store(x => x.Price, value); }
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

        public int AssignedToId
        {
            get { return Retrieve(x => x.AssignedToId); }
            set { Store(x => x.AssignedToId, value); }
        }

        [Required]
        public decimal Quantity
        {
            get { return Retrieve(x => x.Quantity); }
            set { Store(x => x.Quantity, value); }
        }

        [Required]
        public string Unit
        {
            get { return Retrieve(x => x.Unit); }
            set { Store(x => x.Unit, value); }
        }

        public int PersonPerUnit
        {
            get { return Retrieve(x => x.PersonPerUnit); }
            set { Store(x => x.PersonPerUnit, value); }
        }

        public int PartyId
        {
            get { return Retrieve(x => x.PartyId); }
            set { Store(x => x.PartyId, value); }
        }

        public string Title
        {
            get { return Name; }
        }
    }

    public class SupplyPartRecord : ContentPartRecord
    {
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int Likes { get; set; }
        public virtual int Dislikes { get; set; }
        public virtual int AssignedToId { get; set; }
        public virtual decimal Quantity { get; set; }
        public virtual string Unit { get; set; }
        public virtual int PersonPerUnit { get; set; }
        public virtual int PartyId { get; set; }
    }
}