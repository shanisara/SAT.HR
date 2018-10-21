using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class AddressRepository
    {
        public List<ProvinceViewModel> GetProvince()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Province.Select(s => new ProvinceViewModel()
                {
                    ProvinceID = s.ProvinceID,
                    ProvinceName = s.ProvinceName
                }).ToList();
                return list;
            }
        }

        public List<DistrictViewModel> GetDistrict()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_District.Select(s => new DistrictViewModel()
                {
                    DistrictID = s.DistrictID,
                    DistrictName = s.DistrictName,
                    ProvinceID = s.ProvinceID,
                }).ToList();
                return list;
            }
        }

        public List<SubDistrictViewModel> GetSubDistrict()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_SubDistrict.Select(s => new SubDistrictViewModel()
                {
                    SubDistrictID = s.SubDistrictID,
                    SubDistrictName = s.SubDistrictName,
                    DistrictID = s.DistrictID,
                    ProvinceID = s.ProvinceID,
                }).ToList();
                return list;
            }
        }

        public List<CountryViewModel> GetCountry()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Country.Select(s => new CountryViewModel()
                {
                    CountryID = s.CountryID,
                    CountryName = s.CountryName,
                })
                .OrderBy(o => o.CountryName)
                .ToList();
                return list;
            }
        }

    }
}