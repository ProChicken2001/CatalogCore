using Grpc.Core;
using GrpcCatalogCoreServer.Models;
using GrpcCatalogCoreServer.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcCatalogCoreServer.Services
{
    public class TipoUsuarioService : TipoUsuarioServ.TipoUsuarioServBase
    {
        private readonly CatalogCoreDbContext _dbcontext;
        private readonly ILogger<ProveedorService> _logger;

        public TipoUsuarioService(CatalogCoreDbContext dbcontext, ILogger<ProveedorService> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public override async Task<TipoUsuariosReply> getTipoUsuarios(EmptyTipoUsuarioRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Set<TipoUsuarios>().FromSqlRaw("EXEC sp_GetTipoUsuarios").ToListAsync();

                TipoUsuariosReply reply = new TipoUsuariosReply();

                reply.Resultado = true;
                reply.Lista.AddRange(r);

                return reply;

            }
            catch (Exception ex)
            {
                return new TipoUsuariosReply() { Resultado = false, Message = ex.Message };
            }
        }

    }
}
