using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.Models;
using Orchard.Data;
using Orchard.Environment;
using Orchard.Security;
using Orchard.Services;
using SoftIT.HouseParty.Constants;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Handlers
{
    public class UserSearchPartHandler : ContentHandler
    {
        public UserSearchPartHandler(Work<IContentManager> contentManagerWork)
        {
            OnActivated<UserSearchPart>((context, part) =>
            {
                part.UsersField.Loader(() =>
                    contentManagerWork.Value
                        .Query("User")
                        .ForPart<IUser>());
            });
        }
    }
}