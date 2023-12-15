//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Guests.Exceptions
{
    public class NotFoundGuestException : Xeption
    {
        public NotFoundGuestException(Guid guestId)
            : base(message: $"Couldn't find guest with id: {guestId}")
        { }
    }
}
