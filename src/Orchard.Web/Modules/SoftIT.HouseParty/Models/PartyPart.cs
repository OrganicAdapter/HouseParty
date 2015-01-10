using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.ContentManagement.Records;
using Orchard.Core.Common.Utilities;
using Orchard.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Models
{
    public class PartyPart : ContentPart<PartyPartRecord>, ITitleAspect
    {
        private readonly LazyField<IUser> _organizerField = new LazyField<IUser>();
        public LazyField<IUser> OrganizerField { get { return _organizerField; } }
        public IUser Organizer { get { return _organizerField.Value; } }

        [Required]
        public string Name
        {
            get { return Retrieve(x => x.Name); }
            set { Store(x => x.Name, value); }
        }

        [Required]
        public string PublicType
        {
            get { return Retrieve(x => x.PublicType); }
            set { Store(x => x.PublicType, value); }
        }

        [Required]
        public string FoodType
        {
            get { return Retrieve(x => x.FoodType); }
            set { Store(x => x.FoodType, value); }
        }

        [Required]
        public string DrinkType
        {
            get { return Retrieve(x => x.DrinkType); }
            set { Store(x => x.DrinkType, value); }
        }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? Date
        {
            get { return Retrieve(x => x.Date, DateTime.UtcNow); }
            set { Store(x => x.Date, value); }
        }

        [Required]
        public double Income
        {
            get { return Retrieve(x => x.Income); }
            set { Store(x => x.Income, value); }
        }

        [Required]
        public string Country
        {
            get { return Retrieve(x => x.Country); }
            set { Store(x => x.Country, value); }
        }

        [Required]
        public string State
        {
            get { return Retrieve(x => x.State); }
            set { Store(x => x.State, value); }
        }

        [Required]
        public string City
        {
            get { return Retrieve(x => x.City); }
            set { Store(x => x.City, value); }
        }

        [Required]
        public string Address
        {
            get { return Retrieve(x => x.Address); }
            set { Store(x => x.Address, value); }
        }

        public string Policy
        {
            get { return Retrieve(x => x.Policy); }
            set { Store(x => x.Policy, value); }
        }

        public int Likes
        {
            get { return Retrieve(x => x.Likes); }
            set { Store(x => x.Likes, value); }
        }

        public int Dislikes
        {
            get { return Retrieve(x => x.Dislikes); }
            set { Store(x => x.Dislikes, value); }
        }

        [Required]
        public int Limit
        {
            get { return Retrieve(x => x.Limit); }
            set { Store(x => x.Limit, value); }
        }

        [Required]
        public bool Visibility
        {
            get { return Retrieve(x => x.Visibility); }
            set { Store(x => x.Visibility, value); }
        }

        [Required]
        public string Category
        {
            get { return Retrieve(x => x.Category); }
            set { Store(x => x.Category, value); }
        }

        public string Title
        {
            get { return Name; }
        }
    }

    public class PartyPartRecord : ContentPartRecord
    {
        public virtual string Name { get; set; }
        public virtual string PublicType { get; set; }
        public virtual string FoodType { get; set; }
        public virtual string DrinkType { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual double Income { get; set; }
        public virtual string Country { get; set; }
        public virtual string State { get; set; }
        public virtual string City { get; set; }
        public virtual string Address { get; set; }
        public virtual string Policy { get; set; }
        public virtual int Likes { get; set; }
        public virtual int Dislikes { get; set; }
        public virtual int Limit { get; set; }
        public virtual bool Visibility { get; set; }
        public virtual string Category { get; set; }
    }
}