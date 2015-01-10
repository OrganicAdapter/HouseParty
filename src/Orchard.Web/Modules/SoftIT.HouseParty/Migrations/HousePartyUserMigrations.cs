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

namespace SoftIT.HouseParty.Migrations
{
    public class HousePartyUserMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(typeof(HousePartyUserPartRecord).Name, 
                table => table
                    .ContentPartRecord()
                    .Column("FirstName", DbType.String)
                    .Column("LastName", DbType.String)
                    .Column("Country", DbType.String)
                    .Column("State", DbType.String)
                    .Column("City", DbType.String)
                    .Column("Address", DbType.String)
                    .Column("Likes", DbType.Int32)
                    .Column("Dislikes", DbType.Int32));

            ContentDefinitionManager.AlterTypeDefinition("User", 
                type => type
                    .WithPart(typeof(HousePartyUserPart).Name));

            return 1;
        }
    }
}