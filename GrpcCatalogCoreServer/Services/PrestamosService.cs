using Azure.Core;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcCatalogCoreServer.Models;
using GrpcCatalogCoreServer.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcCatalogCoreServer.Services
{
    public class PrestamosService : PrestamosServ.PrestamosServBase
    {
        private readonly CatalogCoreDbContext _dbcontext;
        private readonly ILogger<ProveedorService> _logger;

        public PrestamosService(CatalogCoreDbContext dbcontext, ILogger<ProveedorService> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public override async Task<PrestamosReply> getPrestamos(EmptyPrestamosRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Set<Prestamos>().FromSqlRaw("EXEC sp_GetPrestamos").ToListAsync();

                PrestamosReply reply = new PrestamosReply();

                reply.Resultado = true;
                reply.Lista.AddRange(r);

                return reply;

            }
            catch(Exception ex)
            {
                return new PrestamosReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<PrestamoReply> getPrestamo(PrestamoRequest request, ServerCallContext context)
        {
            try
            {
                var dbres = await _dbcontext.Set<Prestamos>().FromSqlRaw(
                    "EXEC sp_GetPrestamo {0}", request.PrestamoId).ToListAsync();

                var r = dbres.Find(p => p.PrestamoId == request.PrestamoId);

                if(r != null)
                {
                    return new PrestamoReply() { Resultado = true, Registro = r };
                }

                return new PrestamoReply() { Resultado = false };


            }
            catch(Exception ex)
            {
                return new PrestamoReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<PrestamoReply> insertPrestamo(PrestamoRequest request, ServerCallContext context)
        {
            try
            {
                if (request.Registro.FechaDevolucionReal != null)
                {
                    var r = await _dbcontext.Database.ExecuteSqlRawAsync(
                            "EXEC sp_InsertPrestamo {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                            request.Registro.UsuarioId,
                            request.Registro.MaterialId,
                            request.Registro.EmpleadoId,
                            request.Registro.FechaPrestamo.ToDateTime(),
                            request.Registro.FechaDevolucionEsperada.ToDateTime(),
                            request.Registro.FechaDevolucionReal.ToDateTime(),
                            request.Registro.Penalizaciones
                            );

                    if (r > 0)
                    {
                        return new PrestamoReply() { Resultado = true };
                    }
                }
                else
                {
                    var r = await _dbcontext.Database.ExecuteSqlRawAsync(
                            "EXEC sp_InsertPrestamo {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                            request.Registro.UsuarioId,
                            request.Registro.MaterialId,
                            request.Registro.EmpleadoId,
                            request.Registro.FechaPrestamo.ToDateTime(),
                            request.Registro.FechaDevolucionEsperada.ToDateTime(),
                            null,
                            request.Registro.Penalizaciones
                            );

                    if (r > 0)
                    {
                        return new PrestamoReply() { Resultado = true };
                    }
                }

                return new PrestamoReply() { Resultado = false };
            }
            catch(Exception ex)
            {
                return new PrestamoReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<PrestamoReply> updatePrestamo(PrestamoRequest request, ServerCallContext context)
        {
            try
            {
                if (request.Registro.FechaDevolucionReal != null)
                {
                    Console.WriteLine("\tEL VALOR ES ------------------------------------" + request.Registro.FechaDevolucionReal.ToDateTime());
                    var r = await _dbcontext.Database.ExecuteSqlRawAsync(
                            "EXEC sp_UpdatePrestamo {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                            request.PrestamoId,
                            request.Registro.UsuarioId,
                            request.Registro.MaterialId,
                            request.Registro.EmpleadoId,
                            request.Registro.FechaPrestamo.ToDateTime(),
                            request.Registro.FechaDevolucionEsperada.ToDateTime(),
                            request.Registro.FechaDevolucionReal.ToDateTime(),
                            request.Registro.Penalizaciones
                        );

                    if (r > 0)
                    {
                        return new PrestamoReply() { Resultado = true };
                    }
                }
                else
                {
                    Console.WriteLine("\tEL VALOR ES NULL ------------------------------------");
                    var r = await _dbcontext.Database.ExecuteSqlRawAsync(
                            "EXEC sp_UpdatePrestamo {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                            request.PrestamoId,
                            request.Registro.UsuarioId,
                            request.Registro.MaterialId,
                            request.Registro.EmpleadoId,
                            request.Registro.FechaPrestamo.ToDateTime(),
                            request.Registro.FechaDevolucionEsperada.ToDateTime(),
                            null,
                            request.Registro.Penalizaciones
                        );

                    if (r > 0)
                    {
                        return new PrestamoReply() { Resultado = true };
                    }
                }

                return new PrestamoReply() { Resultado = false };
            }
            catch (Exception ex)
            {
                return new PrestamoReply() { Resultado = false, Message = ex.Message };
            }
        }





        public override async Task<PrestamoReply> deletePrestamo(PrestamoRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Database.ExecuteSqlRawAsync(
                    "EXEC sp_DeletePrestamo {0}", request.PrestamoId
                    );

                if (r > 0)
                {
                    return new PrestamoReply() { Resultado = true };
                }

                return new PrestamoReply() { Resultado = false };
            }
            catch(Exception ex)
            {
                return new PrestamoReply() { Resultado = false, Message = ex.Message };
            }
        }

    }
}
