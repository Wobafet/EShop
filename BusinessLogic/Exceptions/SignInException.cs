﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions
{
    public class SignInException : Exception
    {
        public SignInException(string message) : base(message)
        {

        }
    }
}
