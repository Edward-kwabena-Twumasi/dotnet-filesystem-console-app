
// using System.IO;
// using System.Collections.Generic;

using Newtonsoft.Json;

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // READ FILES LOOP
    foreach (var file in salesFiles)
    {      
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesFiles = FindFiles(storesDirectory);
var salesTotal = CalculateSalesTotal(salesFiles);

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

record SalesData (double Total);

// Writing to directories

// Directory.CreateDirectory(salesTotalDir);   // Add this line of code


// File.WriteAllText(Path.Combine(salesTotalDir, "totals.txt"), String.Empty);

// //Reading from directories and parsing data
// var salesJson = File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");
// var salesData = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

// Console.WriteLine(salesData.Total);

// //write json to file
// var data = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

// File.WriteAllText($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", data.Total.ToString());


// //append
// var apnddata = JsonConvert.DeserializeObject<SalesTotal>(salesJson);
// File.AppendAllText($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", $"{Environment.NewLine}{apnddata.Total}");

// class SalesTotal
// {
//   public double Total { get; set; }
// }


