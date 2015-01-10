using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Models
{
    public class RatingsRecord
    {
        public virtual int Id { get; set; }
        public virtual int FromId { get; set; }
        public virtual int ToId { get; set; }
        public virtual bool IsLike { get; set; }
    }
}