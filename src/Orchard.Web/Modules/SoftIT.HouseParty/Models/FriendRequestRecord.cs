using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Models
{
    public class FriendRequestRecord
    {
        public virtual int Id { get; set; }
        public virtual int RequesterId { get; set; }
        public virtual int RequestedId { get; set; }
    }
}