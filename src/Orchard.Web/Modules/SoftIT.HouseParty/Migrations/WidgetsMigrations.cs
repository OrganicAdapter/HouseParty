using Orchard.Data.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.ContentManagement.MetaData;
using SoftIT.HouseParty.Constants;
using SoftIT.HouseParty.Models;

namespace SoftIT.HouseParty.Migrations
{
    public class WidgetsMigrations : DataMigrationImpl
    {
        public int Create()
        {
            return 1;
        }
    }
}