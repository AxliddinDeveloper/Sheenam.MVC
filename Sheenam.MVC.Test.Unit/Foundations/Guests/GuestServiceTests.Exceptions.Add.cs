//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using EFxceptions.Models.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.MVC.Models.Foundations.Guests;
using Sheenam.MVC.Models.Foundations.Guests.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Sheenam.MVC.Test.Unit.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guest someGuest = CreateRandomGuest();
            SqlException sqlException = CreateSqlException();
            var failedGuestStorageException = new FailedGuestStorageException(sqlException);

            var expectedGuestDependencyException =
                new GuestDependencyException(failedGuestStorageException);

            this.storageBrokerMock.Setup(broker => broker.InsertGuestAsync(someGuest)).ThrowsAsync(sqlException);

            // when
            ValueTask<Guest> addGuestTask = this.guestService.AddGuestAsync(someGuest);

            GuestDependencyException actualGuestDependencyException =
                await Assert.ThrowsAsync<GuestDependencyException>(addGuestTask.AsTask);

            // then
            actualGuestDependencyException.Should().BeEquivalentTo(expectedGuestDependencyException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedGuestDependencyException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuestAsync(It.IsAny<Guest>()), Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldTrowDependencyValidationExceptionOnAddIfDuplicateErrorOccursAndLogItAsync()
        {
            // given
            Guest someGuest = CreateRandomGuest();
            string someMessage = GetRandomString();
            var duplicateKeyException = new DuplicateKeyException(someMessage);

            var alreadyExistsGuestException =
                new AlreadyExistsGuestException(duplicateKeyException);

            var expectedGuestDependencyValidationException =
                new GuestDependencyValidationException(alreadyExistsGuestException);

            this.storageBrokerMock.Setup(broker => 
                broker.InsertGuestAsync(someGuest)).ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<Guest> addGuestTask = this.guestService.AddGuestAsync(someGuest);

            GuestDependencyValidationException actualGuestDependencyValidationException =
                await Assert.ThrowsAsync<GuestDependencyValidationException>(addGuestTask.AsTask);

            // then
            actualGuestDependencyValidationException.Should().BeEquivalentTo(
                expectedGuestDependencyValidationException);

            this.loggingBrokerMock.Verify(broker => broker.LogError(It.Is(SameExceptionAs(
                expectedGuestDependencyValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker => broker.InsertGuestAsync(
               It.IsAny<Guest>()), Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
