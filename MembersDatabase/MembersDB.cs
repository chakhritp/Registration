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

        public List<Member> GetMemberList()
        {
            // bind the existing member collection<table> record in grid
            List<Member> memberList = dbContext.Members.Find(m => true).ToList();

            return memberList;
        }

        public void CreateMember(Member member)
        {
            dbContext.Members.InsertOne(member);
        }

        public void UpdateMember(Member member)
        {
            dbContext.Members.ReplaceOne(m => m.MemberId == member.MemberId, member);
        }

        public void DeleteMember(Member member)
        {
            dbContext.Members.DeleteOne(m => m.MemberId == member.MemberId);
        }
    }
}

