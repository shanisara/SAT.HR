using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class DocumentNumberRepository
    {
        public static string GetNextNumber(string code)
        {
            string DocNo = "";
            string CurrentNum = "";

            int yCount = 0;
            int mCount = 0;
            int dCount = 0;
            int sCount = 0;

            var data = GetByCode(code);
            string culture = data.Culture;
            int intCurrentNum = (int)data.CurrentNum;
            int LastMonth = (int)data.LastMonth;
            bool ResetMonth = (bool)data.ResetMonth;
            int LastYear = (int)data.LastYear + (culture == "th-TH" ? + 543 : 0);
            bool ResetYear = (bool)data.ResetYear;
            bool ZeroContain = (bool)data.ZeroContain;
            int NumLength = (int)data.NumLength;
            string Prefix = data.Prefix;
            string Suffix = data.Suffix;
            

            int thisYear = Convert.ToInt32(DateTime.Now.ToString("yyyy", new System.Globalization.CultureInfo(culture)));

            CurrentNum = intCurrentNum.ToString();

            if (LastMonth != DateTime.Now.Month)
            {
                LastMonth = Convert.ToInt16(DateTime.Now.Month);
                if (ResetMonth)
                {
                    intCurrentNum = 1;
                    CurrentNum = intCurrentNum.ToString();
                }
            }

            if (LastYear != thisYear)
            {
                LastYear = thisYear;

                if (ResetYear)
                {
                    intCurrentNum = 1;
                    CurrentNum = intCurrentNum.ToString();
                }
            }

            DocNo = Prefix;

            if (DocNo.IndexOf("y") != -1 || DocNo.IndexOf("m") != -1 || DocNo.IndexOf("d") != -1 || DocNo.IndexOf("#") != -1)
            {
                char[] y = DocNo.ToCharArray(0, DocNo.Length);

                for (int i = 0; i < y.Length; i++)
                {
                    if (y[i] == 'y') yCount++;
                    if (y[i] == 'm') mCount++;
                    if (y[i] == 'd') dCount++;
                    if (y[i] == '#') sCount++;
                }
            }

            if (ZeroContain)
            {
                int i = NumLength - intCurrentNum.ToString().Length;

                for (int j = 0; j < i; j++)
                {
                    CurrentNum = CurrentNum.Insert(0, "0");
                }
            }

            if (sCount >= 1 && DocNo.IndexOf("#", DocNo.IndexOf("#"), 1) != -1)
            {
                DocNo = DocNo.Insert(DocNo.IndexOf("#"), CurrentNum);
                DocNo = DocNo.Remove(DocNo.IndexOf("#"), 1);
            }

            if (yCount >= 4 && DocNo.IndexOf("y", DocNo.IndexOf("y") + 3, 1) != -1)
            {
                DocNo = DocNo.Insert(DocNo.IndexOf("y"), LastYear.ToString());
                DocNo = DocNo.Remove(DocNo.IndexOf("y"), 4);
            }
            else if (yCount >= 2 && DocNo.IndexOf("y", DocNo.IndexOf("y") + 1, 1) != -1)
            {
                DocNo = DocNo.Insert(DocNo.IndexOf("y"), LastYear.ToString().Substring(2));
                DocNo = DocNo.Remove(DocNo.IndexOf("y"), 2);
            }

            string dd = DateTime.Now.Day.ToString();
            string mm = LastMonth.ToString();

            if (dd.Length == 1) dd = dd.Insert(0, "0");
            if (mm.Length == 1) mm = mm.Insert(0, "0");

            if (DocNo.IndexOf("d") != -1 && dCount > 1 && DocNo.IndexOf("d", DocNo.IndexOf("d") + 1, 1) != -1)
            {
                DocNo = DocNo.Insert(DocNo.IndexOf("d"), dd);
                DocNo = DocNo.Remove(DocNo.IndexOf("d"), 2);
            }

            if (DocNo.IndexOf("m") != -1 && mCount > 1 && DocNo.IndexOf("m", DocNo.IndexOf("m") + 1, 1) != -1)
            {
                DocNo = DocNo.Insert(DocNo.IndexOf("m"), mm);
                DocNo = DocNo.Remove(DocNo.IndexOf("m"), 2);
            }

            DocNo = DocNo + Suffix;

            intCurrentNum++;

            DocumentNumberViewModel model = new DocumentNumberViewModel();
            model.DocCode = code;
            model.CurrentNum = intCurrentNum;
            model.LastYear = LastYear;
            model.LastMonth = LastMonth;
            UpdateByEntity(model);

            return DocNo;
        }

        public static DocumentNumberViewModel GetByCode(string code)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Document_Number.Where(x => x.DocCode == code).FirstOrDefault();
                DocumentNumberViewModel model = new Models.DocumentNumberViewModel();
                model.ObjectID = data.ObjectID;
                model.DocCode = data.DocCode;
                model.DocName = data.DocName;
                model.Prefix = data.Prefix;
                model.Suffix = data.Suffix;
                model.NumLength = data.NumLength;
                model.CurrentNum = data.CurrentNum;
                model.ZeroContain = data.ZeroContain;
                model.ResetMonth = data.ResetMonth;
                model.ResetYear = data.ResetYear;
                model.LastMonth = data.LastMonth;
                model.LastYear = data.LastYear;
                model.Culture = data.Culture;
                return model;
            }
        }

        public static void UpdateByEntity(DocumentNumberViewModel model)
        {
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var data = db.tb_Document_Number.Single(x => x.DocCode == model.DocCode);
                    data.CurrentNum = data.CurrentNum+1;
                    data.LastYear = data.LastYear;
                    data.LastMonth = data.LastMonth;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }

    }
}