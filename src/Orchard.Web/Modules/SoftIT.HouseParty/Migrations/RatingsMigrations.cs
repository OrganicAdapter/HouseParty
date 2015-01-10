using Orchard.Data.Migration;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Migrations
{
    public class RatingsMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(typeof(RatingsRecord).Name,
                table => table
                    .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
                    .Column("FromId", DbType.Int32)
                    .Column("ToId", DbType.Int32)
                    .Column("IsLike", DbType.Boolean));

            return 1;
        }
    }
}