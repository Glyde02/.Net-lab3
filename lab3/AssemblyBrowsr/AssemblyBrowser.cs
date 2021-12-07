using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowser
{
    public static class AssemblyBrowsr
    {
        public static Record BrowseFile(string path)
        {
            
            var assembly = Assembly.LoadFrom(path);
            var info = new Info(assembly.GetName().Name);

            foreach (var type in assembly.GetTypes())
            {
                var namespaceName = type.Namespace ?? "No namespace";
                info.AddType(namespaceName, type.Name);

                foreach (var f in
                      type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                {
                    info.AddItemToLastAddedType(
                          namespaceName,
                          new Record(f.Name + " : " + f.FieldType),
                          ItemType.Field);
                }

                foreach (var p in
                type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                {
                    info.AddItemToLastAddedType(
                                namespaceName,
                                new Record(p.Name + " : " + p.PropertyType),
                                ItemType.Property);
                }


                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (var method in methods)
                {
                    var methodNode = new Record(method.Name);
                    var parameters = method.GetParameters();
                    foreach (var param in parameters)
                    {
                        methodNode.AddRecord(new Record(param.Name + " : " + param.ParameterType));
                    }
                    info.AddItemToLastAddedType(namespaceName, methodNode, ItemType.Method);
                }

                var ctors = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (var ctor in ctors)
                {
                    var ctorNode = new Record(ctor.Name);
                    var parameters = ctor.GetParameters();
                    foreach (var param in parameters)
                    {
                        ctorNode.AddRecord(new Record(param.Name + " : " + param.ParameterType));
                    }
                    info.AddItemToLastAddedType(namespaceName, ctorNode, ItemType.Ctor);
                }
            }

            return info.ToNodes();

        }


    }

    
}
