﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookServer.Models.DTO.Auth
{
    public class SignInDTOModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
