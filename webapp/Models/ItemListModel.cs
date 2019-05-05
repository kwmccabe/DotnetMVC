using System.Collections.Generic;

namespace webapp.Models
{

    public class ItemListModel
    {
        public List<Item> Items { get; set; }

        public SearchSession Options { get; set; }

        public List<KeyValuePair<string, string>> Columns { get; set; }

        public ItemListModel()
        {
            Columns = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("Keyname", "Keyname"),
                new KeyValuePair<string, string>("ItemType", "Item Type"),
                new KeyValuePair<string, string>("Owner.UserName", "Owner"),
                new KeyValuePair<string, string>("CreateDate", "Create Date"),
                new KeyValuePair<string, string>("ModificationDate", "Modification Date")
            };

        }
    }
}
