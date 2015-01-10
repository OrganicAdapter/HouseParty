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
        public PartyPartHandler(IRepository<PartyPartRecord> repository, Work<IContentManager> contentManagerWork)
        {
            Filters.Add(StorageFilter.For(repository));

            OnActivated<PartyPart>((context, part) =>
            {
                part.OrganizerField.Loader(() =>
                    contentManagerWork.Value
                        .Query("User")
                        .List<IUser>()
                        .FirstOrDefault(record => record.Id.Equals(part.As<CommonPart>().Owner.Id)));
            });
        }
    }
}