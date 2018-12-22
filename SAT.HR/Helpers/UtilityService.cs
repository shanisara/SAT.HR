using SAT.HR.Data.Repository;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
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

        private static L GetSessionValueList<L>(string name)
        {
            return (L)CurrentSession[name];
        }

        private static string GetSessionValueString(string name)
        {
            return GetSessionValue<string>(name);
        }

        public static void SetSessionValue(string name, object value)
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
                var obj = GetSessionValue<UserProfile>("USER");

                if (obj == null)
                {
                    try
                    {
                        if (HttpContext.Current.Request.IsAuthenticated)
                        {
                            var userid = HttpContext.Current.User.Identity.Name.Split(',');
                            obj = new EmployeeRepository().LoginByID(int.Parse(userid[0]));
                            UtilityService.User = obj;
                        }
                    }
                    catch
                    {
                        return null;

                    }
                }
                return obj;
            }
            set
            {
                SetSessionValue("USER", value);
            }
        }

        public static DateTime ConvertDateThai(DateTime? date)
        {
            DateTime dateThai = Convert.ToDateTime(date.Value.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")));
            return dateThai;
        }

        public static DateTime ConvertDateThai2Eng(DateTime? date)
        {
            int dd = date.Value.Day;
            int mm = date.Value.Month;
            int yy = date.Value.Year - 543;
            DateTime newDate = Convert.ToDateTime(yy + "/" + mm + "/" + dd, new System.Globalization.CultureInfo("en-GB"));
            return newDate;
        }

        public static DateTime ConvertDate2Save(DateTime? date)
        {
            int dd = date.Value.Day;
            int mm = date.Value.Month;
            int yy = date.Value.Year;
            DateTime newDate = Convert.ToDateTime(yy + "/" + mm + "/" + dd, new System.Globalization.CultureInfo("en-GB"));
            return newDate;
        }

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (var item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }

    public class SysConfig
    {
        #region SMTP Server Mail

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
        public static int SMTPPORT
        {
            get
            {
                return Convert.ToInt32(SysConfigRepository.GetKeyValue("SMTPPORT"));
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

        #endregion 

        #region  PathUpload & PathDownload

        public static string ApplicationRoot
        {
            get
            {
                return SysConfigRepository.GetKeyValue("ApplicationRoot");
            }
        }


        public static string PathUploadUserAvatar
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathUploadUserAvatar");
            }
        }
        public static string PathUploadUserAttach
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathUploadUserAttach");
            }
        }
        public static string PathUploadUserEducation
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathUploadUserEducation");
            }
        }
        public static string PathUploadUserInsignia
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathUploadUserInsignia");
            }
        }
        public static string PathUploadUserPosition
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathUploadUserPosition");
            }
        }
        public static string PathUploadLevelTransfer
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathUploadLevelTransfer");
            }
        }
        public static string PathUploadPositionTransfer
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathUploadPositionTransfer");
            }
        }
        public static string PathUploadCourse
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathUploadCourse");
            }
        }
        public static string PathUploadLeaveFile
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathUploadLeaveFile");
            }
        }
        public static string PathUploadSalaryIncrease
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathUploadSalaryIncrease");
            }
        }
        public static string PathUploadBonusCalculator
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathUploadBonusCalculator");
            }
        }


        public static string PathDownloadUserAvatar
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathDownloadUserAvatar");
            }
        }
        public static string PathDownloadUserAttach
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathDownloadUserAttach");
            }
        }
        public static string PathDownloadLevelTransfer
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathDownloadLevelTransfer");
            }
        }
        public static string PathDownloadPositionTransfer
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathDownloadPositionTransfer");
            }
        }
        public static string PathDownloadUserInsignia
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathDownloadUserInsignia");
            }
        }
        public static string PathDownloadUserEducation
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathDownloadUserEducation");
            }
        }
        public static string PathDownloadUserPosition
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathDownloadUserPosition");
            }
        }
        public static string PathDownloadCourse
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathDownloadCourse");
            }
        }
        public static string PathDownloadLeaveFile
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathDownloadLeaveFile");
            }
        }
        public static string PathDownloadSalaryIncrease
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathDownloadSalaryIncrease");
            }
        }
        public static string PathDownloadBonusCalculator
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathDownloadBonusCalculator");
            }
        }

        #endregion

        #region Report

        public static string PathUploadReport
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathUploadReportFile");
            }
        }
        public static string PathDownloadReport
        {
            get
            {
                return SysConfigRepository.GetKeyValue("PathDownloadReportFile");
            }
        }


        public static string ServerName
        {
            get
            {
                return SysConfigRepository.GetKeyValue("ReportServerName");
            }
        }
        public static string DatabaseName
        {
            get
            {
                return SysConfigRepository.GetKeyValue("ReportDatabaseName");

            }

        }
        public static string UserName
        {
            get
            {
                return SysConfigRepository.GetKeyValue("ReportUserID");
            }

        }
        public static string Password
        {
            get
            {
                return SysConfigRepository.GetKeyValue("ReportPassword");
                return ConfigurationManager.AppSettings["ReportPassword"];
            }
        }

        #endregion 
    }

    public class Logs
    {
        private static string GetTempPath()
        {
            string path = SysConfigRepository.GetKeyValue("PathLog"); //System.Environment.GetEnvironmentVariable("TEMP");
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
            if (!path.EndsWith("\\")) path += "\\";
            return path;
        }

        public static void LogMessageToFile(string msg)
        {
            string logPath = "Logs_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
            System.IO.StreamWriter sw = System.IO.File.AppendText(GetTempPath() + logPath);
            try
            {
                string logLine = System.String.Format( "{0:G}: {1}.", System.DateTime.Now, msg);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }
    }

    public class Mails
    {
        public static bool SentMail(MailMessage msg)
        {
            try
            {
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = SysConfig.SMTPSERVER; //noreplysat2019@gmail.com/satsat2019
                    smtp.Port = SysConfig.SMTPPORT;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    if (!string.IsNullOrEmpty(smtp.Host) && smtp.Host.Equals("smtp.gmail.com"))
                    {
                        smtp.EnableSsl = true;
                        smtp.Credentials = new NetworkCredential(SysConfig.SMTPUSER, SysConfig.SMTPPASS);
                    }
                    smtp.Send(msg);

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsValidEmail(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

    }

    public class ValidHeader
    {
        public static bool ValidateColumnHeader(DataTable dtData, string[] columnHeader)
        {
            if (dtData == null
                || columnHeader == null
                || dtData.Columns.Count < columnHeader.Length)
                return false;

            for (int i = 0; i < columnHeader.Length; i++)
            {
                if (String.Compare(columnHeader[i], dtData.Columns[i].ColumnName, StringComparison.OrdinalIgnoreCase) != 0)
                    return false;
            }


            return true;
        }

    }

}