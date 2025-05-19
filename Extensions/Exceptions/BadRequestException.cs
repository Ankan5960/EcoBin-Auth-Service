namespace EcoBin_Auth_Service.Extensions.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }
}