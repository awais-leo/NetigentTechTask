using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace DALUnitTests.RepoUnitTests
{
    [TestFixture]
    public class InquiryRepoUnitTests
    {
        private IInquiryRepository _repository;
        private Mock<ILogger<InquiryRepository>> _mockLogger;
        private NetigentContext _context;

        [OneTimeSetUp]
        public void Setup()
        {
            // Set up in-memory database
            var options = new DbContextOptionsBuilder<NetigentContext>()
                .UseInMemoryDatabase(databaseName: "NetigentTestDatabase")
                .Options;

            _context = new NetigentContext(options);
            _mockLogger = new Mock<ILogger<InquiryRepository>>();

            _repository = new InquiryRepository(_context, _mockLogger.Object);

            // Seed test data
            _context.Inquries.AddRange(new List<Inqury>
            {
                new Inqury { Id = 1, Subject = "Inquiry 1", Inquiry = "Test Inquiry 1" },
                new Inqury { Id = 2, Subject = "Inquiry 2", Inquiry = "Test Inquiry 2" }
            });
            _context.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
            
        }

        [Test]
        public async Task GetAllInquiriesAsync_ReturnsAllInquiries()
        {
            // Act
            var result = await _repository.GetAllInquiriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.That(_context.Inquries.Count(), Is.EqualTo(result.Count()));
        }

        [Test]
        [TestCase(1,"Inquiry 1")]
        [TestCase(2, "Inquiry 2")]
        public async Task GetInquiryByIdAsync_ValidId_ReturnsInquiry(int id,string inquiryName)
        {
            // Act
            var result = await _repository.GetInquiryByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.That(result?.Subject, Is.EqualTo(inquiryName));
        }

        [Test]
        public async Task GetInquiryByIdAsync_InvalidId_ReturnsNewInquiry()
        {
            // Act
            var result = await _repository.GetInquiryByIdAsync(99);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(0));
        }

        [Test]
        public async Task AddInquiryAsync_ValidInquiry_AddsSuccessfully()
        {
            // Arrange
            var newInquiry = new Inqury { Id = 3, Subject = "Inquiry 3", Inquiry = "Test Inquiry 3" };

            // Act
            await _repository.AddInquiryAsync(newInquiry);
            var result = await _repository.GetInquiryByIdAsync(3);

            // Assert
            Assert.NotNull(result);
            Assert.That(result?.Subject, Is.EqualTo("Inquiry 3"));
        }

        [Test, Ignore("ignoring as excute delete async not supported in memeory database")]
        public async Task DeleteInquiryAsync_ValidId_DeletesSuccessfully()
        {
            // Act
            await _repository.DeleteInquiryAsync(1);
            var result = await _repository.GetInquiryByIdAsync(1);

            // Assert
            Assert.That(result?.Id, Is.EqualTo(0));
        }

        [Test, Ignore("ignoring as excute update async not supported in memeory database")]
        public async Task UpdateInquiry_ValidInquiry_UpdatesSuccessfully()
        {
            // Arrange
            var updatedInquiry = new Inqury { Id = 2, Subject = "Updated Inquiry", Inquiry = "Updated Inquiry Text" };

            // Act
            await _repository.UpdateInquiry(updatedInquiry);
            var result = await _repository.GetInquiryByIdAsync(2);

            // Assert
            Assert.NotNull(result);
            Assert.That(result?.Subject, Is.EqualTo("Updated Inquiry"));
            Assert.That(result?.Inquiry, Is.EqualTo("Updated Inquiry Text"));
        }
    }
}
