using System;
using System.Collections.Generic;
using System.Linq;
namespace ConsoleApp
{
    public class ImportedObject : IImportedObject
    {
        public string Name { get; set; }
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }
        public string Type { get; set; }
        public string IsNullable { get; set; }
        public string DataType { get; set; }
        public int NumberOfChildren
        {
            get { return this.Children.Count; }
        }
        public List<IImportedObject> Children { get; private set; } = new List<IImportedObject>();

        public void Add(IImportedObject child)
        {
            Children.Add(child);
        }
        public static ImportedObject CreateTree(IEnumerable<IImportedObject> importedObjects)
        {
            var root = new ImportedObject();
            foreach (var importedObject in importedObjects)
            {
                var parent = importedObjects.FirstOrDefault(x => x.Name == importedObject.ParentName);
                if (parent != null)
                {
                    parent.Add(importedObject);
                }
                else
                {
                    root.Add(importedObject);
                }
            }
            return root;
        }
        public List<IImportedObject> GetImportedObjects()
        {
            var result = new List<IImportedObject>();
            result.Add(new ImportedObject
            {
                Type = Type,
                Name = Name,
                Schema = Schema,
                ParentName = ParentName,
                ParentType = ParentType,
            });
            foreach (var child in Children)
            {
                result.AddRange(child.GetImportedObjects());
            }
            return result;
        }

        public void Print(int depth = 0)
        {
            var text = GetText(depth);

            if (!string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine(text);
            }
            if (Children.Count > 0)
            {
                foreach (var child in Children)
                {
                    child.Print(depth + 1);
                }
            }
        }

        private string GetText(int depth)
        {
            switch (depth)
            {
                case 1: return Type == "DATABASE" ? $"Database '{Name}' ({NumberOfChildren} tables)" : "";
                case 2: return $"\tTable '{Schema}.{Name}' ({NumberOfChildren} columns)";
                case 3: return $"\t\tColumn '{Name}' with {Type} data type {(IsNullable == "1" ? "accepts nulls" : "with no nulls")}";
                default:
                    return "";
            }
        }
    }




}


