using DotnetBoilerPlate.Shared.Interfaces;
using StronglyTypedIds;

[assembly: StronglyTypedIdDefaults(Template.Guid, "guid-efcore")]

namespace DotnetBoilerPlate.Shared.Types;

[StronglyTypedId]
public partial struct UserId : IGuid
{
    public static implicit operator UserId(Guid guid)
    {
        return new UserId(guid);
    }
}