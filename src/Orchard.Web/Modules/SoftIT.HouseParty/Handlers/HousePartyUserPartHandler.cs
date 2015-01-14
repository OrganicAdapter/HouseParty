using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment;
using Orchard.Security;
using SoftIT.HouseParty.Constants;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Handlers
{
    public class HousePartyUserPartHandler : ContentHandler
    {
        public HousePartyUserPartHandler(
            IRepository<HousePartyUserPartRecord> repository, 
            IRepository<FriendRequestRecord> friendRequestRepositoryWork,
            IContentManager contentManager)
        {
            Filters.Add(StorageFilter.For(repository));

            OnActivated<HousePartyUserPart>((context, part) =>
            {
                part.FriendRequestsField.Loader(() => friendRequestRepositoryWork.Table.Where(request => request.RequestedId.Equals(part.Id)).ToList());
            });
        }
    }
}