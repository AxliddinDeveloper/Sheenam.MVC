//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.MVC.Models.Foundations.Guests;
using Xunit;

namespace Sheenam.MVC.Test.Unit.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldModifyGuestAsync()
        {
            // given
            Guest randomGuest = CreateRandomGuest();
            Guest inputGuest = randomGuest;
            Guest storageGuest = inputGuest.DeepClone();
            Guest updatedGuest = inputGuest;
            Guest expectedGuest = updatedGuest.DeepClone();
            Guid guestId = inputGuest.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectGuestByIdAsync(guestId)).ReturnsAsync(storageGuest);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateGuestAsync(inputGuest)).ReturnsAsync(updatedGuest);

            // when
            Guest actualGuest =
                await this.guestService.ModifyGuestAsync(inputGuest);

            // then
            actualGuest.Should().BeEquivalentTo(expectedGuest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectGuestByIdAsync(guestId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateGuestAsync(inputGuest), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
