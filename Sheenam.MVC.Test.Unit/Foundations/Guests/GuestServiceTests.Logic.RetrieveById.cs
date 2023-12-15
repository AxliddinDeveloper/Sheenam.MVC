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
