using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIRegistration.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}