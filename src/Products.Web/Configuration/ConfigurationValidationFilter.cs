using System.Configuration;

namespace Products.Web.Configuration;

public class ConfigurationValidationFilter : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return builder =>
        {
            var configuration = builder.ApplicationServices.GetRequiredService<IConfiguration>();

            var baseUrl = configuration["API_Base_Url"];

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ConfigurationErrorsException("API_Base_Url configuration is missing or contains only whitespace.");
            }

            try
            {
                // Attempt to create a Uri to further validate the format
                _ = new Uri(baseUrl);
            }
            catch (UriFormatException ex)
            {
                throw new ConfigurationErrorsException($"API_Base_Url configuration is invalid: {ex.Message}");
            }

            next(builder);
        };
    }
}