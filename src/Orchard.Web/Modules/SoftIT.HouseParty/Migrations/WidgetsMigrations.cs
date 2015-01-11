using Orchard.Data.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.MetaData;
using SoftIT.HouseParty.Constants;
using SoftIT.HouseParty.Models;

namespace SoftIT.HouseParty.Migrations
{
    public class WidgetsMigrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterTypeDefinition(ContentTypes.UserSearchWidget,
                type => type
                    .WithPart(typeof(UserSearchWidgetPart).Name)
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget"));

            return 1;
        }
    }
}