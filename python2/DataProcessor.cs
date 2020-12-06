using System.Diagnostics;

namespace PythonProcessor
{
    public class DataProcessor
	{
		public static void DetectOutliers(out string errors, out string results)
		{
            // 1) Create Process Info
            var psi = new ProcessStartInfo
            {
                FileName = @"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python37_64\python.exe"
            };

            // 2) Provide script
            var script = @"C:\Users\admin\Desktop\python2\outliers.py";

            psi.Arguments = $"\"{script}\"";

            // 3) Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // 4) Execute process and get output
            errors = "";
            results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }
        }
	}
}
