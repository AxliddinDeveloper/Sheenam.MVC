//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Sheenam.MVC.Models.Foundations.Hosts;
using Sheenam.MVC.Models.Foundations.Hosts.Exceptions;

namespace Sheenam.MVC.Services.Foundations.Hosts
{
    public partial class HostService
    {
        private void ValidateHost(Host host)
        {
            ValidateHostNotNull(host);

            Validate(
                (Rule: IsInvalid(host.Id), Parameter: nameof(Host.Id)),
                (Rule: IsInvalid(host.FirstName), Parameter: nameof(Host.FirstName)),
                (Rule: IsInvalid(host.LastName), Parameter: nameof(Host.LastName)),
                (Rule: IsInvalid(host.Email), Parameter: nameof(Host.Email)),
                (Rule: IsInvalid(host.PhoneNumber), Parameter: nameof(Host.PhoneNumber)));
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidHostException = new InvalidHostExcpetion();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidHostException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidHostException.ThrowIfContainsErrors();
        }
        
        private void ValidateHostNotNull(Host host)
        {
            if (host is null)
            {
                throw new NullHostException();
            }
        }
    }
}
