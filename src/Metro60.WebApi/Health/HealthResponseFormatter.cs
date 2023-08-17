using System.Text;
using System.Text.Json;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Metro60.WebApi.Health;

public static class HealthResponseFormatter
{
    public static Task WriteResponse(HttpContext context, HealthReport result)
    {
        context.Response.ContentType = "application/json; charset=utf-8";
        var options = new JsonWriterOptions { Indented = true };
        using var utf8Json = new MemoryStream();
        using (var jsonWriter = new Utf8JsonWriter(utf8Json, options))
        {
            jsonWriter.WriteStartObject();
            jsonWriter.WriteString("status", result.Status.ToString());
            jsonWriter.WriteStartObject("results");
            foreach (var entry in result.Entries)
            {
                jsonWriter.WriteStartObject(entry.Key);
                var utf8JsonWriter2 = jsonWriter;
                var healthReportEntry = entry.Value;
                var str1 = healthReportEntry.Status.ToString();

                utf8JsonWriter2.WriteString("status", str1);
                var utf8JsonWriter3 = jsonWriter;
                healthReportEntry = entry.Value;
                var description = healthReportEntry.Description;

                utf8JsonWriter3.WriteString("description", description);
                healthReportEntry = entry.Value;
                if (healthReportEntry.Exception != null)
                {
                    var utf8JsonWriter4 = jsonWriter;
                    healthReportEntry = entry.Value;
                    var str2 = healthReportEntry.Exception.ToString();
                    utf8JsonWriter4.WriteString("exception", str2);
                }

                jsonWriter.WriteStartObject("data");
                healthReportEntry = entry.Value;
                foreach (var keyValuePair in healthReportEntry.Data)
                {
                    jsonWriter.WritePropertyName(keyValuePair.Key);
                    var writer = jsonWriter;
                    var obj = keyValuePair.Value;
                    var inputType = keyValuePair.Value?.GetType();
                    if ((object)inputType == null)
                    {
                        inputType = typeof(object);
                    }

                    JsonSerializer.Serialize(writer, obj, inputType);
                }

                jsonWriter.WriteEndObject();
                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndObject();
            jsonWriter.WriteEndObject();
        }

        var text = Encoding.UTF8.GetString(utf8Json.ToArray());

        return context.Response.WriteAsync(text);
    }
}
