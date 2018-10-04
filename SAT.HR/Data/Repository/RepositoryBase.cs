using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;

namespace SAT.HR.Data.Repository
{
    public class RepositoryBase
    {
        public enum ParamStyle
        {
            Undefined,
            General,
            LikeBeginWith,
            LikeEndWith,
            LikeContain,
            LikeCustom,
        }

        public List<T> ExecStoredProcedure<T>(string strName, params ObjectParameter[] objParams)
        {
            using (SATEntities db = new SATEntities())
            {
                var objPrepare = new List<ObjectParameter>();

                foreach (var p in objParams)
                {
                    if (p != null)
                    {
                        objPrepare.Add(p);
                    }
                }

                return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.ExecuteFunction<T>(strName, objPrepare.ToArray()).ToList();

            }
        }

        public int ExecStoredProcedureNoReturn(string strName, params ObjectParameter[] objParams)
        {
            using (SATEntities db = new SATEntities())
            {
                var objPrepare = new List<ObjectParameter>();

                foreach (var p in objParams)
                {
                    if (p != null)
                    {
                        objPrepare.Add(p);
                    }
                }

                return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.ExecuteFunction(strName, objPrepare.ToArray());
            }
        }

        public ObjectParameter GenObjectParam(string name, object value, ParamStyle style = ParamStyle.General)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return null;
            }

            object valuePrepare = value;

            switch (style)
            {
                case ParamStyle.Undefined:
                    break;
                case ParamStyle.General:
                    if (valuePrepare is string)
                    {
                        if (valuePrepare.Equals(string.Empty))
                        {
                            valuePrepare = null;
                        }
                    }
                    break;
                case ParamStyle.LikeBeginWith:
                    valuePrepare = this.PrepareSearchParam(valuePrepare) + "%";
                    break;
                case ParamStyle.LikeEndWith:
                    valuePrepare = "%" + this.PrepareSearchParam(valuePrepare);
                    break;
                case ParamStyle.LikeContain:
                    valuePrepare = "%" + this.PrepareSearchParam(valuePrepare) + "%";
                    break;
                case ParamStyle.LikeCustom:
                    valuePrepare = this.PrepareSearchParam(valuePrepare).Replace("*", "%");
                    break;
                default:
                    break;
            }

            if (valuePrepare == null)
            {
                return null;
            }

            return new ObjectParameter(name, valuePrepare);
        }

        public string PrepareSearchParam(object param, ParamStyle style = ParamStyle.General)
        {

            string paramResult = string.Empty;

            //Validate Param
            if (param != null)
            {
                paramResult = param.ToString();
            }

            //Wildcard
            paramResult = paramResult.Replace("%", "[%]").Replace("_", "[_]");

            return paramResult;
        }



    }
}