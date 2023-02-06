using System.Collections.Generic;

namespace ConsoleApp
{
    public interface IImportedObject
    {
        string Name { get; set; }
        List<IImportedObject> Children { get;  }
        int NumberOfChildren { get; }
        string ParentName { get; set; }
        void Add(IImportedObject child);
        List<IImportedObject> GetImportedObjects();
        void Print(int depth = 0);
    }
}