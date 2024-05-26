using Grpc.Core;
using GrpcCatalogCoreServer.Models;
using GrpcCatalogCoreServer.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcCatalogCoreServer.Services
{
    public class ProveedorService : ProveedorServ.ProveedorServBase
    {
        private readonly CatalogCoreDbContext _dbcontext;
        private readonly ILogger<ProveedorService> _logger;

        public ProveedorService(CatalogCoreDbContext dbcontext, ILogger<ProveedorService> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public override async Task<ProveedoresReply> getProveedores(EmptyProveedorRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Set<Proveedores>().FromSqlRaw("EXEC sp_GetProveedores").ToListAsync();

                ProveedoresReply reply = new ProveedoresReply();

                reply.Resultado = true;
                reply.Lista.AddRange(r);

                return reply;
            }
            catch (Exception ex)
            {
                return new ProveedoresReply { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<ProveedorReply> getProveedor(ProveedorRequest request, ServerCallContext context)
        {
            try
            {
                var dbRes = await _dbcontext.Set<Proveedores>().
                            FromSqlRaw("EXEC sp_GetProveedor {0}", request.ProveedorId).ToListAsync();

                var r = dbRes.Find(m => m.ProveedorId == request.ProveedorId);

                if (r != null)
                {
                    return new ProveedorReply() { Resultado = true, Registro = r };
                }

                return new ProveedorReply() { Resultado = false };


            }
            catch(Exception ex)
            {
                return new ProveedorReply { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<ProveedorReply> insertProveedor(ProveedorRequest request, ServerCallContext context)
        {
            try
            {
                await _dbcontext.Database.ExecuteSqlRawAsync(
                    "EXEC sp_InsertProveedor {0}, {1}, {2}, {3}, {4}",
                    request.Registro.Nombre,
                    request.Registro.TipoMaterial,
                    request.Registro.Direccion,
                    request.Registro.Telefono,
                    request.Registro.CorreoElectronico
                    );

                return new ProveedorReply { Resultado = true };

            }
            catch(Exception ex)
            {
                return new ProveedorReply { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<ProveedorReply> updateProveedor(ProveedorRequest request, ServerCallContext context)
        {
            try
            {
                await _dbcontext.Database.ExecuteSqlRawAsync(
                    "EXEC sp_UpdateProveedor {0}, {1}, {2}, {3}, {4}, {5}",
                    request.ProveedorId,
                    request.Registro.Nombre,
                    request.Registro.TipoMaterial,
                    request.Registro.Direccion,
                    request.Registro.Telefono,
                    request.Registro.CorreoElectronico
                    );

                return new ProveedorReply { Resultado = true };

            }
            catch (Exception ex)
            {
                return new ProveedorReply { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<ProveedorReply> deleteProveedor(ProveedorRequest request, ServerCallContext context)
        {
            try
            {
                await _dbcontext.Database.ExecuteSqlRawAsync(
                    "EXEC sp_DeleteProveedor {0}", request.ProveedorId);

                return new ProveedorReply { Resultado = true };

            }
            catch (Exception ex)
            {
                return new ProveedorReply { Resultado = false, Message = ex.Message };
            }
        }
    }
}
