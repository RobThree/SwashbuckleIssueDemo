using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace SwashbuckleTest.Infrastructure
{
    public static class ServiceCollectionExtensionMethods
    {
        public static OptionsBuilder<TOptions> AddOptions<TOptions>(this IServiceCollection services, IConfiguration configuration)
            where TOptions : class
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            const string OPTIONSSUFFIX = "Options";

            var name = typeof(TOptions).Name;
            if (!name.EndsWith(OPTIONSSUFFIX, StringComparison.Ordinal) || name.Length <= OPTIONSSUFFIX.Length)
                throw new InvalidOperationException($"Invalid class name '{name}'; name must end with 'Options' to function correctly");

            return services.AddOptions<TOptions>().Bind(configuration.GetSection(name[0..^OPTIONSSUFFIX.Length]));
        }
    }
}
