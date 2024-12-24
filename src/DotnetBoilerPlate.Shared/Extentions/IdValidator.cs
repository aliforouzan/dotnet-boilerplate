namespace DotnetBoilerPlate.Shared.Extentions;

public static class IdValidator
{
    public static bool IsValid(int? id)
    {
        return id is > 0;
    }
    
    public static bool IsValid(int id)
    {
        return id > 0;
    }
}