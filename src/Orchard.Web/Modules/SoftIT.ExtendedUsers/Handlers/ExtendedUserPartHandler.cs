using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using SoftIT.ExtendedUsers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.ExtendedUsers.Handlers
{
    public class ExtendedUserPartHandler : ContentHandler
    {
        public ExtendedUserPartHandler(IRepository<ExtendedUserPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}