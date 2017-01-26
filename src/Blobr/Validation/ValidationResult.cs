using System;
using System.Collections.Generic;
using System.Linq;

namespace Blobr.Validation
{
    public class ValidationResult
    {
        private readonly ICollection<ValidationMessage> messages;

        public ValidationResult()
        {
            this.messages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Add a new warning message
        /// </summary>
        /// <param name="message">The message</param>
        public void AddWarning(string message)
        {
            if(string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.messages.Add(new ValidationMessage(message, ValidationMessageSeverity.Warning));
        }

        /// <summary>
        /// Add a new error message
        /// </summary>
        /// <param name="message">The message</param>
        public void AddError(string message)
        {
            if(string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.messages.Add(new ValidationMessage(message, ValidationMessageSeverity.Error));
        }

        /// <summary>
        /// Validation Messages
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<ValidationMessage> Messages
        {
            get
            {
                return this.messages.ToList().AsReadOnly();
            }
        }
    }
}