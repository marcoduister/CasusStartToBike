using System.Web.Optimization;

namespace CasusStartToBike
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            "~/Assets/Scripts/jquery.validate*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Assets/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Contents/css").Include(
                      "~/Assets/Contents/bootstrap.css",
                      "~/Assets/Contents/CMSPanel.css"));
        }
    }
}
