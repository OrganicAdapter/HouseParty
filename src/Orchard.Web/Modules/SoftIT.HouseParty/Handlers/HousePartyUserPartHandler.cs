using Orchard;
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
            IRepository<FriendRequestRecord> friendRequestRepositoryWork,
            IRepository<FriendRecord> friendRepositoryWork,
            IRepository<InvitationRecord> invitationsRepositoryWork,
            Work<IContentManager> contentManagerWork)
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

                part.InvitationsRecordsField.Loader(() => invitationsRepositoryWork.Table.Where(
                    invitation => 
                        invitation.InvitedId.Equals(part.Id) && 
                        invitation.State.Equals("Pending"))
                        .ToList());

                part.PartiesField.Loader(() => contentManagerWork.Value.Query(ContentTypes.Party)
                    .List()
                    .Where(
                        party => party
                            .As<CommonPart>()
                            .Owner.Id.Equals(part.Id))
                    .AsPart<PartyPart>());

                part.PartiesInvitedField.Loader(() => invitationsRepositoryWork.Table.Where(
                    invitation => 
                        invitation.InvitedId.Equals(part.Id) &&
                        invitation.State.Equals("Accepted")));
            });
        }
    }
}