using System;
using System.Linq;
using System.Threading.Tasks;

using SQLite;

using Mine.Models;
using System.Collections.Generic;

namespace Mine.Services
{
    public class DatabaseService : IDataStore<ItemModel>
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false);
                }

                initialized = true;
            }
        }

        Task<bool> IDataStore<ItemModel>.CreateAsync(ItemModel item)
        {
            throw new NotImplementedException();
        }

        Task<bool> IDataStore<ItemModel>.UpdateAsync(ItemModel item)
        {
            throw new NotImplementedException();
        }

        Task<bool> IDataStore<ItemModel>.DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<ItemModel> IDataStore<ItemModel>.ReadAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            var result = await Database.Table<ItemModel>().ToListAsync();
            return result;
        }
    }
}