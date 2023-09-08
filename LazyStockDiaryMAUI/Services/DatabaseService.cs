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
            var result = await Database.CreateTableAsync<Symbol>();
        }

        public async Task<int> UpdateSymbolData(Symbol data, int? Id = null)
        {
            data.Id = Id;
            return await Database.UpdateAsync(data);
        }

        public async Task<bool> RegisterSymbol(Symbol symbol)
        {
            var cnt = await Database.Table<Symbol>().Where(s => s.Code == symbol.Code && s.Exchange == symbol.Exchange).CountAsync();
            if(cnt == 0)
            {
                await Database.InsertAsync(symbol);
            }
            return true;
        }

        public async Task<List<Symbol>> GetSymbols()
        {
            return await Database.Table<Symbol>().ToListAsync();
        }
    }
}

