using Microsoft.EntityFrameworkCore;
using Susep.SISRH.Domain.AggregatesModel.TipoVinculoAggregate;
using Susep.SISRH.Infrastructure.Contexts;
using SUSEP.Framework.Data.Concrete.Repositories;
using System;
using System.Threading.Tasks;

namespace Susep.SISRH.Infrastructure.Repositories
{
    public class TipoVinculoRepository : SqlServerRepository<TipoVinculo>, ITipoVinculoRepository
    {
        private readonly SISRHDbContext _context;

        public TipoVinculoRepository(SISRHDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TipoVinculo> ObterAsync(Int64 tipoVinculoId)
        {
            var item = await Entity.FindAsync(tipoVinculoId);
            return item;
        }

        public async Task<TipoVinculo> AdicionarAsync(TipoVinculo item)
        {
            var result = await Entity.AddAsync(item);
            return result.Entity;
        }

        public void Atualizar(TipoVinculo item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Excluir(TipoVinculo item)
        {
            _context.Entry(item).State = EntityState.Deleted;
        }

        public async Task ExcluirAsync(Int64 tipoVinculoId)
        {
            var item = await Entity.FindAsync(tipoVinculoId);
            _context.Entry(item).State = EntityState.Deleted;
        }
    }
}