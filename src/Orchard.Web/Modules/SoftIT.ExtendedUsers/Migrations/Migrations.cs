using Orchard.Data.Migration;
using SoftIT.ExtendedUsers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.MetaData;

namespace SoftIT.ExtendedUsers.Migrations
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(typeof(ExtendedUserPartRecord).Name,
               table => table
                   .ContentPartRecord()
                   .Column("FirstName", DbType.String)
                   .Column("LastName", DbType.String)
                   .Column("Phone", DbType.String)
                   .Column("Country", DbType.String)
                   .Column("State", DbType.String)
                   .Column("City", DbType.String)
                   .Column("Address", DbType.String)
                   .Column("DateOfBirth", DbType.DateTime));

            ContentDefinitionManager.AlterTypeDefinition("User",
                type => type
                    .WithPart(typeof(ExtendedUserPart).Name));

            return 1;
        }
    }
}