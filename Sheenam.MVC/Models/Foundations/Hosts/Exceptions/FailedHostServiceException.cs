//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Hosts.Exceptions
{
    public class FailedHostServiceException : Xeption
    {
        public FailedHostServiceException(Exception innerException)
    : base(message: "Host service error occurred, contact support.", innerException)
        { }
    }
}
