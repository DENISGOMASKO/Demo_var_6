using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_var_6
{
    internal static class ApplicationContext
    {
        public static readonly string _mockPictureName = "picture.png";
        private static User user { get; set; }
        public static void SetUser(User us)
        {
            user = us;
        }
        public static int GetUserRoleID() => user?.RoleId ?? 1;
        public static string GetUserName() => user?.Lfp ?? string.Empty;
    }
}
