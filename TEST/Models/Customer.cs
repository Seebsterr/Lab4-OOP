using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

       // [Min18YearsIfAMember]
        [Display(Name = "Date of Birth")]
        public DateTime? Birthday { get; set; }

        public Movie Movie { get; set; }
        public int? MovieId { get; set; }


        public enum Membership
        {
            Default = 0,
            PayAsYouGo = 1,
            Monthly = 2,
            Quarterly = 3,
            Yearly = 4
        }
    }
}