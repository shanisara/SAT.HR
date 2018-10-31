using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class JSTreeViewModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public string li_attr { get; set; }
        public string a_attr { get; set; }
        public string node_type { get; set; }
        public JSTreeState state { get; set; }
        public List<JSTreeViewModel> children { get; set; }
    }

    public class JSTreeState
    {
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }
    }

    public class TreeViewModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public string state { get; set; }
        public string types { get; set; }
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }
        public string li_attr { get; set; }
        public string a_attr { get; set; }
        public bool children { get; set; }
        public string node_type { get; set; }
        public int node_mpid { get; set; }
    }

}