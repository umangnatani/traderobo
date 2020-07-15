﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TradeRobo.Service
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            UserName = user.UserName;
            Token = token;
        }
    }
}
