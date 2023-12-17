//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Hosts.Exceptions
{
    public class NullHostException : Xeption
    {
        public NullHostException() : base(message: "Host is null.")
        { }
    }
}
