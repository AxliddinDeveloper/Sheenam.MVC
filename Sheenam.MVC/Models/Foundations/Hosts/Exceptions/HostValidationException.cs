//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Hosts.Exceptions
{
    public class HostValidationException : Xeption
    {
        public HostValidationException(Xeption innerException)
            : base(message: "Host validation error occured, fix the errors and try again.", 
                  innerException)
        { }
    }
}
