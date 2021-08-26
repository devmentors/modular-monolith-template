using System.Collections.Generic;

namespace Modular.Modules.Users.Core.Entities
{
    internal class Role
    {
        public string Name { get; set; }
        public IEnumerable<string> Permissions { get; set; }
        public IEnumerable<User> Users { get; set; }

        public static string Default => User;
        public const string User = "user";
        public const string Admin = "admin";
    }
}