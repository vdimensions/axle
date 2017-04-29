using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Verification;


namespace Axle.Extensions.String
{
    partial class StringExtensions
    {
        public static string Intern(this string str) { return string.Intern(str.VerifyArgument(nameof(str)).IsNotNull()); }

    }
}