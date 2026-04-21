namespace KiWhisky.FrutiLogicPlatform.API.ProfileManagement.Domain.Model.ValueObjects;

/// <summary>
/// Value object representing a person's name.
/// </summary>
public record PersonName
{
    /// <summary>
    /// Gets the first name of the person.
    /// </summary>
    public string FirstName { get; init; }
    
    /// <summary>
    /// Gets the last name of the person.
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonName"/> class.
    /// </summary>
    /// <param name="firstName">The first name of the person.</param>
    /// <param name="lastName">The last name of the person.</param>
    /// <exception cref="ArgumentException">Thrown when the firstName is null or whitespace.</exception>
    public PersonName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be null or whitespace.", nameof(firstName));

        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    /// Gets the full name of the person.
    /// </summary>
    /// <returns>The full name as a concatenation of first name and last name.</returns>
    public string GetFullName() => $"{FirstName} {LastName}";
}
