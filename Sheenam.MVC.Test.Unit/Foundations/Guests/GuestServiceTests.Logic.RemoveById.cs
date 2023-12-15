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
        public async Task ShouldRemoveGuestByIdAsync()
        {
            // given
            Guid randomId = Guid.NewGuid();
            Guid inputGuestId = randomId;
            Guest randomGuest = CreateRandomGuest();
            Guest storagedGuest = randomGuest;
            Guest expectedInputGuest = storagedGuest;
            Guest deletedGuest = expectedInputGuest;
            Guest expectedGuest = deletedGuest.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectGuestByIdAsync(inputGuestId))
                    .ReturnsAsync(storagedGuest);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteGuestAsync(expectedInputGuest))
                    .ReturnsAsync(deletedGuest);

            // when
            Guest actualTicket = await this.guestService
                .RemoveGuestByIdAsync(inputGuestId);

            // then
            actualTicket.Should().BeEquivalentTo(expectedGuest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectGuestByIdAsync(inputGuestId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteGuestAsync(expectedInputGuest), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
