using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using ConsoleApp.Extensions;
namespace ConsoleApp
{


    public class DataReader
    {
        public string FilePath { get; private set; }
        private ImportedObject _rootObject;

        public void ImportData(string fileToImport) 
        {
            FilePath = fileToImport;
            var importedLines = new List<string>();
            using (var streamReader = new StreamReader(fileToImport))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    importedLines.Add(line);
                }
            }
            IEnumerable<IImportedObject> _importedObjects  = 
                importedLines
                   .Select(line => line.Split(';'))
                   .Where(line=>line.Length >=7)
                   .Select(value => new ImportedObject
                   {
                       Type = value[0].TrimAndReplace().ToUpper(),
                       Name = value[1].TrimAndReplace(),
                       Schema = value[2].TrimAndReplace(),
                       ParentName = value[3].TrimAndReplace(),
                       ParentType = value[4].TrimAndReplace(),
                       DataType = value[5],
                       IsNullable = value[6]
                   }).ToList();
            _rootObject = ImportedObject.CreateTree(_importedObjects);

        }

        public void PrintData()
        {
            if (_rootObject == null) throw new ArgumentNullException();
            _rootObject.Print();
            Console.ReadLine();
        }
        public void ImportAndPrint(string fileToImport)
        {
            ImportData(fileToImport);
            PrintData();
        }
    }

}
