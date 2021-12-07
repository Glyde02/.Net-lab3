using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowser
{
    public class AssemblyBrowser
    {

        public static Record BrowseFile(string path)
        {
            
            var assembly = Assembly.LoadFrom(path);
            
            foreach(var type in assembly.GetTypes())
            {

                var namespaceName = type.Namespace;
                if (namespaceName == null)
                    namespaceName = "No namespace";

                foreach (var f in
                              type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                {
                    assemblyInfo.AddItemToLastAddedType(
                          namespaceName,
                          new Node(f.Name + " : " + f.FieldType),
                          ItemType.Field);
                }

            }

        }


    }

    public class Record
    {
        public string text { get;  }
        public List<Record> records { get; }

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
