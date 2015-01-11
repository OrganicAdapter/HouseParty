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
    public class UserSearchWidgetPartHandler : ContentHandler
    {
        public UserSearchWidgetPartHandler(Work<IContentManager> contentManagerWork)
        {
            OnActivated<UserSearchWidgetPart>((context, part) =>
                {
                    part.UsersField.Loader(() => contentManagerWork.Value.Query("User").ForPart<IUser>());
                });
        }
    }
}