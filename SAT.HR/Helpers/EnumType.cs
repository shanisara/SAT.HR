using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Helpers
{
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }

    public class EnumType
    {
        public enum LeaveStatus
        {
            Waiting = 1,
            Approved = 2,
            Rejected = 3,
            Canceled = 4
        }

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