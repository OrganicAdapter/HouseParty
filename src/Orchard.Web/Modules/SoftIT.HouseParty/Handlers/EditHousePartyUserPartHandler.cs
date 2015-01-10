using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment;
using Orchard.Security;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Handlers
{
    public class EditHousePartyUserPartHandler : ContentHandler
    {
        public EditHousePartyUserPartHandler(Work<IContentManager> contentManagerWork, IOrchardServices orchardServices)
        {
            OnActivated<EditHousePartyUserPart>((context, part) =>
            {
                if (orchardServices.WorkContext.CurrentUser != null)
                {
                    part.UserField.Loader(() =>
                        contentManagerWork.Value.Get<HousePartyUserPart>(orchardServices.WorkContext.CurrentUser.Id));
                }
            });
        }
    }
}