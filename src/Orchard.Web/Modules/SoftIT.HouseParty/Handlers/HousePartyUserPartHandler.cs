using Orchard;
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
            IRepository<FriendRecord> friendRepositoryWork,
            IContentManager contentManager)
        {
            Filters.Add(StorageFilter.For(repository));

            OnActivated<HousePartyUserPart>((context, part) =>
            {
                part.FriendRequestsRecordsField.Loader(() => friendRequestRepositoryWork.Table.Where(
                    request => request
                        .RequestedId.Equals(part.Id))
                        .ToList());

                part.FriendsRecordsField.Loader(() => friendRepositoryWork.Table.Where(
                    friend => friend
                        .FriendOneId.Equals(part.Id) || friend.FriendTwoId.Equals(part.Id))
                        .ToList());
            });
        }
    }
}