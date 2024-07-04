using CHEExportsService;
var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddGrpc(
options =>
{
options.MaxReceiveMessageSize = 2 * 1024 * 1024; // 2 MB
options.MaxSendMessageSize = 2 * 1024 * 1024; // 2 MB
});

var app = builder.Build();
 
app.MapGrpcService<AdminService>();
app.MapGrpcService<ApplicationSerivce>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");



//app.MapGet("/", () => "Hello World!");

var port = Environment.GetEnvironmentVariable("PORT") ?? "5243";
app.Urls.Add($"http://localhost:{port}");
app.Run();
