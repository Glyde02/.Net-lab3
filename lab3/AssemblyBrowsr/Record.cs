using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    public class Record
    {
        public string text { get; }
        public List<Record> records { get; set; }

        public Record(string text)
        {
            records = new List<Record>();
            this.text = text;
        }

        public void AddRecord(Record record)
        {
            records.Add(record);
        }
    }
}