using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Helpers
{
    public class EnumType
    {
        public static class StatusName
        {
            public static string Active = "ใช้งาน";
            public static string NotActive = "ไม่ใช้งาน";
        }

        public static class UserLogin
        {
            public const string SESSION_LOGIN = "USER_LOGIN";
            public const string SESSION_MENU = "USER_MENU";
        }
    }
}