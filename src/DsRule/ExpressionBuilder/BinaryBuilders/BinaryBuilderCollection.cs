// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace DsRule.ExpressionBuilder.BinaryBuilders
{
    public class BinaryBuilderCollection
    {
        public static IReadOnlyCollection<IBinaryBuilder> GetBuilders()
        {
            return new List<IBinaryBuilder>() {
                new StringConcatBuilder(),
                new InBuilder(),
                new GenericBuilder()
            };
        }
    }
}
