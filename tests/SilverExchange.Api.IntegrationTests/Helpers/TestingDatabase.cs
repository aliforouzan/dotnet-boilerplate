using System;
using System.Collections.Generic;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Domain.Entities.Enums;
using DotnetBoilerPlate.Infrastructure;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Interfaces;

namespace DotnetBoilerPlate.Api.IntegrationTests.Helpers;

public static class TestingDatabase
{
    public static async Task SeedDatabase(Func<IContext> contextFactory)
    {
        await using var db = contextFactory();
        // db.Heroes.AddRange(GetSeedingHeroes);
        await db.SaveChangesAsync();
    }

    //
    // public static readonly List<Hero> GetSeedingHeroes =
    //     new()
    //     {
    //         new(){ Id = new Guid("824a7a65-b769-4b70-bccb-91f880b6ddf1"), Name = "Corban Best", HeroType = HeroType.ProHero },
    //         new() { Id = new Guid("b426070e-ccb3-42e6-8fb4-ef6aa5a62cc4"), Name = "Priya Hull", HeroType = HeroType.Student },
    //         new() { Id = new Guid("634769f7-a7b8-4146-9cb2-ff2dd90e886b"), Name = "Harrison Vu", HeroType = HeroType.Teacher }
    //     };
}