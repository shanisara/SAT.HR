using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class TreeViewModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public string types { get; set; }
        public string li_attr { get; set; }
        public string a_attr { get; set; }
        public bool children { get; set; }
        public bool is_po { get; set; }
        //public int node_mpid { get; set; }
        public TreeStateViewModel state { get; set; }
    }

    public class TreeStateViewModel
    {
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }
    }

    //public class JSTreeViewModel
    //{
    //    public string id { get; set; }
    //    public string text { get; set; }
    //    public string icon { get; set; }
    //    public string li_attr { get; set; }
    //    public string a_attr { get; set; }
    //    public string node_type { get; set; }
    //    public TreeStateViewModel state { get; set; }
    //    public List<JSTreeViewModel> children { get; set; }
    //}
}