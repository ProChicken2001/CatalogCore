using GrpcCatalogCoreServer.Models;
using GrpcCatalogCoreServer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

builder.Services.AddDbContext<CatalogCoreDbContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("QuerySql")));

var app = builder.Build();

app.UseRouting();
app.UseCors();
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

// Configure the HTTP request pipeline.
app.MapGrpcService<ProveedorService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<CategoriaService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<MaterialService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<TipoUsuarioService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<RolEmpleadoService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<UsuarioService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<PersonalService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<PrestamosService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
