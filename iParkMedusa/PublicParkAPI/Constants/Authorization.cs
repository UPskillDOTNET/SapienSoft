namespace PaxAPI.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Moderator,
            User
        }
        public const string default_username = "admin";
        public const string default_email = "admin@publicpark.com";
        public const string default_password = "Admin123!";
        public const Roles default_role = Roles.User;
    }
}
