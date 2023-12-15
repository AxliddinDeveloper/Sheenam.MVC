//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Guests.Exceptions
{
    public class GuestValidationException : Xeption
    {
        public GuestValidationException(Xeption innerException)
    : base(message: "Guest validation error occured, fix the errors and try again.", innerException)
        { }
    }
}
