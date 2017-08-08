using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIRegistration.Models;

using System.ServiceModel;
using System.ServiceModel.Description;
using WCFRegistrationServiceInterfaces;
using WCFRegistrationServices;

namespace WebAPIRegistration.Controllers
{
    public class MembersController : ApiController
    {
        [HttpGet] // GET api/members
        public IEnumerable<Member> GetAllMembers()
        {
            /*Member mb1 = new Member();
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
            mbl.Add(mb3);*/

            List<Member> mbl = new List<Member>();

            using (ChannelFactory<IMemberRegistrationService> registrationServiceProxy =
                    new ChannelFactory<IMemberRegistrationService>("MemberRegistrationServiceEndpoint"))
            {
                registrationServiceProxy.Open();

                IMemberRegistrationService memberRegistrationService = registrationServiceProxy.CreateChannel();
                MemberDTC[] members = memberRegistrationService.GetMembers();

                foreach (MemberDTC m in members)
                {
                    Member mb = new Member();

                    mb.MemberId = m.MemberId;
                    mb.Title = m.MemberTitle;
                    mb.FirstName = m.MemberFirstName;
                    mb.LastName = m.MemberLastName;
                    mb.Sex = m.MemberSex;
                    mb.Age = m.MemberAge;
                    mb.Address = m.MemberAddress;

                    mbl.Add(mb);

                    //Console.WriteLine(m.MemberId + "," + m.MemberTitle + "," + m.MemberFirstName + "," + m.MemberLastName + "," + m.MemberSex + "," + m.MemberAge + "," + m.MemberAddress);
                }

                registrationServiceProxy.Close();
            }

            return mbl;
        }

        // GET api/members/5
        public Member GetMember(int id)
        {
            /*Member mb1 = new Member();
            Member mb2 = new Member();
            Member mb3 = new Member();
            List<Member> mbl = new List<Member>();

            if (id == 1)
            {
                mb1.MemberId = 1;
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
                mb2.MemberId = 2;
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
                mb3.MemberId = 3;
                mb3.Title = "Mr.";
                mb3.FirstName = "Chakhrit";
                mb3.LastName = "Phungdabot";
                mb3.Sex = "Male";
                mb3.Age = 35;
                mb3.Address = "Thailand";
                mbl.Add(mb3);
            }*/

            //List<Member> mbl = new List<Member>();
            Member mb = new Member();

            using (ChannelFactory<IMemberRegistrationService> registrationServiceProxy =
                    new ChannelFactory<IMemberRegistrationService>("MemberRegistrationServiceEndpoint"))
            {
                registrationServiceProxy.Open();

                IMemberRegistrationService memberRegistrationService = registrationServiceProxy.CreateChannel();
                MemberDTC member = memberRegistrationService.GetMember(id);

                //foreach (MemberDTC m in members)
                //{
                    //Member mb = new Member();

                    mb.MemberId = member.MemberId;
                    mb.Title = member.MemberTitle;
                    mb.FirstName = member.MemberFirstName;
                    mb.LastName = member.MemberLastName;
                    mb.Sex = member.MemberSex;
                    mb.Age = member.MemberAge;
                    mb.Address = member.MemberAddress;

                    //mbl.Add(mb);

                    //Console.WriteLine(m.MemberId + "," + m.MemberTitle + "," + m.MemberFirstName + "," + m.MemberLastName + "," + m.MemberSex + "," + m.MemberAge + "," + m.MemberAddress);
                //}

                registrationServiceProxy.Close();
            }

            return mb;
        }

        [HttpPost] // POST api/members
        public void PostMember([FromBody]Member member)
        {
            using (ChannelFactory<IMemberRegistrationService> registrationServiceProxy =
            new ChannelFactory<IMemberRegistrationService>("MemberRegistrationServiceEndpoint"))
            {
                registrationServiceProxy.Open();

                IMemberRegistrationService memberRegistrationService = registrationServiceProxy.CreateChannel();
                MemberDTC m = new MemberDTC();

                m.MemberTitle = member.Title;
                m.MemberFirstName = member.FirstName;
                m.MemberLastName = member.LastName;
                m.MemberSex = member.Sex;
                m.MemberAge = member.Age;
                m.MemberAddress = member.Address;

                memberRegistrationService.CreateMember(m);
                registrationServiceProxy.Close();
            }
        }

        [HttpPut]  // PUT api/members/5
        public void PutMember(int id, [FromBody]Member member)
        {
            using (ChannelFactory<IMemberRegistrationService> registrationServiceProxy =
            new ChannelFactory<IMemberRegistrationService>("MemberRegistrationServiceEndpoint"))
            {
                registrationServiceProxy.Open();

                IMemberRegistrationService memberRegistrationService = registrationServiceProxy.CreateChannel();
                MemberDTC m = memberRegistrationService.GetMember(member.MemberId);

                m.MemberTitle = member.Title;
                m.MemberFirstName = member.FirstName;
                m.MemberLastName = member.LastName;
                m.MemberSex = member.Sex;
                m.MemberAge = member.Age;
                m.MemberAddress = member.Address;

                memberRegistrationService.UpdateMember(m);
                registrationServiceProxy.Close();
            }
        }

        [HttpDelete] // DELETE api/members/5
        public void DeleteMember(int id)
        {
            using (ChannelFactory<IMemberRegistrationService> registrationServiceProxy =
            new ChannelFactory<IMemberRegistrationService>("MemberRegistrationServiceEndpoint"))
            {
                registrationServiceProxy.Open();

                IMemberRegistrationService memberRegistrationService = registrationServiceProxy.CreateChannel();
                MemberDTC m = memberRegistrationService.GetMember(id);

                memberRegistrationService.DeleteMember(m);
                registrationServiceProxy.Close();
            }
        }
    }
}
