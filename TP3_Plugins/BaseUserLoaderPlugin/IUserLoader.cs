using System.Collections.Generic;

namespace BaseUserLoaderPlugin
{
    public interface IUserLoader
    {
        string Name { get; }
        string Description { get; }

        List<User> LoadUsers();
    }
}
