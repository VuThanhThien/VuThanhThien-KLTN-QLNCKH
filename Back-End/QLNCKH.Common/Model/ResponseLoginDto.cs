﻿using System;

namespace QLNCKH.Common.Model
{
    public class ResponseLoginDto
    {
        public string Token { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }
    }
}
