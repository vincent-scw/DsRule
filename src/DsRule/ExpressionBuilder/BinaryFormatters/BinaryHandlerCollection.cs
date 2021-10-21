// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace DsRule.ExpressionBuilder.BinaryFormatters
{
    public class BinaryHandlerCollection
    {
        public static IReadOnlyCollection<IBinaryFormatter> GetHandlers()
        {
            return new List<IBinaryFormatter> 
            {
                new ChangeTypeFormatter(),
            };
        }
    }
}
