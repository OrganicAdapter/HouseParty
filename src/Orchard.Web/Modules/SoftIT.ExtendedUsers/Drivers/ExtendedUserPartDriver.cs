using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using SoftIT.ExtendedUsers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SoftIT.ExtendedUsers.Drivers
{
    public class ExtendedUserPartDriver : ContentPartDriver<ExtendedUserPart>
    {
        protected override string Prefix
        {
            get
            {
                return "SoftIT.ExtendedUsers.Models.ExtendedUserPart";
            }
        }

        public Localizer T { get; set; }

        public ExtendedUserPartDriver()
        {
            T = NullLocalizer.Instance;
        }

        protected override DriverResult Editor(ExtendedUserPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_ExtendedUser_Edit", () => shapeHelper.EditorTemplate(
                TemplateName: "Parts/ExtendedUser",
                Model: part,
                Prefix: Prefix));
        }

        protected override DriverResult Editor(ExtendedUserPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            if (!string.IsNullOrWhiteSpace(part.Phone) && !IsPhoneNumberValid(part.Phone))
            {
                updater.AddModelError("MalformedPhoneNumber", T("Phone number is not valid. Correct format: +36304567896"));
            }

            return Editor(part, shapeHelper);
        }

        private bool IsPhoneNumberValid(string phone)
        { 
            return Regex.IsMatch(phone, @"^\+(?:[0-9]●?){6,14}[0-9]$");
        }
    }
}