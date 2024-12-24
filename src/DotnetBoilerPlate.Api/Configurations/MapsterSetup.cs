using Mapster;
using Newtonsoft.Json;
using System.Linq;

namespace DotnetBoilerPlate.Api.Configurations;

public static class MapsterSetup
{
    public static void AddMapsterSetup()
    {
        TypeAdapterConfig.GlobalSettings.Default
            .GetMemberName(member => member.GetCustomAttributes(true)
                .OfType<JsonPropertyAttribute>()
                .FirstOrDefault()?.PropertyName); 
    }
}