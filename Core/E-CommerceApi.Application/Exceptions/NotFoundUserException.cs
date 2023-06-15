﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Application.Exceptions
{
    public class NotFoundUserException:Exception
    {
        public NotFoundUserException():base("Wrong Username or Passoword")
        {
        }

        public NotFoundUserException(string? message):base(message)
        {

        }

        public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
