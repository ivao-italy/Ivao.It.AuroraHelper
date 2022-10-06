namespace Ivao.It.AuroraHelper.EnavData.Exceptions;

public class EnavDataException : Exception
{
    public EnavDataException()
    {
    }

    public EnavDataException(string? message) : base(message)
    {
    }

    public EnavDataException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public static EnavDataException RegexMissMatch(string fieldName) 
        => new EnavDataException($"Unable to match {fieldName} on the given string value");
}
