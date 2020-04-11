using System.Web;
using System.Web.Optimization;

namespace asp.net_project_1 {
    public class BundleConfig {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/styles").Include(
                      "~/Scripts/main.js",
                      "~/Scripts/about.js"));
        }
    }
}
