﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN.Common.Entity
{
    public class User
    {
        /// <summary>
        /// Tên tài khoản
        /// </summary>
        [Required]
        public string Account { get; set; }
        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}