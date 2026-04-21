namespace KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.Exceptions;

/// <summary>
///     This exception is thrown when a Value Object fails validation.
/// </summary>
public class ValueObjectValidationException : Exception
{
    /// <summary>
    ///     The name of the Value Object that caused the exception.
    /// </summary>
    public string RecordName { get; }
    
    /// <summary>
    ///     This constructor creates a new instance of the ValueObjectNullException class.
    ///     It takes the name of the Value Object as a parameter and constructs a message indicating that the Value Object must be a non-empty value.
    /// </summary>
    /// <param name="valueObjectName">
    ///     The name of the Value Object that caused the exception.
    /// </param>
    public ValueObjectValidationException(string valueObjectName)
        : base($"Error trying to validate the Value Object '{valueObjectName}'.")
    {
        RecordName = valueObjectName;
    }

    /// <summary>
    ///     This constructor creates a new instance of the ValueObjectNullException class with additional details.
    /// </summary>
    /// <param name="valueObjectName">
    ///     The name of the Value Object that caused the exception.
    /// </param>
    /// <param name="details">
    ///     The details about why the Value Object is considered invalid.
    /// </param>
    public ValueObjectValidationException(string valueObjectName, string details)
        : base($"Error trying to validate the Value Object '{valueObjectName}': {details}")
    {
        RecordName = valueObjectName;
    }

    /// <summary>
    ///     This constructor creates a new instance of the ValueObjectNullException class with additional details and an inner exception.
    /// </summary>
    /// <param name="valueObjectName">
    ///     The name of the Value Object that caused the exception.
    /// </param>
    /// <param name="details">
    ///     The details about why the Value Object is considered invalid.
    /// </param>
    /// <param name="innerException">
    ///     The inner exception that caused this exception to be thrown.
    /// </param>
    public ValueObjectValidationException(string valueObjectName, string details, Exception innerException)
        : base($"Error trying to validate the Value Object '{valueObjectName}': {details}", innerException)
    {
        RecordName = valueObjectName;
    }
}
