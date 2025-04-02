namespace RestaurantGroup.Identity.Domain.Exceptions;

public class InvalidCredentialsException : DomainException
{
    public InvalidCredentialsException()
        : base("Invalid credentials provided.")
    { }
}