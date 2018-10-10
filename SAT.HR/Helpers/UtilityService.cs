using SAT.HR.Data.Repository;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace SAT.HR.Helpers
{
    public class UtilityService
    {
        private static HttpSessionState CurrentSession
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        private static T GetSessionValue<T>(string name)
        {
            return (T)CurrentSession[name];
        }

        private static List<T> GetSessionList<T>(string name)
        {
            return (List<T>)CurrentSession[name];
        }

        private static string GetSessionValueString(string name)
        {
            return GetSessionValue<string>(name);
        }

        private static void SetSessionValue(string name, object value)
        {
            CurrentSession[name] = value;
        }

        public static void ClearSession()
        {
            CurrentSession.Clear();
        }

        public static UserProfile User
        {
            get
            {
                return GetSessionValue<UserProfile>("USER");
            }
            set
            {
                SetSessionValue("USER", value);
            }
        }
    }

    public class SysConfig
    {
        public static string SMTPSERVER
        {
            get
            {
                return SysConfigRepository.GetKeyValue("SMTPSERVER");
            }
        }

        public static string SMTPUSER
        {
            get
            {
                return SysConfigRepository.GetKeyValue("SMTPUSER");
            }
        }

        public static string SMTPPORT
        {
            get
            {
                return SysConfigRepository.GetKeyValue("SMTPPORT");
            }
        }

        public static string SMTPPASS
        {
            get
            {
                return SysConfigRepository.GetKeyValue("SMTPPASS");
            }
        }

        public static string SMTPMAILSENDER
        {
            get
            {
                return SysConfigRepository.GetKeyValue("SMTPMAILFROM");
            }
        }

        public static string PathUploadAvatar
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathUploadAvatar");
            }
        }
    }
 }