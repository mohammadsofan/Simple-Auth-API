using SimpleAuthAPI.Models;

namespace SimpleAuthAPI.Data
{
    public class FakeUserStorage
    {
        public static long Id = 1;
        public static IList<User> Users = new List<User>()
        {
            new User()
            {
                Id = FakeUserStorage.Id++,
                Name = "Admin",
                Email = "Admin@gmail.com",
                Password = "Admin@123",
                Role="Admin"
            }

        };
    }
}
