// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsLinq.Ast
{
    internal enum Operators
    {
        /// <summary>
        /// +
        /// </summary>
        Add,
        /// <summary>
        /// -
        /// </summary>
        Subtract,
        /// <summary>
        /// *
        /// </summary>
        Multiply,
        /// <summary>
        /// /
        /// </summary>
        Divide,
        /// <summary>
        /// =
        /// </summary>
        Equal,
        /// <summary>
        /// 
        /// </summary>
        NotEqual,
        /// <summary>
        /// 
        /// </summary>
        Modulo,
        /// <summary>
        /// 
        /// </summary>
        Power,
        /// <summary>
        /// 
        /// </summary>
        And,
        /// <summary>
        /// 
        /// </summary>
        Or,
        /// <summary>
        /// 
        /// </summary>
        LessThanOrEqual,
        /// <summary>
        /// 
        /// </summary>
        LessThan,
        /// <summary>
        /// 
        /// </summary>
        GreaterThan,
        /// <summary>
        /// 
        /// </summary>
        GreaterThanOrEqual,
        /// <summary>
        /// Negative value
        /// </summary>
        Negate,
        /// <summary>
        /// 
        /// </summary>
        Not,
        /// <summary>
        /// 
        /// </summary>
        Is,
    }
}
