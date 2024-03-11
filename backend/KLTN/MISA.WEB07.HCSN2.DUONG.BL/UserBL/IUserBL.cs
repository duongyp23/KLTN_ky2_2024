﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLTN.Common.Entity;

namespace KLTN.BussinesLayer
{
    public interface IUserBL : IBaseBL<User>
    {
        public string Authenticate(User user);
    }
}