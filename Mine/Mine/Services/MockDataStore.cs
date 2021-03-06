using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine.Models;

namespace Mine.Services
{
    public class MockDataStore : IDataStore<ItemModel>
    {
        readonly List<ItemModel> items;

        public MockDataStore()
        {
            items = new List<ItemModel>()
            {
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Wooden Sword", Description="A sword made from wood", Value = 1 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Bronze Sword", Description="A sword smelted from copper and tin", Value = 2 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Iron Sword", Description="A sword smelted from iron", Value=4 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Steel Sword", Description="An improved version of the Iron Sword", Value=6 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Damascus Steel Sword", Description="A special version of the Steel Sword", Value=9 },
            };
        }

        public async Task<bool> CreateAsync(ItemModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(ItemModel item)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ItemModel> ReadAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}