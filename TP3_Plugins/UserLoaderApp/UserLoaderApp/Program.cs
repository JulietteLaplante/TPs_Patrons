using BaseUserLoaderPlugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UserLoaderApp
{
    class Program
    {
        /* TODO: lire les plugins depuis le dossier plugins
         * 
         * Dans la consigne, les plugins sont censés être placé dans le dossier plugins, mais le code crash lorsque c'est le cas.
         * Il suffit de décommenter la ligne 28 pour voir le changement.
         */

        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 1 && args[0] == "/d")
                {
                    Console.WriteLine("Waiting for any key...");
                    Console.ReadLine();
                }


                // Load commands from plugins.
                string[] pluginPaths = Directory.GetFiles("plugins", "*.dll");
                    // { @"..\JSONUserLoaderPlugin\bin\Debug\netcoreapp3.1\JSONUserLoaderPlugin.dll"};


                IEnumerable<IUserLoader> userLoaders = pluginPaths.SelectMany(pluginPath =>
                {
                    Console.WriteLine("load plugin :" + pluginPath);
                    Assembly pluginAssembly = LoadPlugin(pluginPath);
                    return CreateLoader(pluginAssembly);
                }).ToList();


                if (args.Length == 0)
                {
                    // Output the loaded commands.
                    Console.WriteLine("Help - use this program with one of the following parameters: ");
                    foreach (IUserLoader userLoader in userLoaders)
                    {
                        Console.WriteLine($"{userLoader.Name}\t - {userLoader.Description}");
                    }
                }
                else
                {
                    foreach (string commandName in args)
                    {
                        Console.WriteLine($"-- {commandName} --");

                        // Execute the loader with the name passed as an argument.
                        IUserLoader userLoader = userLoaders.FirstOrDefault(c => c.Name == commandName);
                        if (userLoader == null)
                        {
                            Console.WriteLine("No such command is known.");
                            return;
                        }

                        List<User> users = userLoader.LoadUsers();
                        Console.WriteLine();

                        // Display results
                        Console.WriteLine("List of users found:");

                        for (int i = 0; i < users.Count; i++)
                        {
                            User user = users[i];
                            Console.WriteLine($"User #{i + 1}: {user.FirstName} {user.LastName} [{user.MailAddress}]");
                        }

                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Press any key to close this window . . . ");
            ConsoleKeyInfo cki = Console.ReadKey();
        }


        /// <summary>
        /// Code from Microsoft to load a plugin
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        static Assembly LoadPlugin(string relativePath)
        {
            // Navigate up to the solution root
            /*string root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));*/
            string root = Directory.GetCurrentDirectory();

            string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            Console.WriteLine($"Loading userLoaders from: {pluginLocation}");
            PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        /// <summary>
        /// Code from Microsoft to create object from Assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        static IEnumerable<IUserLoader> CreateLoader(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IUserLoader).IsAssignableFrom(type))
                {
                    IUserLoader result = Activator.CreateInstance(type) as IUserLoader;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements IUserLoader in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }

        }
    }
}
