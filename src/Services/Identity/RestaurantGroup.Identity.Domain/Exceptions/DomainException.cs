using System;

namespace RestaurantGroup.Identity.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException()
        { }

        public DomainException(string message) 
            : base(message)
        { }

        public DomainException(string message, Exception innerException) 
            : base(message, innerException)
        { }
    }

    public class UserAlreadyExistsException : DomainException
    {
        public UserAlreadyExistsException(string email)
            : base($"User with email {email} already exists.")
        { }
    }

    public class InvalidCredentialsException : DomainException
    {
        public InvalidCredentialsException()
            : base("Invalid credentials provided.")
        { }
    }

    public class UserNotFoundException : DomainException
    {
        public UserNotFoundException(string identifier)
            : base($"User with identifier {identifier} was not found.")
        { }

        public UserNotFoundException(Guid id)
            : base($"User with ID {id} was not found.")
        { }
    }

    public class RoleNotFoundException : DomainException
    {
        public RoleNotFoundException(string name)
            : base($"Role with name {name} was not found.")
        { }

        public RoleNotFoundException(Guid id)
            : base($"Role with ID {id} was not found.")
        { }
    }
}