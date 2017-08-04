using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCFRegistrationServiceInterfaces
{
    [ServiceContract]
    public interface IMemberRegistrationService
    {
        //[OperationContract]
        //void EnrollStudent(Student s);

        [OperationContract]
        MemberDTC[] GetMembers();

        [OperationContract]
        void CreateMember(MemberDTC mdtc);

        /*[OperationContract]
        void UpdateMember(MemberDTC mdtc);

        [OperationContract]
        void DeleteMembers(MemberDTC mdtc);*/
    }

    [DataContract]
    public class MemberDTC
    {
        private int Id;
        private string Title;
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Sex { get; set; }
        private int Age { get; set; }
        private string Address { get; set; }

        [DataMember]
        public int MemberId
        {
            set { this.Id = value; }
            get { return this.Id; }
        }

        [DataMember]
        public string MemberTitle
        {
            set { this.Title = value; }
            get { return this.Title; }
        }

        [DataMember]
        public string MemberFirstName
        {
            set { this.FirstName = value; }
            get { return this.FirstName; }
        }

        [DataMember]
        public string MemberLastName
        {
            set { this.LastName = value; }
            get { return this.LastName; }
        }

        [DataMember]
        public string MemberSex
        {
            set { this.Sex = value; }
            get { return this.Sex; }
        }

        [DataMember]
        public int MemberAge
        {
            set { this.Age = value; }
            get { return this.Age; }
        }

        [DataMember]
        public string MemberAddress
        {
            set { this.Address = value; }
            get { return this.Address; }
        }
    }
}

