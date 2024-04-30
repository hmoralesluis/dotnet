using System.Net;
using MassTransit;
using Polly;
using Polly.Extensions.Http;
using SearchService;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<AuctionSvcHttpClient>().AddPolicyHandler(GetRetryPolicy());
builder.Services.AddMassTransit(x => {
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    // x.UsingRabbitMq((context, cfg) => {
    //     cfg.ConfigureEndpoints(context);
        // cfg.ReceiveEndpoint("search-auction-created", e => {
        //     e.UseMessageRetry(r => r.Interval(5, 5));
        //     e.ConfigureConsumer<AuctionCreatedConsumer>(context);
        // });
    // });  

    x.UsingRabbitMq((context, cfg) => {

        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host => {
            host.Username(builder.Configuration.GetValue("RabiitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabiitMq:Password", "guest"));
            
        });
        cfg.ConfigureEndpoints(context);
    }); 
}) ;  

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () => {
    try {
        await DbInitializer.InitDb(app);
    } catch (Exception ex) {
        Console.WriteLine(ex);
    }
});



app.Run();

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() 
=> HttpPolicyExtensions
    .HandleTransientHttpError()
    .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
    .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
