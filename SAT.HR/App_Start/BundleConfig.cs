using System.Web;
using System.Web.Optimization;

namespace SAT.HR
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/assets/js/core/jquery.min.js",
                        "~/Content/assets/js/core/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/assets/js/plugins/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/assets/js/core/bootstrap-material-design.min.js",
                      "~/Content/assets/js/plugins/perfect-scrollbar.jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugin").Include(
                        "~/Content/assets/js/plugins/moment.min.js",
                        "~/Content/assets/js/plugins/sweetalert2.js",
                        "~/Content/assets/js/plugins/jquery.bootstrap-wizard.js",
                        "~/Content/assets/js/plugins/jquery.dataTables.min.js",
                        "~/Content/assets/js/plugins/bootstrap-datetimepicker.min.js",
                        "~/Content/assets/js/plugins/bootstrap-notify.js",
                        "~/Content/assets/js/plugins/bootstrap-tagsinput.js",
                        "~/Content/assets/js/plugins/jasny-bootstrap.min.js",
                        "~/Content/assets/js/plugins/bootstrap-select.min.js",
                        "~/Content/assets/js/jstree/jstree.js",
                        "~/Content/assets/js/plugins/nouislider.min.js",
                        "~/Content/assets/js/plugins/arrive.min.js",
                        "~/Content/assets/js/plugins/chartist.min.js",
                        "~/Content/assets/js/nprogress/nprogress.js",
                        "~/Content/assets/js/plugins/jquery.blockUI.js",
                        "~/Content/assets/js/plugins/accounting.js",
                        "~/Content/assets/js/material-dashboard.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/assets/css/material-dashboard.css",
                      "~/Content/assets/js/jstree/themes/default/style.css",
                      //"~/Content/assets/demo/demo.css",
                      "~/fonts/font-awesome/css/font-awesome.min.css",
                      "~/Content/assets/js/nprogress/nprogress.css",
                      "~/fonts/font-materialicons/fonts/material-icons.css",
                      "~/fonts/font-prompt/css/Prompt.css"));
        }
    }
}
