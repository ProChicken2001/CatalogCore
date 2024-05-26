using CurrieTechnologies.Razor.SweetAlert2;
using Grpc.Net.Client.Web;
using GrpcCatalogCoreClient;
using GrpcCatalogCoreClient.Protos;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddGrpcClient<ProveedorServ.ProveedorServClient>(op =>
{
    op.Address = new Uri("https://localhost:7241");
}).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));

builder.Services.AddGrpcClient<CategoriaServ.CategoriaServClient>(op =>
{
    op.Address = new Uri("https://localhost:7241");
}).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));

builder.Services.AddGrpcClient<MaterialesServ.MaterialesServClient>(op =>
{
    op.Address = new Uri("https://localhost:7241");
}).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));

builder.Services.AddGrpcClient<TipoUsuarioServ.TipoUsuarioServClient>(op =>
{
    op.Address = new Uri("https://localhost:7241");
}).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));

builder.Services.AddGrpcClient<RolEmpleadoServ.RolEmpleadoServClient>(op =>
{
    op.Address = new Uri("https://localhost:7241");
}).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));

builder.Services.AddGrpcClient<UsuariosServ.UsuariosServClient>(op =>
{
    op.Address = new Uri("https://localhost:7241");
}).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));

builder.Services.AddGrpcClient<PersonalServ.PersonalServClient>(op =>
{
    op.Address = new Uri("https://localhost:7241");
}).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));

builder.Services.AddGrpcClient<PrestamosServ.PrestamosServClient>(op =>
{
    op.Address = new Uri("https://localhost:7241");
}).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
