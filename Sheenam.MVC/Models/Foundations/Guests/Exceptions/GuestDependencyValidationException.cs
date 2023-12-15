//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Guests.Exceptions
{
    public class GuestDependencyValidationException : Xeption
    {
        public GuestDependencyValidationException(Xeption innerException)
            : base(message: "Guest dependency validation error occurred, fix the errors and try again.",
                innerException)
        { }
    }
}
