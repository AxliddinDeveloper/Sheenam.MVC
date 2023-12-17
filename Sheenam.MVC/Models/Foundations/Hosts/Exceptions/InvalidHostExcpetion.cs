//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Hosts.Exceptions
{
    public class InvalidHostExcpetion : Xeption
    {
        public InvalidHostExcpetion()
            : base(message: "Host is invalid.")
        { }
    }
}
