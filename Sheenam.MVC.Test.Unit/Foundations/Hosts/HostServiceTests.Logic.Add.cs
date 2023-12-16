//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.MVC.Models.Foundations.Hosts;
using Xunit;

namespace Sheenam.MVC.Test.Unit.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        [Fact]
        public async Task ShouldAddHostAsync()
        {
            //given
            Host randomHost = CreateRandomHost();
            Host inputHost = randomHost;
            Host storedHost = inputHost;
            Host expectedHost = storedHost.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertHostAsync(inputHost))
                    .ReturnsAsync(storedHost);
            
            //when
            Host actualHost = await this.hostService
                .AddHostAsync(inputHost);

            //then
            actualHost.Should().BeEquivalentTo(expectedHost);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHostAsync(inputHost), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
