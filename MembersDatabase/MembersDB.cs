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
    public class MembersDB
    {
        MongoDBContext dbContext = new MongoDBContext();

        public List<Member> GetMembers()
        {
            // bind the existing member collection<table> record in grid
            List<Member> memberList = dbContext.Members.Find(m => true).ToList();

            return memberList;
        }

        public Member GetMember(int id)
        {
            return dbContext.Members.Find(m => m.MemberId == id).FirstOrDefault();
        }

        public void CreateMember(Member member)
        {
            member.MemberId = (int)dbContext.Members.Count(new BsonDocument()) + 1;

            dbContext.Members.InsertOne(member);
        }

        public void UpdateMember(Member member)
        {
            Member mb = GetMember(member.MemberId);

            mb.Title = member.Title;
            mb.FirstName = member.FirstName;
            mb.LastName = member.LastName;
            mb.Sex = member.Sex;
            mb.Age = member.Age;
            mb.Address = member.Address;

            dbContext.Members.ReplaceOne(m => m.MemberId == mb.MemberId, mb);
        }

        public void DeleteMember(Member member)
        {
            dbContext.Members.DeleteOne(m => m.MemberId == member.MemberId);
        }
    }
}
