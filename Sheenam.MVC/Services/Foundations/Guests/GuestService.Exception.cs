//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsGuestException = new AlreadyExistsGuestException(duplicateKeyException);

                throw CreateAndDependencyValidationException(alreadyExistsGuestException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedGuestException = new LockedGuestException(dbUpdateConcurrencyException);

                throw CreateAndDependencyValidationException(lockedGuestException);
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

        private GuestDependencyValidationException CreateAndDependencyValidationException(Xeption exception)
        {
            var guestDependencyValidationException = new GuestDependencyValidationException(exception);
            this.loggingBroker.LogError(guestDependencyValidationException);

            return guestDependencyValidationException;
        }
    }
}
