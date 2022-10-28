using System;

namespace AuthRPolicy.Sample.Domain.DocumentAggregate
{
    public record DocumentId(Guid Id)
    {
        public static DocumentId New() => new(Guid.NewGuid());
        public static DocumentId Empty() => new(Guid.Empty);
    }
}
