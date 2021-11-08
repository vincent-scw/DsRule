// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DsRule.UnitTest
{
    public class LinqExtensionsTests
    {
        [Theory]
        [InlineData("Age >= 30", 2)]
        [InlineData("Age >= 20 AND Gender = 'Female'", 1)]
        [InlineData("OnboardDate >= today", 1)]
        internal void LinqParser_ShouldWork(string expression, int matchCount)
        {
            var employees = BuildEmployees();
            var res = employees.Where(expression).Count();

            Assert.Equal(matchCount, res);
        }

        private List<Employee> BuildEmployees()
        {
            var list = new List<Employee> {
                new Employee() {
                    Age = 25,
                    FirstName = "Bob",
                    LastName = "Abc",
                    Gender = Gender.Male,
                    OnboardDate = DateTime.Today,
                    Valid = true
                },
                new Employee() {
                    Age = 30,
                    FirstName = "Sam",
                    LastName = "Walt",
                    Gender = Gender.Male,
                    OnboardDate = DateTime.Today.AddDays(-10),
                    Valid = true
                },
                new Employee() {
                    Age = 45,
                    FirstName = "Rose",
                    LastName = "Walt",
                    Gender = Gender.Female,
                    OnboardDate = DateTime.Today.AddDays(-1000),
                    Valid = true
                }
            };
            
            return list;
        }
    }
}
