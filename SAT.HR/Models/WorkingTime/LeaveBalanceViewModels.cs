using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class LeaveBalanceViewModel
    {
        public int RowNumber { get; set; }

        public int? UserID { get; set; }

        public int? LeaveYear { get; set; }

        public decimal? IDCard { get; set; }

        public string FullNameTh { get; set; }

        public int LevID { get; set; }

        public int LevYear { get; set; }

        public string LevName { get; set; }

        public DateTime? LevStartDate { get; set; }

        public DateTime? LevEndDate { get; set; }

        public decimal? LevMax { get; set; }

        public decimal? LevStandard { get; set; }

        public decimal? LevUsed { get; set; }

        public decimal? LevBalance { get; set; }

        public List<LeaveBalanceViewModel> ListLeaveBalance { get; set; }


    }

}