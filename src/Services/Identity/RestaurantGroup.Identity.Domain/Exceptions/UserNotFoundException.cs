namespace RestaurantGroup.Identity.Domain.Exceptions;

public class UserNotFoundException : DomainException
{
    public UserNotFoundException(string identifier)
        : base($"User with identifier {identifier} was not found.")
    { }

    public UserNotFoundException(Guid id)
        : base($"User with ID {id} was not found.")
    { }
}