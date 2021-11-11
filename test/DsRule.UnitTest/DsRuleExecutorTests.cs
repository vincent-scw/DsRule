// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
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
        [InlineData("-10.3 + 10 - 0.33", -0.63)]
        [InlineData("10 * (1 + 1)", 20)]
        internal void CalculationTest(string input, double output)
        {
            var result = DsRuleExecutor.Execute<double>(input);
            Assert.Equal(output, result);
        }

        [Theory]
        [InlineData("FirstName = 'Vincent'", true)]
        [InlineData("FirstName = 'Vincent' AND Valid = true", true)]
        [InlineData("Manager <> null", true)]
        [InlineData("Manager != null", true)]
        [InlineData("not(Manager = null)", true)]
        [InlineData("Manager.FirstName = 'hehe'", false)]
        [InlineData("Manager.FirstName = 'Mgr'", true)]
        [InlineData("Age > 30 OR Age = 30", true)]
        [InlineData("Age > 30 Or Age < 20", false)]
        [InlineData("Age + 10 = 40", true)]
        [InlineData("FirstName + '.' + LastName = 'Vincent.Any'", true)]
        [InlineData("OnboardDate > '2021-01-01'", true)]
        [InlineData("Gender = 'male'", true)]
        [InlineData("Gender = '0'", true)]
        [InlineData("OnboardDate <= now", true)]
        [InlineData("now >= today", true)]
        [InlineData("Manager.Manager <> null AND Manager.Manager.FirstName = 'Hello'", false)]
        [InlineData("FirstName in ['Vincent', 'John']", true)]
        [InlineData("FirstName in ['Mary', 'John']", false)]
        [InlineData("FirstName in [LastName]", false)] // support variables 
        [InlineData("LastName in [LastName]", true)]
        [InlineData("LastName in [Manager.LastName]", true)]
        internal void EmployeeModelTest(string input, bool output)
        {
            var model = new Employee() 
            {
                FirstName = "Vincent",
                LastName = "Any",
                Age = 30,
                Gender = Gender.Male,
                Manager = new Employee 
                {
                    FirstName = "Mgr",
                    LastName = "Any",
                    Gender = Gender.Female
                },
                Valid = true,
                OnboardDate = new DateTime(2021, 9, 30)
            };

            var result = DsRuleExecutor.Execute<Employee, bool>(model, input);
            Assert.Equal(output, result);
        }
    }
}
