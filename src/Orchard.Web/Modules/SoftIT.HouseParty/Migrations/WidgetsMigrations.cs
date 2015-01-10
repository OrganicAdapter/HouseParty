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
            ContentDefinitionManager.AlterTypeDefinition(ContentTypes.UserSearchWidget,
                type => type
                    .WithPart(typeof(UserSearchPart).Name)
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget"));

            ContentDefinitionManager.AlterTypeDefinition(ContentTypes.CreatePartyWidget,
                type => type
                    .WithPart(typeof(CreatePartyPart).Name)
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget"));

            ContentDefinitionManager.AlterTypeDefinition(ContentTypes.EditHousePartyUserWidget,
                type => type
                    .WithPart(typeof(EditHousePartyUserPart).Name)
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget"));

            return 3;
        }

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterTypeDefinition(ContentTypes.CreatePartyWidget,
                type => type
                    .WithPart(typeof(CreatePartyPart).Name)
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget"));

            return 2;
        }

        public int UpdateFrom2()
        {
            ContentDefinitionManager.AlterTypeDefinition(ContentTypes.EditHousePartyUserWidget,
                type => type
                    .WithPart(typeof(EditHousePartyUserPart).Name)
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget"));

            return 3;
        }
    }
}