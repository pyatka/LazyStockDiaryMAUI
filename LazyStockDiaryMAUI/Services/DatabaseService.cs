﻿using System;
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
            await Database.CreateTableAsync<Dividend>();
        }

        public async Task<DateTime?> GetSymbolFirstBuyDate(Symbol symbol)
        {
            if (symbol.Id.HasValue)
            {
                var operations = await GetSymbolOperations(symbol.Id.Value);
                if (operations.Count > 0)
                {
                    return operations.Last().Date;
                }
            }

            return null;
        }

        public async Task<List<Operation>> GetSymbolOperations(int symbolId)
        {
            return await Database.Table<Operation>()
                                 .Where(o => o.SymbolId == symbolId)
                                 .OrderByDescending(o => o.Date)
                                 .ToListAsync();
        }

        public async Task<int> UpdateSymbol(Symbol symbol)
        {
            return await Database.UpdateAsync(symbol);
        }

        public async Task UpdateSymbolDividend(Symbol symbol, List<Dividend> dividends, bool clear = true)
        {
            if (clear)
            {
                var symbolDividendQuery = Database.Table<Dividend>()
                                             .Where(d => d.Code == symbol.Code
                                                      && d.Exchange == symbol.Exchange);
                await symbolDividendQuery.DeleteAsync();
                await Database.InsertAllAsync(dividends);
            } else
            {
                await Database.UpdateAllAsync(dividends);
            }

            var dbSymbol = await GetSymbol(symbol.Code, symbol.Exchange);
            dbSymbol.DividendLastUpdate = DateTime.Now;
            await Database.UpdateAsync(dbSymbol);
        }

        public async Task<List<Operation>> GetSymbolOperations(Symbol symbol)
        {
            return await Database.Table<Operation>().Where(o => o.SymbolId == symbol.Id).ToListAsync();
        }

        public async Task<List<Dividend>> GetSymbolDividends(string code,
                                                             string exchange,
                                                             DateTime startDate,
                                                             DateTime endDate)
        {
            return await Database.Table<Dividend>().Where(d => d.Code == code
                                                            && d.Exchange == exchange
                                                            && d.RecordDate >= startDate
                                                            && d.RecordDate < endDate)
                                                   .ToListAsync();
        }

        public async Task<bool> SymbolExists(string code, string exchange)
        {
            var res = await Database.Table<Symbol>().Where(s => s.Code == code && s.Exchange == exchange).CountAsync();
            return res > 0;
        }

        public async void PutOperation(Operation operation)
        {
            var a = await Database.InsertAsync(operation);
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

