using Orchard.Data.Migration;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Migrations
{
    public class RecordMigrations : DataMigrationImpl
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

            SchemaBuilder.CreateTable(typeof(FriendRequestRecord).Name,
                table => table
                    .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
                    .Column("RequesterId", DbType.Int32)
                    .Column("RequestedId", DbType.Int32));

            SchemaBuilder.CreateTable(typeof(FriendRecord).Name,
                table => table
                    .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
                    .Column("FriendOneId", DbType.Int32)
                    .Column("FriendTwoId", DbType.Int32));

            return 2;
        }

        public int UpdateFrom1()
        {
            SchemaBuilder.CreateTable(typeof(FriendRecord).Name,
                table => table
                    .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
                    .Column("FriendOneId", DbType.Int32)
                    .Column("FriendTwoId", DbType.Int32));

            return 2;
        }
    }
}