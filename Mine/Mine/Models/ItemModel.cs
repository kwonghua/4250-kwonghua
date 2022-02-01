using System;
using SQLite;

namespace Mine.Models
{
    /// <summary>
    /// Items for the Characters and Monsters to use
    /// </summary>
    public class ItemModel
    {
        // the Id for the Item
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        // The display text for the item
        public string Text { get; set; }
        // the description of the item
        public string Description { get; set; }
        // the value of the item +9 damage
        public int Value { get; set; } = 0;
    }
}