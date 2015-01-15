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
    public class PartyPartHandler : ContentHandler
    {
        public PartyPartHandler(
            IRepository<PartyPartRecord> repository, 
            Work<IContentManager> contentManagerWork,
            IRepository<InvitationRecord> invitationsRepository)
        {
            Filters.Add(StorageFilter.For(repository));

            OnActivated<PartyPart>((context, part) =>
            {
                part.SuppliesField.Loader(() =>
                    contentManagerWork.Value
                        .Query(ContentTypes.Supply)
                        .ForPart<SupplyPart>()
                        .List<SupplyPart>()
                        .Where(supply => supply.PartyId.Equals(part.Id))
                        .ToList());

                part.ParticipantInvitationsRecordsField.Loader(() => 
                    invitationsRepository.Table
                        .Where(invitation =>
                            invitation.PartyId.Equals(part.Id) &&
                            invitation.State.Equals("Accepted"))
                        .ToList());
            });
        }
    }
}