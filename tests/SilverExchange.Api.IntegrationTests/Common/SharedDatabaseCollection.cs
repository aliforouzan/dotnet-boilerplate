namespace DotnetBoilerPlate.Api.IntegrationTests.Common;

[CollectionDefinition("Test collection")]
public class SharedDatabaseCollection : ICollectionFixture<CustomWebApplicationFactory>
{
    
}