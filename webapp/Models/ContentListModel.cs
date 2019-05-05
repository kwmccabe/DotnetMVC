using System.Collections.Generic;

namespace webapp.Models
{

    public class ContentListModel
    {
        public List<Content> Items { get; set; }

        public SearchSession Options { get; set; }

        public List<KeyValuePair<string, string>> Columns { get; set; }

        public ContentListModel()
        {
            Columns = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("Keyname", "Keyname"),
                new KeyValuePair<string, string>("Title", "Content Title"),
                new KeyValuePair<string, string>("Template.Keyname", "Template"),
                new KeyValuePair<string, string>("ContentStatus", "Content Status"),
                new KeyValuePair<string, string>("Item.Owner.UserName", "Owner"),
                new KeyValuePair<string, string>("Item.ModificationDate", "Modification Date")
            };

        }
    }
}
