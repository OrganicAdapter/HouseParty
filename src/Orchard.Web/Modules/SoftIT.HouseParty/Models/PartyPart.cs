using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.ContentManagement.Records;
using Orchard.Core.Common.Utilities;
using Orchard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Models
{
    public class PartyPart : ContentPart<PartyPartRecord>, ITitleAspect
    {
        private readonly LazyField<IUser> _organizerField = new LazyField<IUser>();
        public LazyField<IUser> OrganizerField { get { return _organizerField; } }
        public IUser Organizer { get { return _organizerField.Value; } }

        public string Name
        {
            get { return Retrieve(x => x.Name); }
            set { Store(x => x.Name, value); }
        }

        public string PublicType
        {
            get { return Retrieve(x => x.PublicType); }
            set { Store(x => x.PublicType, value); }
        }

        public string FoodType
        {
            get { return Retrieve(x => x.FoodType); }
            set { Store(x => x.FoodType, value); }
        }

        public string DrinkType
        {
            get { return Retrieve(x => x.DrinkType); }
            set { Store(x => x.DrinkType, value); }
        }

        public DateTime Date
        {
            get
            {
                DateTime date;

                if (DateTime.TryParse(Retrieve(x => x.Date), out date))
                    return date;
                else
                    return DateTime.Now;
            }
            set { Store(x => x.Date, value.ToString()); }
        }

        public double BasePrice
        {
            get { return Retrieve(x => x.BasePrice); }
            set { Store(x => x.BasePrice, value); }
        }

        public double Price
        {
            get { return Retrieve(x => x.Price); }
            set { Store(x => x.Price, value); }
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

        public string Policy
        {
            get { return Retrieve(x => x.Policy); }
            set { Store(x => x.Policy, value); }
        }

        public double Rating
        {
            get { return Retrieve(x => x.Rating); }
            set { Store(x => x.Rating, value); }
        }

        public int Limit
        {
            get { return Retrieve(x => x.Limit); }
            set { Store(x => x.Limit, value); }
        }

        public bool Visibility
        {
            get { return Retrieve(x => x.Visibility); }
            set { Store(x => x.Visibility, value); }
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
        public virtual string Date { get; set; }
        public virtual double BasePrice { get; set; }
        public virtual double Price { get; set; }
        public virtual string Country { get; set; }
        public virtual string State { get; set; }
        public virtual string City { get; set; }
        public virtual string Address { get; set; }
        public virtual string Policy { get; set; }
        public virtual double Rating { get; set; }
        public virtual int Limit { get; set; }
        public virtual bool Visibility { get; set; }
    }
}