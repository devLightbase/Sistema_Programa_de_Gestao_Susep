using Microsoft.EntityFrameworkCore;
using Susep.SISRH.Domain.AggregatesModel.SituacaoAggregate;
using Susep.SISRH.Infrastructure.Contexts;
using SUSEP.Framework.Data.Concrete.Repositories;
using System;
using System.Threading.Tasks;

namespace Susep.SISRH.Infrastructure.Repositories
{
    public class SituacaoRepository : SqlServerRepository<Situacao>, ISituacaoRepository
    {
        private readonly SISRHDbContext _context;

        public SituacaoRepository(SISRHDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Situacao> ObterAsync(Int64 situacaoPessoaId)
        {
            var item = await Entity.FindAsync(situacaoPessoaId);
            return item;
        }

        public async Task<Situacao> AdicionarAsync(Situacao item)
        {
            var result = await Entity.AddAsync(item);
            return result.Entity;
        }

        public void Atualizar(Situacao item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Excluir(Situacao item)
        {
            _context.Entry(item).State = EntityState.Deleted;
        }

        public async Task ExcluirAsync(Int64 situacaoPessoaId)
        {
            var item = await Entity.FindAsync(situacaoPessoaId);
            _context.Entry(item).State = EntityState.Deleted;
        }
    }
}