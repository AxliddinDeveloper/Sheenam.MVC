//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Moq;
using Sheenam.MVC.Brokers.Loggings;
using Sheenam.MVC.Brokers.Storages;
using Sheenam.MVC.Models.Foundations.Hosts;
using Sheenam.MVC.Services.Foundations.Hosts;
using Tynamix.ObjectFiller;

namespace Sheenam.MVC.Test.Unit.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IHostService hostService;

        public HostServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.hostService = new HostService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Host CreateRandomHost() =>
            CreateHostFiller().Create();

        private static Filler<Host> CreateHostFiller() =>
            new Filler<Host>();
    }
}
