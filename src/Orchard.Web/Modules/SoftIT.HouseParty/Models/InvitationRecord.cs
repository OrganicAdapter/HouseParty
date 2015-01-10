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
        public virtual int InvitingId { get; set; }
        public virtual int InvitedId { get; set; }
        public virtual int PartyId { get; set; }
    }
}