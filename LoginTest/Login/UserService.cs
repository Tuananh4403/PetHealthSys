namespace LoginTest
{
    public class UserService
    {
        public string LoginUser(User user)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                return "User Name cannot be blank";
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                return "Password cannot be blank";
            }
            if (user.Password.Length < 6)
            {
                return "Password must be at least 6 characters long";
            }
            if (user.UserName == "testuser" && user.Password == "password123")
            {
                return "Login successful";
            }
            else
            {
                return "Invalid username or password";
            }
        }
    }
}