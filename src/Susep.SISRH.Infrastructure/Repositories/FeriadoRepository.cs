using Microsoft.EntityFrameworkCore;
using Susep.SISRH.Domain.AggregatesModel.FeriadoAggregate;
using Susep.SISRH.Infrastructure.Contexts;
using SUSEP.Framework.Data.Concrete.Repositories;
using System;
using System.Threading.Tasks;

namespace Susep.SISRH.Infrastructure.Repositories
{
    public class FeriadoRepository : SqlServerRepository<Feriado>, IFeriadoRepository
    {
        private readonly SISRHDbContext _context;

        public FeriadoRepository(SISRHDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Feriado> ObterAsync(Int64 feriadoId)
        {
            var item = await Entity.FindAsync(feriadoId);
            return item;
        }

        public async Task<Feriado> AdicionarAsync(Feriado item)
        {
            var result = await Entity.AddAsync(item);
            return result.Entity;
        }

        public void Atualizar(Feriado item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Excluir(Feriado item)
        {
            _context.Entry(item).State = EntityState.Deleted;
        }

        public async Task ExcluirAsync(Int64 feriadoId)
        {
            var item = await Entity.FindAsync(feriadoId);
            _context.Entry(item).State = EntityState.Deleted;
        }
    }
}