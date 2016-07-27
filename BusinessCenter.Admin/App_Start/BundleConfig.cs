using System.Web;
using System.Web.Optimization;

namespace BusinessCenter.Admin
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
          
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                      "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/css/css").Include(
                        "~/css/app.css",
                      "~/css/customstyles.css",
                      "~/css/normalize.css",
                       "~/css/overrides.css",
                        "~/css/flexslider.css",
                      "~/css/print.css",
                      "~/css/variables.css",
                      "~/css/bootstrap-datetimepicker.min.css"
                   
                     ));

            bundles.Add(new StyleBundle("~/styles/libs").Include(
                   "~/styles/libs/jquery-ui.min.css",
                   "~/styles/libs/jquery-ui.structure.css",
                   "~/styles/libs/jquery-ui.structure.min.css",
                   "~/styles/libs/jquery-ui.theme.css",
                   "~/styles/libs/jquery-ui.theme.min.css"
                ));
            
            bundles.Add(new StyleBundle("~/datatable/datatable").Include(
                        "~/datatable/jquery.dataTables.css"
                     ));
            BundleTable.EnableOptimizations = true;

        }
    }
}
