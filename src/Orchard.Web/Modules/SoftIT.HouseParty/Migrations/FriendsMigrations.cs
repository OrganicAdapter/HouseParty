using Orchard.Data.Migration;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Migrations
{
    public class FriendsMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(typeof(FriendsRecord).Name,
                table => table
                    .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
                    .Column("FriendOne", DbType.Int32)
                    .Column("FriendTwo", DbType.Int32)
                    .Column("IsAccepted", DbType.Boolean));

            return 1;
        }
    }
}