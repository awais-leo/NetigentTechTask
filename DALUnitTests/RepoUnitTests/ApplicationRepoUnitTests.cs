using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;


namespace DALUnitTests.RepoUnitTests
{
    [TestFixture]
    public class ApplicationRepoUnitTests
    {
        private IApplicationRepository _repository;
        private Mock<ILogger<ApplicationRepository>> _mockLogger;
        private NetigentContext _context;

        [OneTimeSetUp]
        public void Setup()
        {
            // Create in-memory database options
            var options = new DbContextOptionsBuilder<NetigentContext>()
                .UseInMemoryDatabase(databaseName: "NetigentTestDatabase")
                .Options;

            _context = new NetigentContext(options);
            _mockLogger = new Mock<ILogger<ApplicationRepository>>();

            _repository = new ApplicationRepository(_context, _mockLogger.Object);

            // Seed test data
            _context.Applications.AddRange(new List<Application>
            {
                new Application { Id = 1, ProjectName = "Project A", AppStatus = "Closed",Status = new StatusLevel(){Id=1, StatusName ="Closed" } },
                new Application { Id = 2, ProjectName = "Project B", AppStatus = "Open",Status = new StatusLevel(){Id=2, StatusName ="Open" } }
            });
            _context.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetAllApplicationsAsync_ReturnsAllApplications()
        {
            // Act
            var result = await _repository.GetAllApplicationsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.That(_context.Applications.Count(), Is.EqualTo(result.Count()));
        }

        [Test]
        [TestCase(1,"Project A")]
        [TestCase(2,"Project B")]
        public async Task GetApplicationByIdAsync_ValidId_ReturnsApplication(int applicationId,string projectName)
        {
            // Act
            var result = await _repository.GetApplicationByIdAsync(applicationId);

            // Assert
            Assert.NotNull(result);
            Assert.That(result?.ProjectName, Is.EqualTo(projectName));
        }

        [Test]
        public async Task GetApplicationByIdAsync_InvalidId_ReturnsNull()
        {
            // Act
            var result = await _repository.GetApplicationByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task AddApplicationAsync_ValidApplication_AddsSuccessfully()
        {
            // Arrange
            var newApplication = new Application { Id = 3, ProjectName = "Project C", AppStatus = "Pending", Status = new StatusLevel() { Id = 3, StatusName = "Pending" } };

            // Act
            await _repository.AddApplicationAsync(newApplication);
            var result = await _repository.GetApplicationByIdAsync(newApplication.Id);

            // Assert
            Assert.NotNull(result);
            Assert.That(result?.ProjectName, Is.EqualTo(newApplication.ProjectName));
        }

        [Test, Ignore("ignoring as excute delete async not supported in memeory database")]
        public async Task DeleteApplicationAsync_ValidId_DeletesSuccessfully()
        {
            // Act
            await _repository.DeleteApplicationAsync(1);
            var result = await _repository.GetApplicationByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Test, Ignore("ignoring as excute update async not supported in memeory database")]
        public async Task UpdateApplication_ValidApplication_UpdatesSuccessfully()
        {
            // Arrange
            var updatedApplication = new Application { Id = 2, ProjectName = "Updated Project B", AppStatus = "Closed" };

            // Act
            await _repository.UpdateApplication(updatedApplication);
            var result = await _repository.GetApplicationByIdAsync(2);

            // Assert
            Assert.NotNull(result);
            Assert.That(result?.ProjectName, Is.EqualTo("Updated Project B"));
            Assert.That(result?.AppStatus, Is.EqualTo("Archived"));
        }
    }
}
