
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace DALUnitTests.RepoUnitTests
{
    public class StatusLevelUnitTests
    {
        private IStatusLevelRepository _repository;
        private Mock<ILogger<StatusLevelRepository>> _mockLogger;
        private NetigentContext _context;

        [OneTimeSetUp]
        public void Setup()
        {
            // Create in-memory database options
            var options = new DbContextOptionsBuilder<NetigentContext>()
                .UseInMemoryDatabase(databaseName: "NetigentTestDatabase")
                .Options;

            _context = new NetigentContext(options);
            _mockLogger = new Mock<ILogger<StatusLevelRepository>>();

            _repository = new StatusLevelRepository(_context, _mockLogger.Object);

            // Seed test data
            _context.StatusLevels.AddRange(new List<StatusLevel>
        {
            new StatusLevel { Id = 1, StatusName = "New" },
            new StatusLevel { Id = 2, StatusName = "Approved" }
        });
            _context.SaveChanges();
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetAllStatusLevelsAsync_ReturnsAllStatusLevels()
        {
            // Act
            var result = await _repository.GetAllStatusLevelsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.That(_context.StatusLevels.ToList().Count, Is.EqualTo(result.Count()));
        }

        [Test]
        [TestCase(1, "New")]
        [TestCase(2, "Approved")]
        public async Task GetStatusLevelByIdAsync_ValidId_ReturnsStatusLevel(int id, string statusName)
        {
            // Act
            var result = await _repository.GetStatusLevelByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.That(statusName, Is.EqualTo(result?.StatusName));
        }

        [Test]
        public async Task GetStatusLevelByIdAsync_InvalidId_ReturnsNull()
        {
            // Act
            var result = await _repository.GetStatusLevelByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task AddStatusLevelAsync_ValidStatusLevel_AddsSuccessfully()
        {
            // Arrange
            var newStatus = new StatusLevel { Id = 3, StatusName = "In Progress" };

            // Act
            await _repository.AddStatusLevelAsync(newStatus);
            var result = await _repository.GetStatusLevelByIdAsync(3);

            // Assert
            Assert.NotNull(result);
            Assert.That(result?.StatusName, Is.EqualTo("In Progress"));
        }

        [Test, Ignore("ignoring as excute delete async not supported in memeory database")]
        public async Task DeleteStatusLevelAsync_ValidId_DeletesSuccessfully()
        {
            // Act
            await _repository.DeleteStatusLevelAsync(1);
            var result = await _repository.GetStatusLevelByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Test,Ignore("ignoring as excute update async not supported in memeory database")]
        public async Task UpdateStatusLevel_ValidStatusLevel_UpdatesSuccessfully()
        {
            // Arrange
            var updatedStatus = new StatusLevel { Id = 2, StatusName = "New" };

            // Act
            await _repository.UpdateStatusLevel(updatedStatus);
            var result = await _repository.GetStatusLevelByIdAsync(2);

            // Assert
            Assert.NotNull(result);
            Assert.That(result?.StatusName, Is.EqualTo("New"));

        }
    }
}