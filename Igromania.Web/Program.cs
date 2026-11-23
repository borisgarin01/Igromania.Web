using Smidge;
using Umbraco.Cms.Core.Services;

public partial class Program
{
    private static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.CreateUmbracoBuilder()
            .AddBackOffice()
            .AddWebsite()
            .AddDeliveryApi()
            .AddComposers()
            .Build();

        builder.Services.AddSmidge(builder.Configuration.GetSection("smidge"));

        WebApplication app = builder.Build();

        await app.BootUmbracoAsync();

        app.UseUmbraco()
            .WithMiddleware(u =>
            {
                u.UseBackOffice();
                u.UseWebsite();
            })
            .WithEndpoints(u =>
            {
                u.UseBackOfficeEndpoints();
                u.UseWebsiteEndpoints();
            });

        app.UseSmidge();

        await app.RunAsync();
    }
}