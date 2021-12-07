using AssemblyBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly
{
    public enum ItemType
    {
        Field, Property, Method, Ctor
    }

    class Info
    {
        private string assemblyName;
        public readonly Dictionary<string, Record> namespaces = new();

        public Info(string name)
        {
            assemblyName = name;
        }

        public void AddType(string namespaceName, string typeName)
        {
            if (!namespaces.ContainsKey(namespaceName))
            {
                namespaces.Add(namespaceName, new Record(namespaceName));
            }

            var type = new Record(typeName);
            type.AddRecord(new Record("Fields"));
            type.AddRecord(new Record("Properties"));
            type.AddRecord(new Record("Methods"));
            type.AddRecord(new Record("Ctors"));
            namespaces[namespaceName].AddRecord(type);
        }

        public void AddItemToLastAddedType(string namespaceName, Record itemToAdd, ItemType recordType)
        {
            namespaces[namespaceName].records.Last().records[(int)recordType].AddRecord(itemToAdd);
        }

        public Record ToNodes()
        {
            var result = new Record(assemblyName) { records = namespaces.Values.ToList() };
            return result;
        }
    }
}
