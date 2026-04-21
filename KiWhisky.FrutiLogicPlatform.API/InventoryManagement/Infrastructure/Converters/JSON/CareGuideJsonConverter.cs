using System.Text.Json;
using System.Text.Json.Serialization;
using KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;

namespace KiWhisky.FrutiLogicPlatform.API.InventoryManagement.Infrastructure.Converters.JSON;

/// <summary>
///     This class is a custom JSON converter for the CareGuide entity.
/// </summary>
public class CareGuideJsonConverter : JsonConverter<CareGuide>
{
    public override CareGuide Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;

        var careGuideId = root.GetProperty("careGuideId").GetString() ??
            throw new JsonException("careGuideId is required");

        var accountId = root.GetProperty("accountId").GetString() ??
            throw new JsonException("accountId is required");

        var productId = root.GetProperty("productId").GetString() ??
            throw new JsonException("productId is required");

        var title = root.GetProperty("title").GetString() ??
            throw new JsonException("title is required");

        var summary = root.TryGetProperty("summary", out var summaryElement) ?
            summaryElement.GetString() ?? string.Empty : string.Empty;

        var recommendedMinTemp = root.TryGetProperty("recommendedMinTemperature", out var minTempElement) ?
            minTempElement.GetDouble() : 0.0;

        var recommendedMaxTemp = root.TryGetProperty("recommendedMaxTemperature", out var maxTempElement) ?
            maxTempElement.GetDouble() : 0.0;

        var recommendedPlaceStorage = root.TryGetProperty("recommendedPlaceStorage", out var placeElement) ?
            placeElement.GetString() ?? string.Empty : string.Empty;

        var generalRecommendation = root.TryGetProperty("generalRecommendation", out var recElement) ?
            recElement.GetString() ?? string.Empty : string.Empty;

        return new CareGuide(
            careGuideId: careGuideId,
            accountId: AccountId.Create(accountId),
            productAssociated: null,
            productId: productId,
            title: title,
            summary: summary,
            recommendedMinTemperature: recommendedMinTemp,
            recommendedMaxTemperature: recommendedMaxTemp,
            recommendedPlaceStorage: recommendedPlaceStorage,
            generalRecommendation: generalRecommendation
        );
    }

    public override void Write(Utf8JsonWriter writer, CareGuide value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("careGuideId", value.CareGuideId);
        writer.WriteString("accountId", value.AccountId.ToString());
        writer.WriteString("productId", value.ProductId);
        writer.WriteString("title", value.Title);

        if (!string.IsNullOrEmpty(value.Summary))
            writer.WriteString("summary", value.Summary);

        writer.WriteNumber("recommendedMinTemperature", value.RecommendedMinTemperature);
        writer.WriteNumber("recommendedMaxTemperature", value.RecommendedMaxTemperature);

        if (!string.IsNullOrEmpty(value.RecommendedPlaceStorage))
            writer.WriteString("recommendedPlaceStorage", value.RecommendedPlaceStorage);

        if (!string.IsNullOrEmpty(value.GeneralRecommendation))
            writer.WriteString("generalRecommendation", value.GeneralRecommendation);

        writer.WriteEndObject();
    }
}

