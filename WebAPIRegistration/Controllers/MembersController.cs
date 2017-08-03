using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIRegistration.Models;

namespace WebAPIRegistration.Controllers
{
    public class MembersController : ApiController
    {
        [HttpGet] // GET api/members
        public IEnumerable<Member> GetAllMembers()
        {
            Member mb1 = new Member();
            Member mb2 = new Member();
            Member mb3 = new Member();
            List<Member> mbl = new List<Member>();

            mb1.MemberId = 1;
            mb1.Title = "Mr.";
            mb1.FirstName = "Lionel";
            mb1.LastName = "Messi";
            mb1.Sex = "Male";
            mb1.Age = 28;
            mb1.Address = "Argentina";

            mb2.MemberId = 2;
            mb2.Title = "Ms.";
            mb2.FirstName = "Gal";
            mb2.LastName = "Gadot";
            mb2.Sex = "Male";
            mb2.Age = 32;
            mb2.Address = "Israel";

            mb3.MemberId = 3;
            mb3.Title = "Mr.";
            mb3.FirstName = "Chakhrit";
            mb3.LastName = "Phungdabot";
            mb3.Sex = "Male";
            mb3.Age = 35;
            mb3.Address = "Thailand";

            mbl.Add(mb1);
            mbl.Add(mb2);
            mbl.Add(mb3);

            return mbl;
        }

        // GET api/members/5
        public IEnumerable<Member> Get(int id)
        {
            Member mb1 = new Member();
            Member mb2 = new Member();
            Member mb3 = new Member();
            List<Member> mbl = new List<Member>();

            if (id == 1)
            {
                mb1.Title = "Mr.";
                mb1.FirstName = "Lionel";
                mb1.LastName = "Messi";
                mb1.Sex = "Male";
                mb1.Age = 28;
                mb1.Address = "Argentina";
                mbl.Add(mb1);
            }
            else if (id == 2)
            {
                mb2.Title = "Ms.";
                mb2.FirstName = "Gal";
                mb2.LastName = "Gadot";
                mb2.Sex = "Male";
                mb2.Age = 32;
                mb2.Address = "Israel";
                mbl.Add(mb2);
            }
            else if (id == 3)
            {
                mb3.Title = "Mr.";
                mb3.FirstName = "Chakhrit";
                mb3.LastName = "Phungdabot";
                mb3.Sex = "Male";
                mb3.Age = 35;
                mb3.Address = "Thailand";
                mbl.Add(mb3);
            }

            return mbl;
        }

        // POST api/members
        public void Post([FromBody]string value)
        {
        }

        // PUT api/members/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/members/5
        public void Delete(int id)
        {
        }
    }
}
