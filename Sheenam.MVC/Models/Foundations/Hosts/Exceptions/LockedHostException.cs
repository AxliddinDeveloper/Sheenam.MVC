//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Hosts.Exceptions
{
    public class LockedHostException : Xeption
    {
        public LockedHostException(Exception innerException)
    : base(message: "Host is locked, please try again.", innerException)
        { }
    }
}
