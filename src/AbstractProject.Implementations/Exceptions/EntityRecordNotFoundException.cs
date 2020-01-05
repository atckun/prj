using System;

namespace AbstractProject.Implementations.Exceptions
{
    [Serializable]
    public sealed class EntityRecordNotFoundException : Exception
    {
        public EntityRecordNotFoundException(string entityName, object entityKey)
        : base(message: $"Record in entity \"{entityName}\" ({entityKey}) not found.")
        {
        }
    }
}