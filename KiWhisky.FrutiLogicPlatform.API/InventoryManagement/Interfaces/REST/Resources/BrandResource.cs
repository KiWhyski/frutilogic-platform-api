namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
///     Record that represents a brand resource.
///     It is used to transfer brand data between the API and the client.
/// </summary>
public record BrandResource(
        string Name
    );
