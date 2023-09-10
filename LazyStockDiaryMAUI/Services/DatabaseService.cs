using System;
using LazyStockDiaryMAUI.Models;
using SQLite;

namespace LazyStockDiaryMAUI.Services
{
	public class DatabaseService
	{
        SQLiteAsyncConnection Database;

        public DatabaseService()
		{
            init();
            createTables();
        }

        private async Task init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        private async Task createTables()
        {
            await Database.CreateTableAsync<Symbol>();
            await Database.CreateTableAsync<Operation>(); 
        }

        public async Task<List<Operation>> GetSymbolOperations(Symbol symbol)
        {
            return await Database.Table<Operation>().Where(o => o.SymbolId == symbol.Id).ToListAsync();
        }

        public async Task<bool> SymbolExists(string code, string exchange)
        {
            var res = await Database.Table<Symbol>().Where(s => s.Code == code && s.Exchange == exchange).CountAsync();
            return res > 0;
        }

        public async void PutOperation(Operation operation)
        {
            await Database.InsertAsync(operation);
        }

        public async Task<int> UpdateSymbolData(Symbol data, int? Id = null)
        {
            if(Id != null)
            {
                data.Id = Id;
            }

            return await Database.UpdateAsync(data);
        }

        public async Task<int> RegisterSymbol(Symbol symbol)
        {
            var cnt = await Database.Table<Symbol>().Where(s => s.Code == symbol.Code && s.Exchange == symbol.Exchange).CountAsync();
            if(cnt == 0)
            {
                await Database.InsertAsync(symbol);
                var inserted = await Database.Table<Symbol>().Where(s => s.Code == symbol.Code
                                                                        && s.Exchange == symbol.Exchange)
                                                             .FirstAsync();
                return inserted.Id.Value;
            }
            return -1;
        }

        public async Task<List<Symbol>> GetSymbols()
        {
            return await Database.Table<Symbol>().ToListAsync();
        }

        public async Task<Symbol> GetSymbol(string code, string exchange)
        {
            return await Database.Table<Symbol>().Where(s => s.Code == code && s.Exchange == exchange).FirstAsync();
        }
    }
}

