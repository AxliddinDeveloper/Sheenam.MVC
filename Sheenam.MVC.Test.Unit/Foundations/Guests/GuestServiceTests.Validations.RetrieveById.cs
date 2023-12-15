//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Sheenam.MVC.Models.Foundations.Guests;
using Sheenam.MVC.Models.Foundations.Guests.Exceptions;
using Xunit;

namespace Sheenam.MVC.Test.Unit.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            var invalidGuestId = Guid.Empty;
            var invalidGuestException = new InvalidGuestExcpetion();

            invalidGuestException.AddData(
                key: nameof(Guest.Id),
                values: "Id is required");

            var expectedGuestValidationException = new
                GuestValidationException(invalidGuestException);

            // when
            ValueTask<Guest> retrieveGuestByIdTask =
                this.guestService.RetrieveGuestByIdAsync(invalidGuestId);

            GuestValidationException actualGuestValidationException =
                await Assert.ThrowsAsync<GuestValidationException>(
                    retrieveGuestByIdTask.AsTask);

            // then
            actualGuestValidationException.Should().BeEquivalentTo(expectedGuestValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuestValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectGuestByIdAsync(It.IsAny<Guid>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionOnRetrieveByIdIfGuestIsNotFoundAndLogItAsync()
        {
            // given
            Guid someGuestId = Guid.NewGuid();
            Guest noGuest = null;

            var notFoundGuestException =
                new NotFoundGuestException(someGuestId);

            var expectedGuestValidationException =
                new GuestValidationException(notFoundGuestException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectGuestByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(noGuest);

            // when
            ValueTask<Guest> retrieveGuestByIdTask =
                this.guestService.RetrieveGuestByIdAsync(someGuestId);

            GuestValidationException actualGuestalidationException =
                await Assert.ThrowsAsync<GuestValidationException>(
                    retrieveGuestByIdTask.AsTask);

            // then
            actualGuestalidationException.Should().BeEquivalentTo(expectedGuestValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectGuestByIdAsync(It.IsAny<Guid>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuestValidationException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
