using System;
using System.Threading;
using WebDriverDemo.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace WebDriverDemo.Libraries.Wrappers
{
    public static class AssertWrapper
    {
        private static readonly AsyncLocal<ITestOutputHelper> _log = new AsyncLocal<ITestOutputHelper>();

        public static void SetOutputHelper(ITestOutputHelper output)
        {
            _log.Value = output;
        }

        public static void Equal<T>(T expected, T actual)
        {
            try
            {
                Assert.Equal(expected, actual);
            }
            catch (Exception ex)
            {
                _log.Value.ResultDescription("'Assert.Equal' Failed:");
                _log.Value.ResultDescription($"Expected = {expected}");
                _log.Value.ResultDescription($"Actual =   {actual}");

                throw;
            }
        }

        public static void True(bool condition)
        {
            try
            {
                Assert.True(condition);
            }
            catch (Exception ex)
            {
                _log.Value.ResultDescription("'Assert.True' Failed:");
                _log.Value.ResultDescription($"Expected = True");
                _log.Value.ResultDescription($"Actual =   {condition}");

                throw;
            }
        }

        public static void False(bool condition)
        {
            try
            {
                Assert.False(condition);
            }
            catch (Exception ex)
            {
                _log.Value.ResultDescription("'Assert.False' Failed:");
                _log.Value.ResultDescription($"Expected = False");
                _log.Value.ResultDescription($"Actual =   {condition}");

                throw;
            }
        }
    }
}