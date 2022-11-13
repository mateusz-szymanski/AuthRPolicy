using AuthRPolicy.MediatRExtensions.Exceptions;
using AuthRPolicy.Sample.Authorization;
using AuthRPolicy.Sample.Features.CreateDocument;
using AuthRPolicy.Sample.Tests.CustomRequests.GetAllDocuments;
using AuthRPolicy.Sample.Tests.Initialization;
using AuthRPolicy.Sample.Tests.UserMocking;
using System.Threading.Tasks;
using Xunit;

namespace AuthRPolicy.Sample.Tests.Features
{
    public class CreateDocumentCommandTests : IClassFixture<EmptyDatabaseFixture>
    {
        private readonly EmptyDatabaseFixture _emptyDatabaseFixture;

        public CreateDocumentCommandTests(EmptyDatabaseFixture emptyDatabaseFixture)
        {
            _emptyDatabaseFixture = emptyDatabaseFixture;
        }

        [Fact]
        public async Task Handler_ShouldCreateDocument_GivenValidUser()
        {
            // Arrange
            var title = "My Title";
            var reviewer = "my-reviewer";

            var currentUser = new UserBuilder()
                .WithRoles(Roles.DocumentCreator)
                .Build();

            await using var app = await new ApplicationBuilder()
                .WithExistingDatabase(_emptyDatabaseFixture.ConnectionStringProvider)
                .Build();

            // Act
            app.UserSwitcher.ChangeUser(currentUser);
            var command = new CreateDocumentCommand(title, reviewer);
            await app.Mediator.Send(command);

            // Assert
            var actualDocuments = await app.Mediator.Send(new GetAllDocumentsQuery());

            Assert.Single(actualDocuments);
        }

        [Fact]
        public async Task Handler_ShouldThrowUserUnauthorizedException_GivenInvalidUser()
        {
            // Arrange
            var title = "My Title";
            var reviewer = "my-reviewer";

            var currentUser = new UserBuilder()
                .WithRoles(Roles.DocumentReviewer, Roles.Admin)
                .Build();

            await using var app = await new ApplicationBuilder()
                .WithExistingDatabase(_emptyDatabaseFixture.ConnectionStringProvider)
                .Build();

            // Act
            // Assert
            app.UserSwitcher.ChangeUser(currentUser);
            var command = new CreateDocumentCommand(title, reviewer);

            await Assert.ThrowsAsync<UserUnauthorizedException>(() => app.Mediator.Send(command));
        }
    }
}
