using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Shared;
using MongoDB.Bson.Serialization.Attributes;

namespace MembersDatabase
{
    public class MongoDBContext
    {
        //public static bool IsSSL { get; set; }
        private IMongoDatabase _database { get; set; }

        public MongoDBContext()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://localhost"));

                //if (IsSSL)
                //{
                //    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                //}

                var mongoClient = new MongoClient(settings);

                _database = mongoClient.GetDatabase("RegistrationDB");
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to db server.", ex);
            }
        }

        public IMongoCollection<Member> Members
        {
            get
            {
                return _database.GetCollection<Member>("Members");
            }
        }
    }
}
