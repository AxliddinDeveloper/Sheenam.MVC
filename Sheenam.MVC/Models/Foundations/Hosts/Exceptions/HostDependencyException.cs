//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Hosts.Exceptions
{
    public class HostDependencyException : Xeption
    {
        public HostDependencyException(Exception innerException) 
            : base (message: "Host dependency error occurred, contact support.", innerException)
        {}
    }
}
