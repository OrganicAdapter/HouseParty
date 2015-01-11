using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Models
{
    public class InvitationRecord
    {
        public virtual int Id { get; set; }
        public virtual int InviterId { get; set; }
        public virtual int InvitedId { get; set; }
        public virtual int PartyId { get; set; }
        public virtual string State { get; set; }
    }
}