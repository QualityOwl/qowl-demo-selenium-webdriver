using Xunit.Abstractions;

namespace WebDriverDemo.Extensions.Logging
{
    public static class XunitExtensions
    {
        private static int _stepNumber = 1;

        public static void StepDescription(this ITestOutputHelper output, string input)
        {
            output.WriteLine($"Step #{(_stepNumber <= 9 ? $"{_stepNumber} " : _stepNumber)} *** {input}");

            _stepNumber++;
        }

        public static void HeaderText(this ITestOutputHelper output, string input = "Test started!")
        {
            var logText = $"======== {input} ========";

            output.WriteLine(logText);
            
            output.WriteLine(new string('=', logText.Length));
        }

        public static void FooterText(this ITestOutputHelper output, string input = "Test completed!")
        {
            var logText = $"======== {input} ========";

            output.WriteLine(new string('=', logText.Length));

            output.WriteLine(logText);
        }
    }
}