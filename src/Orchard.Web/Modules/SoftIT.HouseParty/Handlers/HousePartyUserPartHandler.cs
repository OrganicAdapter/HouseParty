using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Handlers
{
    public class HousePartyUserPartHandler : ContentHandler
    {
        public HousePartyUserPartHandler(IRepository<HousePartyUserPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}