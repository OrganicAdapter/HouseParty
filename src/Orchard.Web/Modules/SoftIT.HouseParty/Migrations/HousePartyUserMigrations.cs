using Orchard.Data.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.MetaData;
using SoftIT.ExtendedUsers.Models;
using System.Data;
using SoftIT.HouseParty.Models;

namespace SoftIT.HouseParty.Migrations
{
    public class HousePartyUserMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(typeof(HousePartyUserPartRecord).Name,
               table => table
                   .ContentPartRecord()
                   .Column("Likes", DbType.Int32)
                   .Column("Dislikes", DbType.Int32)
                   .Column("Money", DbType.Int32));

            ContentDefinitionManager.AlterTypeDefinition("User",
                type => type
                    .WithPart(typeof(HousePartyUserPart).Name));

            return 1;
        }
    }
}