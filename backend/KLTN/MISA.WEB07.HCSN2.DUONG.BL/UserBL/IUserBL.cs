﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WEB07.HCSN2.DUONG.Common.Entity;

namespace MISA.WEB07.HCSN2.DUONG.BL
{
    public interface IUserBL : IBaseBL<User>
    {
        public string Authenticate(User user);
    }
}
