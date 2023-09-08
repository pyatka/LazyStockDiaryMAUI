using System;
using SQLite;

namespace LazyStockDiaryMAUI.Services
{
	public class DatabaseService
	{
        SQLiteAsyncConnection Database;

        public DatabaseService()
		{
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }
    }
}

