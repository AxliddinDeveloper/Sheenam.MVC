//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Guests.Exceptions
{
    public class InvalidGuestExcpetion : Xeption
    {
        public InvalidGuestExcpetion()
    : base(message: "Guest is invalid.")
        { }
    }
}
