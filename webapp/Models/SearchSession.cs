using System;
using System.Reflection;

namespace webapp.Models
{
    public class SearchSession
    {
        public int ItemCount;
        public int PageCount;
        // FIX THIS: add owner-vs-user
        public string Search;
        public string ItemType;
        public string Status;
        public string Sort;
        public string Order;
        public int Offset;
        public int Limit;
        public int PageNum;

        public SearchSession()
        {
            ItemCount = 0;
            PageCount = 0;
            Search = "";
            ItemType = "All";
            Status = "All";
            Sort = "Keyname";
            Order = "ASC";
            Offset = 0;
            Limit = 3;
            PageNum = 1;
        }

        public override string ToString()
        {
            var result = "";
            foreach (FieldInfo field in this.GetType().GetFields())
            {
                result += (string.IsNullOrEmpty(result)) ? "SearchSession[ " : ", ";
                result += String.Format("{0}='{1}'", field.Name, field.GetValue(this));
            }
            result += " ]";
            return result;

        }

    }
}
