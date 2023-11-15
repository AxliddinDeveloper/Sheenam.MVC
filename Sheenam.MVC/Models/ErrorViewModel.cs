//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

namespace Sheenam.MVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
