using Grpc.Core;
using GrpcCatalogCoreServer.Models;
using GrpcCatalogCoreServer.Protos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;

namespace GrpcCatalogCoreServer.Services
{
    public class UsuarioService : UsuariosServ.UsuariosServBase
    {
        private readonly CatalogCoreDbContext _dbcontext;
        private readonly ILogger<ProveedorService> _logger;

        public UsuarioService(CatalogCoreDbContext dbcontext, ILogger<ProveedorService> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public override async Task<UsuariosReply> getUsuarios(EmptyUsuarioRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Set<Usuarios>().FromSqlRaw("EXEC sp_GetUsuarios").ToListAsync();

                UsuariosReply reply = new UsuariosReply();

                reply.Resultado = true;
                reply.Lista.AddRange(r);

                return reply;

            }
            catch(Exception ex)
            {
                return new UsuariosReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<UsuarioReply> getUsuario(UsuarioRequest request, ServerCallContext context)
        {
            try
            {
                var dbRes = await _dbcontext.Set<Usuarios>().
                    FromSqlRaw("EXEC sp_GetUsuario {0}", request.UsuarioId).ToListAsync();

                var r = dbRes.Find(u => u.UsuarioId == request.UsuarioId);

                if (r != null)
                {
                    return new UsuarioReply() { Resultado = true, Registro = r };
                }

                return new UsuarioReply() { Resultado = false };
            }
            catch(Exception ex)
            {
                return new UsuarioReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<UsuarioReply> insertUsuario(UsuarioRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Database.ExecuteSqlRawAsync("EXEC sp_InsertUsuario {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                        request.Registro.NombreCompleto,
                        request.Registro.TipoUsuarioId,
                        request.Registro.Direccion,
                        request.Registro.Telefono,
                        request.Registro.CorreoElectronico,
                        request.Registro.FechaInscripcion.ToDateTime(),
                        request.Registro.AccesibilidadNecesaria
                    );

                if(r > 0)
                {
                    return new UsuarioReply() { Resultado = true };
                }

                return new UsuarioReply() { Resultado = false };
            }
            catch (Exception ex)
            {
                return new UsuarioReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<UsuarioReply> updateUsuario(UsuarioRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Database.ExecuteSqlRawAsync("EXEC sp_UpdateUsuario {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                        request.UsuarioId,
                        request.Registro.NombreCompleto,
                        request.Registro.TipoUsuarioId,
                        request.Registro.Direccion,
                        request.Registro.Telefono,
                        request.Registro.CorreoElectronico,
                        request.Registro.FechaInscripcion.ToDateTime(),
                        request.Registro.AccesibilidadNecesaria
                    );

                if (r > 0)
                {
                    return new UsuarioReply() { Resultado = true };
                }

                return new UsuarioReply() { Resultado = false };
            }
            catch (Exception ex)
            {
                return new UsuarioReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<UsuarioReply> deleteUsuario(UsuarioRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Database.
                    ExecuteSqlRawAsync("EXEC sp_DeleteUsuario {0}", request.UsuarioId);

                if (r > 0)
                {
                    return new UsuarioReply() { Resultado = true };
                }

                return new UsuarioReply() { Resultado = false };
            }
            catch (Exception ex)
            {
                return new UsuarioReply() { Resultado = false, Message = ex.Message };
            }
        }

    }
}
