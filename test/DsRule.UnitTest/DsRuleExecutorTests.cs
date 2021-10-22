// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Xunit;

namespace DsRule.UnitTest
{
    public class DsRuleExecutorTests
    {
        [Theory]
        [InlineData("1 + 1", 2)]
        [InlineData("1.1 + 0.9", 2)]
        [InlineData("1.1 - 0.9", 0.2)]
        [InlineData("1.1 * 2", 2.2)]
        [InlineData("9.3 / 3", 3.1)]
        public void CalculationTest(string input, double output)
        {
            var result = DsRuleExecutor.Execute<double>(input);
            Assert.Equal(output, result);
        }

        
    }
}
