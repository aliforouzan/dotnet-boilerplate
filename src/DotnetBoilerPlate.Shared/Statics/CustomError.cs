using Ardalis.Result;

namespace DotnetBoilerPlate.Shared.Statics;

public static class CustomError
{
    public static readonly Result InternalServerError = Result.CriticalError("خطا در پردازش درخواست");
    public static readonly Result DbError = Result.Error("خطا در پردازش درخواست");
    public static readonly Result Unauthorized = Result.Unauthorized("عدم اعتبار مجوز دسترسی");
}