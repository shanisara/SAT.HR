using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class ManPowerRepository
    {
        //public List<ManPowerViewModel> GetDivisionManPower(int typeid)
        //{
        //    using (SATEntities db = new SATEntities())
        //    {
        //        var list = db.vw_Man_Power.Where(w => w.TypeID == typeid)
        //                    .Select(s => new ManPowerViewModel()
        //                    {
        //                        MpID = s.MpID,
        //                        MpCode = s.MpCode,
        //                        PoID = s.PoID,
        //                        PoName = s.PoName,
        //                        FullNameTh = s.FullNameTh
        //                    }).OrderBy(x => x.MpCode).ToList();

        //        return list;
        //    }
        //}

        //public List<ManPowerViewModel> GetDepartmentManPower(int typeid, int divid)
        //{
        //    using (SATEntities db = new SATEntities())
        //    {
        //        var list = db.vw_Man_Power.Where(w => w.TypeID == typeid && w.DivID == divid)
                           
        //                   .Select(s => new ManPowerViewModel()
        //                    {
        //                        MpID = s.MpID,
        //                        MpCode = s.MpCode,
        //                        PoID = s.PoID,
        //                        PoName = s.PoName,
        //                        FullNameTh = s.FullNameTh
        //                    }).OrderBy(x => x.MpCode).ToList();

        //        return list;
        //    }
        //}

        //public List<ManPowerViewModel> GetSectionManPower(int typeid, int divid, int depid)
        //{
        //    using (SATEntities db = new SATEntities())
        //    {
        //        var list = db.vw_Man_Power.Where(w => w.TypeID == typeid && w.DivID == divid)

        //                   .Select(s => new ManPowerViewModel()
        //                   {
        //                       MpID = s.MpID,
        //                       MpCode = s.MpCode,
        //                       PoID = s.PoID,
        //                       PoName = s.PoName,
        //                       FullNameTh = s.FullNameTh
        //                   }).OrderBy(x => x.MpCode).ToList();

        //        return list;
        //    }
        //}

        public List<ManPowerViewModel> GetPositionManPower(int? typeid, int? divid, int? depid, int? secid)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_Man_Power.ToList();

                if (typeid.HasValue)
                {
                    if(typeid == 1)
                        data = data.Where(w => w.TypeID == typeid || w.TypeID == null).ToList();
                    else
                        data = data.Where(w => w.TypeID == typeid).ToList();
                }
                

                if (divid != null)
                    data = data.Where(w => w.DivID == divid).ToList();

                if (depid != null)
                    data = data.Where(w => w.DepID == depid).ToList();

                if (secid != null)
                    data = data.Where(w => w.SecID == secid).ToList();

                var list = data
                            .Select(s => new ManPowerViewModel()
                            {
                                MpID = s.MpID,
                                MpCode = s.MpCode,
                                PoID = s.PoID,
                                PoName = s.PoName,
                                FullNameTh = s.FullNameTh
                            }).OrderBy(x => x.MpCode).ToList();

                return list;
            }
        }


        //public List<ManPowerViewModel> GetPositionByDep(int? typeid, int? depid)
        //{
        //    using (SATEntities db = new SATEntities())
        //    {
        //        var list = db.vw_Man_Power.Where(m => m.TypeID == typeid && m.DepID == depid)
        //            .Select(s => new ManPowerViewModel()
        //            {
        //                MpID = s.MpID,
        //                MpCode = s.MpCode,
        //                PoID = s.PoID,
        //                PoName = s.PoName,
        //                FullNameTh = s.FullNameTh
        //            }).OrderBy(x => x.MpCode).ToList();
        //        return list;
        //    }
        //}


    }
}