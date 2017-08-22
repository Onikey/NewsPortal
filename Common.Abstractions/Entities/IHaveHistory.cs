using System;

namespace Common.Abstractions.Entities
{
    public interface IHaveHistory : IHaveCreationDate
    {
        DateTime LastModifiedDate { get; }
    }
}