using System;

namespace GetEventStoreRepository
{
    [Serializable]
    public class AggregateNotFoundException : Exception
    {
        public readonly Guid Id;
        public readonly Type Type;

        public AggregateNotFoundException(Guid id, Type type)
            : base(string.Format("Aggregate '{0}' (type {1}) was not found.", id, type.Name))
        {
            Id = id;
            Type = type;
        }
    }
}