using Grpc.Core;
using GrpcCatalogCoreServer.Models;
using GrpcCatalogCoreServer.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcCatalogCoreServer.Services
{
    public class PersonalService : PersonalServ.PersonalServBase
    {
        private readonly CatalogCoreDbContext _dbcontext;
        private readonly ILogger<ProveedorService> _logger;

        public PersonalService(CatalogCoreDbContext dbcontext, ILogger<ProveedorService> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public override async Task<EmpleadosReply> getEmpleados(EmptyEmpleadosRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Set<Empleados>().FromSqlRaw("EXEC sp_GetPersonal").ToListAsync();

                EmpleadosReply reply = new EmpleadosReply();

                reply.Resultado = true;
                reply.Lista.AddRange(r);

                return reply;

            }
            catch (Exception ex)
            {
                return new EmpleadosReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<EmpleadoReply> getEmpleado(EmpleadoRequest request, ServerCallContext context)
        {
            try
            {
                var dbRes = await _dbcontext.Set<Empleados>()
                    .FromSqlRaw("EXEC sp_GetEmpleado {0}", request.EmpleadoId).ToListAsync();

                var r = dbRes.Find(e => e.EmpleadoId == request.EmpleadoId);

                if(r != null)
                {
                    return new EmpleadoReply() { Resultado = true, Registro = r };
                }

                return new EmpleadoReply() { Resultado = false };

            }
            catch (Exception ex)
            {
                return new EmpleadoReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<EmpleadoReply> insertEmpleado(EmpleadoRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Database.ExecuteSqlRawAsync(
                    "EXEC sp_InsertPersonal {0}, {1}, {2}, {3}, {4}, {5}",
                        request.Registro.NombreCompleto,
                        request.Registro.RolId,
                        request.Registro.FechaContratacion.ToDateTime(),
                        request.Registro.Salario,
                        request.Registro.HorarioTrabajo,
                        request.Registro.ContactoEmergencia
                    );

                if(r > 0)
                {
                    return new EmpleadoReply() { Resultado = true };
                }

                return new EmpleadoReply() { Resultado = false };


            }
            catch (Exception ex)
            {
                return new EmpleadoReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<EmpleadoReply> updateEmpleado(EmpleadoRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Database.ExecuteSqlRawAsync(
                    "EXEC sp_UpdatePersonal {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                        request.EmpleadoId,
                        request.Registro.NombreCompleto,
                        request.Registro.RolId,
                        request.Registro.FechaContratacion.ToDateTime(),
                        request.Registro.Salario,
                        request.Registro.HorarioTrabajo,
                        request.Registro.ContactoEmergencia
                    );

                if (r > 0)
                {
                    return new EmpleadoReply() { Resultado = true };
                }

                return new EmpleadoReply() { Resultado = false };


            }
            catch (Exception ex)
            {
                return new EmpleadoReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<EmpleadoReply> deleteEmpleado(EmpleadoRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Database.ExecuteSqlRawAsync(
                    "EXEC sp_DeletePersonal {0}",
                        request.EmpleadoId
                    );

                if (r > 0)
                {
                    return new EmpleadoReply() { Resultado = true };
                }

                return new EmpleadoReply() { Resultado = false };
            }
            catch(Exception ex)
            {
                return new EmpleadoReply() { Resultado = false, Message = ex.Message };
            }
        }

    }
}
