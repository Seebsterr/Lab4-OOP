using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TEST.Models;

namespace TEST.ViewModels
{
    public class CustomerFormViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
    }
}