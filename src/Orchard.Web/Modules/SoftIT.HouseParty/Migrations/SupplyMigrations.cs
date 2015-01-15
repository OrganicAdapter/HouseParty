using Orchard.Data.Migration;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Migrations
{
    public class SupplyMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(typeof(SupplyPartRecord).Name,
                table => table
                    .ContentPartRecord()
                    .Column("Name", DbType.String)
                    .Column("Price", DbType.Decimal)
                    .Column("Likes", DbType.Int32)
                    .Column("Dislikes", DbType.Int32)
                    .Column("AssignedToId", DbType.Int32)
                    .Column("Quantity", DbType.Decimal)
                    .Column("Unit", DbType.String)
                    .Column("PersonPerUnit", DbType.Int32)
                    .Column("PartyId", DbType.Int32));

            return 1;
        }
    }
}