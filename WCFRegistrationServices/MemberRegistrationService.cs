using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WCFRegistrationServiceInterfaces;
using MembersDatabase;

namespace WCFRegistrationServices
{
    public class MemberRegistrationService : IMemberRegistrationService
    {
        MembersDB md = new MembersDB();

        public MemberDTC[] GetMembers()
        {
            List<Member> memberList = md.GetMembers();
            MemberDTC[] memberArray = new MemberDTC[memberList.Count];

            for (int i = 0; i < memberList.Count; i++)
            {
                MemberDTC m = new MemberDTC();

                m.MemberId = memberList[i].MemberId;
                m.MemberTitle = memberList[i].Title;
                m.MemberFirstName = memberList[i].FirstName;
                m.MemberLastName = memberList[i].LastName;
                m.MemberSex = memberList[i].Sex;
                m.MemberAge = memberList[i].Age;
                m.MemberAddress = memberList[i].Address;

                memberArray[i] = m;
            }

            /*MemberDTC[] memberArray = new MemberDTC[1];

            MemberDTC m = new MemberDTC();

            m.MemberId = 1;
            m.MemberTitle = "Mr.";
            m.MemberFirstName = "Lionel";
            m.MemberLastName = "Messi";
            m.MemberSex = "Male";
            m.MemberAge = 28;
            m.MemberAddress = "Argentina";

            memberArray[0] = m;*/

            return memberArray;
        }

        public MemberDTC GetMember(int id)
        {
            Member member = md.GetMember(id);
            MemberDTC m = new MemberDTC();

            m.MemberId = member.MemberId;
            m.MemberTitle = member.Title;
            m.MemberFirstName = member.FirstName;
            m.MemberLastName = member.LastName;
            m.MemberSex = member.Sex;
            m.MemberAge = member.Age;
            m.MemberAddress = member.Address;

            return m;
        }

        public void CreateMember(MemberDTC mdtc)
        {
            Member member = new Member();

            member.MemberId = mdtc.MemberId;
            member.Title = mdtc.MemberTitle;
            member.FirstName = mdtc.MemberFirstName;
            member.LastName = mdtc.MemberLastName;
            member.Sex = mdtc.MemberSex;
            member.Age = mdtc.MemberAge;
            member.Address = mdtc.MemberAddress;

            md.CreateMember(member);
        }

        public void UpdateMember(MemberDTC mdtc)
        {
            Member member = new Member();

            member.MemberId = mdtc.MemberId;
            member.Title = mdtc.MemberTitle;
            member.FirstName = mdtc.MemberFirstName;
            member.LastName = mdtc.MemberLastName;
            member.Sex = mdtc.MemberSex;
            member.Age = mdtc.MemberAge;
            member.Address = mdtc.MemberAddress;

            md.UpdateMember(member);
        }

        public void DeleteMember(MemberDTC mdtc)
        {
            Member member = new Member();

            member.MemberId = mdtc.MemberId;
            member.Title = mdtc.MemberTitle;
            member.FirstName = mdtc.MemberFirstName;
            member.LastName = mdtc.MemberLastName;
            member.Sex = mdtc.MemberSex;
            member.Age = mdtc.MemberAge;
            member.Address = mdtc.MemberAddress;

            md.DeleteMember(member);
        }
    }
}
