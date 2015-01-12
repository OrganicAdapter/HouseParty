using Orchard.Data.Migration;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Migrations
{
    public class InvitationMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(typeof(InvitationRecord).Name,
                table => table
                    .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
                    .Column("InviterId", DbType.Int32)
                    .Column("InvitedId", DbType.Int32)
                    .Column("PartyId", DbType.Int32)
                    .Column("State", DbType.String));

            return 1;
        }
    }
}