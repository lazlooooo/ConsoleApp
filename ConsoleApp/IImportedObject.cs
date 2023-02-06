using System.Collections.Generic;

namespace ConsoleApp
{
    public interface IImportedObject
    {
        string DataType { get; set; }
        string IsNullable { get; set; }
        string Name { get; set; }
        List<IImportedObject> Children { get;  }
        int NumberOfChildren { get; }
        string ParentName { get; set; }
        string ParentType { get; set; }
        string Schema { get; set; }
        string Type { get; set; }

        void Add(IImportedObject child);
        List<IImportedObject> GetImportedObjects();
        void Print(int depth = 0);
    }
}