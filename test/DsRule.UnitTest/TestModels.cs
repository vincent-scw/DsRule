// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;

namespace DsRule.UnitTest
{
    class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public Employee Manager { get; set; }
        public bool Valid { get; set; }
        public DateTime? OnboardDate { get; set; }
    }

    enum Gender
    {
        Male,
        Female
    }
}
