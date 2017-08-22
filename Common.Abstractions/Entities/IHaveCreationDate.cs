using System;

namespace Common.Abstractions.Entities
{
    public interface IHaveCreationDate
    {
        DateTime CreationDate { get; }
    }
}