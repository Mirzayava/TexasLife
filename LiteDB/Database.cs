using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;

namespace TexasLife.Database
{
    public static class TLDatabase
    {
        // Pull the data before your Upsert it.
        public static bool Upsert<T>(T data)
        {
            using(var db = new LiteDatabase(@"./Database.db"))
            {
                return db.GetCollection<T>().Upsert(data);
            }
        }

        // Return a specific value based on a specific field.
        // Return null if nothing is found
        public static T GetDataByField<T>(string fieldName, BsonValue data)
        {
            using (var db = new LiteDatabase(@"./Database.db"))
            {
                return db.GetCollection<T>().FindOne(Query.EQ(fieldName, data));
            }
        }

        public static bool Update<T>(T data)
        {
            using (var db = new LiteDatabase(@"./Database.db"))
            {
                return db.GetCollection<T>().Update(data);
            }
        }

        public static T GetByID<T>(int id)
        {
            using (var db = new LiteDatabase(@"./Database.db"))
            {
                return db.GetCollection<T>().FindById(id);
            }
        }
    }
}
