//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Hosts.Exceptions
{
    public class NotFoundHostException : Xeption
    {
        public NotFoundHostException(Guid hostId)
            : base(message: $"Couldn't find host with id: {hostId}")
        { }
    }
}
