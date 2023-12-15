//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Guests.Exceptions
{
    public class AlreadyExistsGuestException : Xeption
    {
        public AlreadyExistsGuestException(Exception innerException)
            : base(message: "Guest already exists.", innerException)
        { }
    }
}
