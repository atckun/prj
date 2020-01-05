using System;
using System.Diagnostics.CodeAnalysis;
using AbstractProject.Implementations.Exceptions;

namespace AbstractProject.Domain.Tables
{
    [SuppressMessage(category: "ReSharper", checkId: "AutoPropertyCanBeMadeGetOnly.Local")]
    public class ItemEntity
    {
        public ItemEntity(
            string title,
            string description) : this()
        {
            ChangeTitle(title: title);
            ChangeDescription(description: description);
        }

        private ItemEntity()
        {
            // Default value for creationHistory property
            CreationHistory = DateTimeOffset.UtcNow;
        }
        
        /// <summary>
        ///     Unique identificator
        /// </summary>
        public long Id { get; private set; }
        
        /// <summary>
        ///     Item title
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        ///     Method to change title property
        /// </summary>
        /// <param name="title">item title</param>
        /// <exception cref="ArgumentException"></exception>
        public void ChangeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(value: title))
                throw new ArgumentException(message: "Value cannot be null or whitespace.", paramName: nameof(title));
            if (!title.IsNormalized())
                throw new NotNormalizedException(nameof(title));

            Title = title;
        }
        
        /// <summary>
        ///     Item description
        /// </summary>
        public string Description { get; private set; }
        
        /// <summary>
        ///     Method to change description property
        /// </summary>
        /// <param name="description">item description</param>
        /// <exception cref="ArgumentException"></exception>
        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(value: description))
                throw new ArgumentException(message: "Value cannot be null or whitespace.", paramName: nameof(description));
            if (!description.IsNormalized())
                throw new NotNormalizedException(nameof(description));

            Description = description;
        }
        
        /// <summary>
        ///     System creation history
        /// </summary>
        /// <remarks>create in Universal Coordinated Universal Time (UTC)</remarks>
        public DateTimeOffset CreationHistory { get; private set; }
    }
}