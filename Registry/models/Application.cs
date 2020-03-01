using System.Data;
using System.Collections.Generic;

namespace Registry
{
    public class Application
    {
        public Application(DataRowCollection rows)
        {
            this.Name = rows[0]["ApplicationName"].ToString();
            this.ClientGUID = rows[0]["ClientGUID"].ToString();
            this.Queue = rows[0]["RMQueue"].ToString();
            this.Bindings = new Dictionary<string, List<string>>();

            foreach (DataRow row in rows)
            {
                if (!this.Bindings.ContainsKey(row["ExchangeName"].ToString()))
                {
                    this.Bindings.Add(row["ExchangeName"].ToString(), new List<string>());
                }

                this.Bindings[row["ExchangeName"].ToString()].Add(row["FieldName"].ToString());
            }

        }
        public string Name { get; }
        public string ClientGUID { get; }
        public string Queue { get; }
        public Dictionary<string, List<string>> Bindings { get; }
    }
}
