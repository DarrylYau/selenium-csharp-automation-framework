using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class TestDataReader
{
    public static List<LoginData> GetLoginData()
    {
        // Build an absolute path relative to the test binary so the file can be found at runtime.
        string path = Path.Combine(AppContext.BaseDirectory, "TestData", "loginData.json");

        if (!File.Exists(path))
            throw new FileNotFoundException($"Login data file not found at '{path}'. Ensure 'TestData/loginData.json' is included in the project and set to copy to output directory.");

        string json = File.ReadAllText(path);

        // Ensure we never return null (fixes CS8603) and produce a clear error if deserialization fails.
        List<LoginData>? data = JsonConvert.DeserializeObject<List<LoginData>>(json);
        return data ?? throw new InvalidOperationException("Failed to deserialize loginData.json into LoginData.");
    }
}