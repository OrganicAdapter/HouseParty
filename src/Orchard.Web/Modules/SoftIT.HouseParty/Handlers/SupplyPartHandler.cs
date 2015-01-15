using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment;
using Orchard.Security;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Handlers
{
    public class SupplyPartHandler : ContentHandler
    {
        public SupplyPartHandler(IRepository<SupplyPartRecord> repository, Work<IContentManager> contentManagerWork)
        {
            Filters.Add(StorageFilter.For(repository));

            OnActivated<SupplyPart>((context, part) =>
                {
                    part.PartyField.Loader(() => contentManagerWork.Value.Get(part.PartyId));
                    part.AssignedToField.Loader(() => contentManagerWork.Value.Get(part.AssignedToId).As<IUser>());
                });
        }
    }
}