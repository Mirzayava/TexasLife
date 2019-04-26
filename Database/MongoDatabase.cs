using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace TexasLife.Database
{
    public class TLMongoDatabase
    {

        private static readonly IMongoClient Client = new MongoClient("mongodb://localhost:27017");
        private static readonly IMongoDatabase Database = Client.GetDatabase("TLDB");

        public void Insert<T>(T data)
        // USEAGE:  db.Insert<User>(user);
        {
                var collection = Database.GetCollection<T>(typeof(T).Name);
                collection.InsertOne(data);
        }

        public async void Update<T>(ObjectId id, string updateFieldName, dynamic updateFieldValue)
        // USEAGE:  db.Update<User>("5cb3463b1d190531ec8d8693", "Email", "abc@123.com");
        {
            try
            {
                var collection = Database.GetCollection<T>(typeof(T).Name);
                var filter = Builders<T>.Filter.Eq("_id", id);
                var update = Builders<T>.Update.Set(updateFieldName, updateFieldValue);
                await collection.UpdateOneAsync(filter, update);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async void Delete<T>(ObjectId id)
        // USEAGE:  db.Update<User>("5cb3463b1d190531ec8d8693");
        {
            var collection = Database.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq("_id", id);
            await collection.DeleteOneAsync(filter);
        }

        public async Task<T> GetSingle<T>(string fieldName, string fieldValue)
        // USEAGE:  var result = db.GetSingle<User>("Username", "Bob").Result;
        {
            try
            {
                var collection = Database.GetCollection<T>(typeof(T).Name);
                var filter = Builders<T>.Filter.Eq(fieldName, fieldValue);
                return await collection.Find(filter).SingleAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<T>> GetList<T>(string fieldName, string fieldValue)
        // USEAGE:  var result = db.GetList<User>("Username", "Bob").Result;
        {
            var collection = Database.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq(fieldName, fieldValue);
            return await collection.Find(filter).ToListAsync();
        }

        public async Task<List<T>> GetList<T>()
        // USEAGE:  var result = db.GetList<User>("Username", "Bob").Result;
        {
            var collection = Database.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Empty;
            return await collection.Find(filter).ToListAsync();
        }

        public async Task<List<T>> GetListById<T>(ObjectId id)
        // USEAGE:  var result = db.GetListById<User>("Username", "Bob").Result;
        {
            var collection = Database.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq("_id", id);
            return await collection.Find(filter).ToListAsync();
        }
    }
}

