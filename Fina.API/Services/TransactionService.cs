using Fina.API.Data;
using Fina.Core.Ferramentas;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Fina.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Fina.API.Services
{
    public class TransactionService(AppDbContext context) : ITransactionService
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                //Se o valor recebido na requisição for positivo e o tipo da transacao for negativo, é preciso converter
                //type = saida
                //300 -> -300

                if (request is { Type : Core.Enums.ETransactionType.Withdraw, Amount: >= 0})
                {
                    request.Amount *= -1;
                }

                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreateAt = DateTime.UtcNow,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceveidAt,
                    Title = request.Title,
                    Type = request.Type
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso");
            }
            catch
            {
                return new Response<Transaction?>(null, 501, "Não foi possível criar a transação");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(c => c.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, message: "Categoria removida com sucesso");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível excluir a categoria");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == request.UserId && c.Id == request.Id);
                if (transaction == null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                return new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível encontrar a transação");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetPrimeiroDia();
                request.EndDate ??= DateTime.Now.GetUltimoDia();


                var query = context.Transactions
                    .AsNoTracking()
                    .Where(c => c.UserId == request.UserId && c.PaidOrReceivedAt >= request.StartDate && c.PaidOrReceivedAt <= request.EndDate)
                    .OrderBy(c => c.PaidOrReceivedAt);

                var transactions = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 404, "Não foi possível recurar a lista de transações");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                //Se o valor recebido na requisição for positivo e o tipo da transacao for negativo, é preciso converter
                //type = saida
                //300 -> -300

                if (request is { Type: Core.Enums.ETransactionType.Withdraw, Amount: >= 0 })
                {
                    request.Amount *= -1;
                }

                var transaction = await context.Transactions.FirstOrDefaultAsync(c => c.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrReceveidAt;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();


                return new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível atualizar a transação");
            }
        }

    }
}
