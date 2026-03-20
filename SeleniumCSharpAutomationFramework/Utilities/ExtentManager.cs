

using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

public class ExtentManager
{
    private static ExtentReports extent;
    private static ExtentSparkReporter reporter;

    public static ExtentReports GetInstance()
    {
        if (extent == null)
        {
            var baseDir = AppContext.BaseDirectory;
            var di = new DirectoryInfo(baseDir);

            for (int i = 0; i < 3; i++)
            {
                di = di.Parent;
            }

            string reportDir = Path.Combine(di.FullName, "Reports");

            if (!Directory.Exists(reportDir))
            {
                Directory.CreateDirectory(reportDir);
            }

            string reportPath = Path.Combine(reportDir, "TestReport.html");
            var reporter = new ExtentSparkReporter(reportPath);
            reporter.Config.DocumentTitle = "Automation Test Report";
            reporter.Config.ReportName = "Selenium C# Test Execution Report"; 

            extent = new ExtentReports();
            extent.AttachReporter(reporter);
        }
        return extent;
    }
}