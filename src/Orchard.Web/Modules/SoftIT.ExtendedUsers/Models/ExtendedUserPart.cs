using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoftIT.ExtendedUsers.Models
{
    public class ExtendedUserPart : ContentPart<ExtendedUserPartRecord>
    {
        [Required]
        public string FirstName
        {
            get { return Retrieve(x => x.FirstName); }
            set { Store(x => x.FirstName, value); }
        }

        [Required]
        public string LastName
        {
            get { return Retrieve(x => x.LastName); }
            set { Store(x => x.LastName, value); }
        }

        [DataType(DataType.PhoneNumber)]
        public string Phone
        {
            get { return Retrieve(x => x.Phone); }
            set { Store(x => x.Phone, value); }
        }

        public string Country
        {
            get { return Retrieve(x => x.Country); }
            set { Store(x => x.Country, value); }
        }

        public string State
        {
            get { return Retrieve(x => x.State); }
            set { Store(x => x.State, value); }
        }

        public string City
        {
            get { return Retrieve(x => x.City); }
            set { Store(x => x.City, value); }
        }

        public string Address
        {
            get { return Retrieve(x => x.Address); }
            set { Store(x => x.Address, value); }
        }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth
        {
            get { return Retrieve(x => x.DateOfBirth, DateTime.UtcNow.AddYears(-18)); }
            set { Store(x => x.DateOfBirth, value); }
        }
    }

    public class ExtendedUserPartRecord : ContentPartRecord
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Country { get; set; }
        public virtual string State { get; set; }
        public virtual string City { get; set; }
        public virtual string Address { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
    }
}