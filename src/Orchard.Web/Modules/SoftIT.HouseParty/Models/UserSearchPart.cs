﻿using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;
using Orchard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftIT.HouseParty.Models
{
    public class UserSearchPart : ContentPart
    {
        private readonly LazyField<IContentQuery<IUser>> _usersField = new LazyField<IContentQuery<IUser>>();
        public LazyField<IContentQuery<IUser>> UsersField { get { return _usersField; } }
        public IContentQuery<IUser> Users { get { return _usersField.Value; } }
    }
}