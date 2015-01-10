using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Models
{
    public class FriendsRecord
    {
        public virtual int Id { get; set; }
        public virtual int FriendOne { get; set; }
        public virtual int FriendTwo { get; set; }
        public virtual bool IsAccepted { get; set; }
    }
}