using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Description;
using WCFRegistrationServiceInterfaces;
using WCFRegistrationServices;

namespace RegistrationServiceHost
{
    class Program
    {
        static ServiceHost host = null;

        static void StartService()
        {
            host = new ServiceHost(typeof(MemberRegistrationService)); 
	    /***********
	     * if you don't want to use App.Config for the web service host, 
             * just uncomment below:
	     ***********
             host.AddServiceEndpoint(new ServiceEndpoint(
             ContractDescription.GetContract(typeof(IStudentEnrollmentService)),
             new WSHttpBinding(), 
             new EndpointAddress("http://localhost:8732/awesomeschoolservice")));
	     **********/
            host.AddServiceEndpoint(new ServiceEndpoint(
             ContractDescription.GetContract(typeof(IMemberRegistrationService)),
             new WSHttpBinding(),
             new EndpointAddress("http://localhost:8732/memberregistrtionservice")));
            host.Open();
        }

        static void CloseService()
        {
            if (host.State != CommunicationState.Closed)
            {
                host.Close();
            }
        }

        static void Main(string[] args)
        {
            StartService();

            Console.WriteLine("Member Registration Service is running....");

            using (ChannelFactory<IMemberRegistrationService> registrationServiceProxy =
            new ChannelFactory<IMemberRegistrationService>("MemberRegistrationServiceEndpoint"))
            {
                registrationServiceProxy.Open();
                IMemberRegistrationService memberRegistrationService = registrationServiceProxy.CreateChannel();
                MemberDTC[] members = memberRegistrationService.GetMembers();
                foreach (MemberDTC m in members)
                {
                    Console.WriteLine(m.MemberId + "," + m.MemberTitle + "," + m.MemberFirstName + "," + m.MemberLastName + "," + m.MemberSex + "," + m.MemberAge + "," + m.MemberAddress);
                }
                registrationServiceProxy.Close();
            }

            Console.ReadKey();

            CloseService();
        }
    }
}

