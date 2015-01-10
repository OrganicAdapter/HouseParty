using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.Models;
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
            Work<IContentManager> contentManagerWork, 
            IRepository<FriendsRecord> friendsRepository)
        {
            Filters.Add(StorageFilter.For(repository));

            OnActivated<HousePartyUserPart>((context, part) =>
                {
                    part.PartiesField.Loader(() =>
                        contentManagerWork.Value
                            .Query(ContentTypes.Party)
                            .Where<CommonPartRecord>(record => record.OwnerId.Equals(part.Id)));

                    part.Friends = friendsRepository.Table.ToList()
                        .Where(item => (item.FriendOne.Equals(part.Id) || item.FriendTwo.Equals(part.Id)) && item.IsAccepted)
                        .Select(item => item.FriendOne.Equals(part.Id) ? contentManagerWork.Value.Get<HousePartyUserPart>(item.FriendTwo) : contentManagerWork.Value.Get<HousePartyUserPart>(item.FriendOne));

                    part.FriendRequests = friendsRepository.Table.ToList()
                        .Where(item => item.FriendTwo.Equals(part.Id) && !item.IsAccepted)
                        .Select(item => item.FriendOne.Equals(part.Id) ? contentManagerWork.Value.Get<HousePartyUserPart>(item.FriendTwo) : contentManagerWork.Value.Get<HousePartyUserPart>(item.FriendOne));
                });
        }
    }
}