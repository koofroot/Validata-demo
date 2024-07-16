using Moq;
using Validata.Data.Interfaces;
using Validata.Data.Models;
using Validata.Infrastructure.Handlers.CustomerHandlers;

namespace Validata.Infrastructure.Tests.Handlers.CustomerHandlers
{
    public class CreateCustomerRequestHandlerTests
    {
        private CreateCustomerRequestHandler _createCustomerRequestHandler;

        private Mock<IRepository<Customer>> _customerRepository;

        [SetUp]
        public void SetUp() 
        {
            _customerRepository = new Mock<IRepository<Customer>>();
            _createCustomerRequestHandler = new CreateCustomerRequestHandler(_customerRepository.Object);
        }

        [Test]
        public async Task Handle_ShouldCallCreateAndSaveOnce()
        {
            var command = new CreateCustomerCommand("c1", "c2", "c3", "c4");

            var result = await _createCustomerRequestHandler.Handle(command, CancellationToken.None);

            _customerRepository.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.Once);
            _customerRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }


        [Test]
        public async Task Handle_ShouldReturnNewCustomerId()
        {
            var command = new CreateCustomerCommand("c1", "c2", "c3", "c4");
            var id = Guid.NewGuid();
            _customerRepository.Setup(x => x.CreateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(() => id);

            var result = await _createCustomerRequestHandler.Handle(command, CancellationToken.None);

            Assert.That(result, Is.EqualTo(id));
        }
    }
}
