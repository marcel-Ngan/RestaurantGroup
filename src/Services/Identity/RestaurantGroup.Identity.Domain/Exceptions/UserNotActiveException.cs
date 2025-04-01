namespace RestaurantGroup.Identity.Domain.Exceptions
{
    public class UserNotActiveException : DomainException
    {
        public UserNotActiveException(string email)
            : base($"User with email {email} is not active.")
        {
        }
    }
}