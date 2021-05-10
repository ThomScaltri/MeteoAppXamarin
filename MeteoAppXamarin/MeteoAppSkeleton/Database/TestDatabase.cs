using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MeteoAppSkeleton.Models;
using SQLite;

namespace MeteoAppSkeleton
{
    public class TestDatabase
    {
        readonly SQLiteAsyncConnection database;

        public TestDatabase()
        {
            // collegamento al database
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MeteoDBX.db3");
            database = new SQLiteAsyncConnection(dbPath);

            // crea la tabella se non esiste
            database.CreateTableAsync<Entry>().Wait();
        }

        /*
         * Ritorna una lista con tutti gli items.
         */
        public Task<List<Entry>> GetItemsAsync()
        {
            return database.Table<Entry>().ToListAsync();
        }

        /*
         * Query con statement SQL.
         */
        public Task<List<Entry>> GetItemsWithWhere(int id)
        {
            return database.QueryAsync<Entry>("SELECT * FROM [Entry] WHERE [ID] =?", id);
        }

        /*
         * Query con LINQ.
         */
        public Task<Entry> GetItemAsync(int id)
        {
            return database.Table<Entry>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        /*
         * Salvataggio o update.
         */
        public Task<int> SaveItemAsync(Entry item)
        {
            if (item.ID == 0) // esempio
                return database.UpdateAsync(item);

            return database.InsertAsync(item);
        }

        public Task<int> InsertItemAsync(Entry item)
        {
            return database.InsertAsync(item);
        }

        /*
         * Cancellazione di un elemento.
         */
        public Task<int> DeleteItemAsync(Entry item)
        {
            return database.DeleteAsync(item);
        }

    }
}
