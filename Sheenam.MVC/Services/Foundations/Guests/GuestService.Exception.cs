//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Threading.Tasks;
using Sheenam.MVC.Models.Foundations.Guests;
using Sheenam.MVC.Models.Foundations.Guests.Exceptions;
using Xeptions;

namespace Sheenam.MVC.Services.Foundations.Guests
{
    public partial class GuestService
    {
        private delegate ValueTask<Guest> ReturningGuestFunction();

        private async ValueTask<Guest> TryCatch(ReturningGuestFunction returningTeamFunction)
        {
            try
            {
                return await returningTeamFunction();
            }
            catch (NullGuestException nullGuestExcpetion)
            {
                throw CreateAndLogValidationException(nullGuestExcpetion);
            }
            catch (InvalidGuestExcpetion invalidGuestException)
            {
                throw CreateAndLogValidationException(invalidGuestException);
            }
            catch (NotFoundGuestException notFoundGuestException)
            {
                throw CreateAndLogValidationException(notFoundGuestException);
            }
        }

        private GuestValidationException CreateAndLogValidationException(Xeption exception)
        {
            var guestValidationExpcetion = new GuestValidationException(exception);
            this.loggingBroker.LogError(guestValidationExpcetion);

            return guestValidationExpcetion;
        }
    }
}
