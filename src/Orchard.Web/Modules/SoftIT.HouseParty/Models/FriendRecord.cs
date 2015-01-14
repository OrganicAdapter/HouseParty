using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Models
{
    public class FriendRecord
    {
        public virtual int Id { get; set; }
        public virtual int FriendOneId { get; set; }
        public virtual int FriendTwoId { get; set; }
    }
}