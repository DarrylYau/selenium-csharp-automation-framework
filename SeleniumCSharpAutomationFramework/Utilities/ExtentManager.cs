

using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Reflection.Metadata;

public class ExtentManager
{
    public static string ReportDir { get; private set; }
    private static ExtentReports extent;
    
    public static ExtentReports GetInstance()
    {
        if (extent == null)
        {
            //Start from base directory
            var dir = new DirectoryInfo(AppContext.BaseDirectory);

            //Move up until we find the project root (where .csproj is located)
            while (dir != null && !dir.GetFiles("*.csproj").Any())
            {
                dir = dir.Parent;
            }

            string projectRoot = dir?.FullName ?? AppContext.BaseDirectory;
            ReportDir = Path.Combine(projectRoot, "Reports");

            
            if (!Directory.Exists(ReportDir))
            {
                Directory.CreateDirectory(ReportDir);
            }

            string reportPath = Path.Combine(ReportDir, "TestReport.html");

            var reporter = new ExtentSparkReporter(reportPath);
            reporter.Config.DocumentTitle = "Automation Test Report";
            reporter.Config.ReportName = "Selenium C# Test Execution Report"; 

            extent = new ExtentReports();
            extent.AttachReporter(reporter);
        }
        return extent;
    }
}