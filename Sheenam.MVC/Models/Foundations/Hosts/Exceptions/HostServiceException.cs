//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Hosts.Exceptions
{
    public class HostServiceException : Xeption
    {
        public HostServiceException(Exception innerException)
            : base(message: "Host service error occurred, contact support.", innerException)
        { }
    }
}
