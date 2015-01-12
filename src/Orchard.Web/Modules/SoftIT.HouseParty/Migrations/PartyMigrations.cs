using Orchard.Data.Migration;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.ContentManagement.MetaData;
using SoftIT.HouseParty.Constants;

namespace SoftIT.HouseParty.Migrations
{
    public class PartyMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(typeof(PartyPartRecord).Name,
                table => table
                    .ContentPartRecord()
                    .Column("Name", DbType.String)
                    .Column("PublicType", DbType.String)
                    .Column("FoodType", DbType.String)
                    .Column("DrinkType", DbType.String)
                    .Column("Date", DbType.DateTime)
                    .Column("Income", DbType.Double)
                    .Column("Country", DbType.String)
                    .Column("State", DbType.String)
                    .Column("City", DbType.String)
                    .Column("Address", DbType.String)
                    .Column("Policy", DbType.String)
                    .Column("Limit", DbType.Int32)
                    .Column("Visibility", DbType.Boolean)
                    .Column("Category", DbType.String)
                    .Column("Currency", DbType.String));

            ContentDefinitionManager.AlterTypeDefinition(ContentTypes.Party,
                type => type
                    .Creatable()
                    .WithPart(typeof(PartyPart).Name)
                    .WithPart("CommonPart")
                    .WithPart("BodyPart"));

            ContentDefinitionManager.AlterTypeDefinition(ContentTypes.PartyDashboardWidget,
                type => type
                    .WithPart(typeof(NewPartyWidgetPart).Name)
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget")
                );

            return 1;
        }
    }
}