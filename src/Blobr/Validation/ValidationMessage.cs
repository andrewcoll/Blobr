using System;

namespace Blobr.Validation
{
    public class ValidationMessage
    {
        private readonly ValidationMessageSeverity severity;
        private readonly string message;

        /// <summary>
        /// Create a new instance of ValidationMessage
        /// </summary>
        /// <param name="message">The validation message</param>
        /// <param name="severity">The severity</param>
        public ValidationMessage(string message, ValidationMessageSeverity severity)
        {
            if(string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.message = message;
            this.severity = severity;
        }

        /// <summary>
        /// The validation message
        /// </summary>
        /// <returns></returns>
        public string Message
        {
            get
            {
                return this.message;
            }
        }

        /// <summary>
        /// The severity of the validation message
        /// </summary>
        /// <returns></returns>
        public ValidationMessageSeverity Severity
        {
            get
            {
                return this.severity;
            }
        }
    }
}