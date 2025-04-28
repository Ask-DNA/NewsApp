var builder = WebApplication.CreateBuilder(args);
NewsApp.Server.WebAPI.ConfigurationManager.ConfigureServices(builder);

var app = builder.Build();
NewsApp.Server.WebAPI.ConfigurationManager.ConfigureApp(app);

app.Run();