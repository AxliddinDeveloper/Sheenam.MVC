//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Hosts.Exceptions
{
    public class FailedHostStorageException : Xeption
    {
        public FailedHostStorageException( Exception innerException) 
            : base(message: "Failed host storage error occurred, contact support", innerException)
        {}
    }
}
