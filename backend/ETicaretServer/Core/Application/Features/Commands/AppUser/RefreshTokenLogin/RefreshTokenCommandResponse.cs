﻿using Application.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenCommandResponse
    {
        public Token Token { get; set; }    
    }
}
