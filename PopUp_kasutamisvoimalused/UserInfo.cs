using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondMobileApp.PopUp_kasutamisvoimalused
{
    public static class UserInfo
    {
        public static string UserName { get; set; }
        public static bool IsUserNameValid()
        {
            return !string.IsNullOrEmpty(UserName);
        }
    }
}

