using System.Linq;
using FluentAssertions;
using TcOpen.Inxton.RavenDb;
using Xunit;

namespace IntegrationTests.RavenDb.Features.CreateNvi
{
    [Trait("Create NVI", "Happy Path")]
    public class CreateNvi_HappyPath
    {
        private readonly RavenDbTestRepositorySettings<Nvi> testSettings;

        public CreateNvi_HappyPath()
        {
            testSettings = new RavenDbTestRepositorySettings<Nvi>();
            RavenDbRepository<Nvi> repo = new RavenDbRepository<Nvi>(testSettings);

            repo.Create("test identifier", new Nvi());
        }

        [Fact(DisplayName = "1. One Nvi is created in the database")]
        public void OneNviCreated()
        {
            using (var session = testSettings.Fixture.Store.OpenSession())
            {
                int nviCount = session.Query<Nvi>().Count();

                nviCount.Should().Be(1);
            }
        }
    }
}
