namespace RestaurantGroup.Identity.Domain.Exceptions;

public class RoleNotFoundException : DomainException
{
    public RoleNotFoundException(string name)
        : base($"Role with name {name} was not found.")
    { }

    public RoleNotFoundException(Guid id)
        : base($"Role with ID {id} was not found.")
    { }
}