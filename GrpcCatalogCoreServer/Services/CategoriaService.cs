using Grpc.Core;
using GrpcCatalogCoreServer.Models;
using GrpcCatalogCoreServer.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcCatalogCoreServer.Services
{
    public class CategoriaService : CategoriaServ.CategoriaServBase
    {
        private readonly CatalogCoreDbContext _dbcontext;
        private readonly ILogger<ProveedorService> _logger;

        public CategoriaService(CatalogCoreDbContext dbcontext, ILogger<ProveedorService> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public override async Task<CategoriasReply> getCategorias(EmptyCategoriaRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Set<Categorias>().FromSqlRaw("EXEC sp_GetCategorias").ToListAsync();

                CategoriasReply reply = new CategoriasReply();

                reply.Resultado = true;
                reply.Lista.AddRange(r);

                return reply;
            }
            catch (Exception ex)
            {
                return new CategoriasReply { Resultado = false, Message = ex.Message };
            }
        }
    }
}
