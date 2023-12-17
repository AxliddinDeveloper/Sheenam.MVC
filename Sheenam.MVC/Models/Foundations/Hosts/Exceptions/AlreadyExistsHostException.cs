//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Sheenam.MVC.Models.Foundations.Hosts.Exceptions
{
    public class AlreadyExistsHostException : Xeption
    {
        public AlreadyExistsHostException(Exception innerException)
            : base(message: "Host already exists.", innerException)
        { }
    }
}
