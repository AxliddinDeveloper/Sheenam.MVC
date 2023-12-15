//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Moq;
using System.Threading.Tasks;
using System;
using Xunit;
using Sheenam.MVC.Models.Foundations.Guests;
using Force.DeepCloner;
using FluentAssertions;

namespace Sheenam.MVC.Test.Unit.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveTeamByIdAsync()
        {
            // given
            Guid randomGuestId = Guid.NewGuid();
            Guid inputGuestId = randomGuestId;
            Guest randomGuest = CreateRandomGuest();
            Guest storageGuest = randomGuest;
            Guest expectedGuest = storageGuest.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectGuestByIdAsync(inputGuestId)).ReturnsAsync(storageGuest);

            // when
            Guest actualTeam =
                await this.guestService.RetrieveGuestByIdAsync(inputGuestId);

            // then
            actualTeam.Should().BeEquivalentTo(expectedGuest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectGuestByIdAsync(inputGuestId), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
