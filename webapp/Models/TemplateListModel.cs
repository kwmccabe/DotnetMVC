using System.Collections.Generic;

namespace webapp.Models
{

    public class TemplateListModel
    {
        public List<Template> Items { get; set; }

        public SearchSession Options { get; set; }

        public List<KeyValuePair<string, string>> Columns { get; set; }

        public TemplateListModel()
        {
            Columns = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("Keyname", "Keyname"),
                new KeyValuePair<string, string>("Title", "Template Title"),
                new KeyValuePair<string, string>("TemplateStatus", "Template Status"),
                new KeyValuePair<string, string>("Item.Owner.UserName", "Owner"),
                new KeyValuePair<string, string>("Item.ModificationDate", "Modification Date"),

            };

        }
    }
}
