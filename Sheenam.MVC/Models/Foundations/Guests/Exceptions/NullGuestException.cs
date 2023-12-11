//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Guests.Exceptions
{
    public class NullGuestException : Xeption
    {
        public NullGuestException() : base(message: "Guest is null.")
        { }
    }
}
