//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Sheenam.MVC.Models.Foundations.Guests;
using Sheenam.MVC.Models.Foundations.Guests.Exceptions;
using Xeptions;

namespace Sheenam.MVC.Services.Foundations.Guests
{
    public partial class GuestService
    {
        private delegate ValueTask<Guest> ReturningGuestFunction();

        private async ValueTask<Guest> TryCatch(ReturningGuestFunction returningGuestFunction)
        {
            try
            {
                return await returningGuestFunction();
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
            catch (SqlException sqlException)
            {
                var failedGuestStorageException = new FailedGuestStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedGuestStorageException);
            }
        }

        private GuestValidationException CreateAndLogValidationException(Xeption exception)
        {
            var guestValidationExpcetion = new GuestValidationException(exception);
            this.loggingBroker.LogError(guestValidationExpcetion);

            return guestValidationExpcetion;
        }

        private GuestDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var guestDependencyException = new GuestDependencyException(exception);
            this.loggingBroker.LogCritical(guestDependencyException);

            return guestDependencyException;
        }
    }
}
