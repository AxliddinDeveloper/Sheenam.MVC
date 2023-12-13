//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Sheenam.MVC.Models.Foundations.Guests;
using Sheenam.MVC.Models.Foundations.Guests.Exceptions;

namespace Sheenam.MVC.Services.Foundations.Guests
{
    public partial class GuestService
    {
        private void ValidateGuest(Guest guest)
        {
            ValidateGuestNotNull(guest);

            Validate(
                (Rule: IsInvalid(guest.Id), Parameter: nameof(Guest.Id)),
                (Rule: IsInvalid(guest.FirstName), Parameter: nameof(Guest.FirstName)),
                (Rule: IsInvalid(guest.LastName), Parameter: nameof(Guest.LastName)),
                (Rule: IsInvalid(guest.Email), Parameter: nameof(Guest.Email)),
                (Rule: IsInvalid(guest.PhoneNumber), Parameter: nameof(Guest.PhoneNumber)));
        }

        private void ValidateGuestOnModify(Guest guest)
        {
            ValidateGuestNotNull(guest);

            Validate(
                (Rule: IsInvalid(guest.Id), Parameter: nameof(Guest.Id)),
                (Rule: IsInvalid(guest.FirstName), Parameter: nameof(Guest.FirstName)),
                (Rule: IsInvalid(guest.LastName), Parameter: nameof(Guest.LastName)),
                (Rule: IsInvalid(guest.Email), Parameter: nameof(Guest.Email)),
                (Rule: IsInvalid(guest.PhoneNumber), Parameter: nameof(Guest.PhoneNumber)));
        }

        private void ValidateGuestId(Guid guestId) =>
            Validate((Rule: IsInvalid(guestId), Parameter: nameof(Guest.Id)));

        private static void ValidateStorageGuestExists(Guest maybeGuest, Guid guestId)
        {
            if (maybeGuest is null)
                throw new NotFoundGuestException(guestId);
        }

        private static void ValidateAgainstStorageGuestOnModify(Guest inputGuest, Guest storageGuest) =>
            ValidateStorageGuestExists(storageGuest, inputGuest.Id);

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
            var invalidGuestException = new InvalidGuestExcpetion();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidGuestException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidGuestException.ThrowIfContainsErrors();
        }

        private void ValidateGuestNotNull(Guest guest)
        {
            if (guest is null)
            {
                throw new NullGuestException();
            }
        }
    }
}
