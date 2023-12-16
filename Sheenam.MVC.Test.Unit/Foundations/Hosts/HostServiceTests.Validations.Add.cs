//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Sheenam.MVC.Models.Foundations.Hosts;
using Sheenam.MVC.Models.Foundations.Hosts.Exceptions;
using Xunit;

namespace Sheenam.MVC.Test.Unit.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfInputIsNullAndLogItAsync()
        {
            //given
            Host nullHost = null;
            var nullHostException = new NullHostException();

            var expectedHostValidationException =
                new HostValidationException(nullHostException);

            //when
            ValueTask<Host> addHostTask =
                this.hostService.AddHostAsync(nullHost);

            HostValidationException actualHostValidationException =
               await Assert.ThrowsAsync<HostValidationException>(addHostTask.AsTask);

            //then
            actualHostValidationException.Should()
                .BeEquivalentTo(expectedHostValidationException);

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(
                   expectedHostValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHostAsync(It.IsAny<Host>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
