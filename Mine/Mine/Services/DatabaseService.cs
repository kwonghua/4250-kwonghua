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

        /// <summary>
        /// InsertAsync will write to the table, it returns the ID of what was written, 
        /// for our usage item already holds the ID, so as long as it is not 0, it worked
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(ItemModel item)
        {
            if (item == null)
            {
                return false;
            }

            var result = await Database.InsertAsync(item);

            if (result == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// updates item in database, true if success
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ItemModel item)
        {
            if(item == null)
            {
                return false;
            }

            var result = await Database.UpdateAsync(item);
            if(result == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Used to delete item from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var data = await ReadAsync(id);
            if(data == null)
            {
                return false;
            }

            var result = await Database.DeleteAsync(data);
            if(result == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ReadAsync will take an ID and return the ItemModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ItemModel> ReadAsync(string id)
        {
            if(id == null)
            {
                return null;
            }

            // Call the Database to read the ID
            // Using Linq syntax. Find the first record that the ID that matches
            var result = Database.Table<ItemModel>().FirstOrDefaultAsync(m => m.Id.Equals(id));

            return result;
        }

        /// <summary>
        /// index async for database
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            var result = await Database.Table<ItemModel>().ToListAsync();
            return result;
        }
    }
}