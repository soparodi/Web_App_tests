// I Metodi di files

//Creare un file
string path = @"test.txt";
File.Create(path).Close();

//creare un file con il timestamp
string path= $"test_{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt";
File.Create(path).Close();

//Scrivere su un file
File.WriteAllText(path, "Hello,World");

//Scrivere una lista di stringhe su un file
string[]lines ={"line1", "line2", "line3"};
File.WriteAllLines(path, lines);

//Aggiungere testo ad un file
File.AppendAllText(path, "Hello, world");

//aggiungere una lista di stringhe ad un file
string[] lines = {"line1", "line2", "line3"};
File.AppendAllLines(path, lines);

//WriteAllText sovrascrive il contenuto del file, AppendAllText aggiunge il testo alla fine del file
//WriteAllLines sovrascrive il contenuto del file, AppendAllLines aggiunge le righe alla fine del file
//WruteAllText e AppendAllText sono equivalenti a scrivere con StreamWriter e usare il metodo Write o WriteLine rispettivamente

//scrittura con StreamWriter
using (StreamWriter sw = new StreamWriter(path))
{
    sw.Write("Hello,World");
}

//Leggere da un file
string content = File.ReadAllText(path);
//stampo il contenuto del file
Console.WriteLine(content);

//Copiare un file
string path2 = @"test2.txt";
File.Copy(path2,path2);

//Rinominare un file
string path3 = @"test3.txt";
File.Move(path2,path3);

//Eliminare un file
File.Delete(path3);

//Creare una directory
string di = @"test";
Directory.CreateDirectory(dir);

//Eliminare una directory
Directory.Delete(dir);

//Creare un file temporaneo
string File= path.GetTempFileName();
Console.WriteLine(File);

//Creare un file temporaneo in una directory specifica
//Path.Combine unisce i path in questo caso aggiunge "temp" alla directory temporanea
string tempDir= path.Combine(path.GetTempPath(), "temp");
Directory.CreateDirectory(tempDir);

//verificare se un file esiste (restituisce un valore booleano)
if (File.Exists(path))
{
    Console.WriteLine("File.exists");
}

//verificare se una directory esiste
if(Directory.Exists(dir))
{
Console.WriteLine("Directory exists");
}

//ottenere informazioni su un file
FileInfo info = new FileInfo(path);
Console.WriteLine

//ottenere informazione su una Directory
DirectoryInfo dirInfo= new DirectoryInfo(dir);
Console.WriteLine(dirInfo.CreationTime);

//ottenere informazioni su tutti i file in una directory
string[] files = DirectoryInfo.GetFiles(dir);
foreach (string file in files)
{
    Console.WriteLine(file);
}

//ottenere informazioni su tutte le directory in una directory
string[] dirs= DirectoryInfo.GetDirectories(dir);
foreach (string d in dirs)
{
    Console.WriteLine(d);
}

//ottenere informazioni su tutti i file e le ditectory in una directory
string[] all = Directory.GetFileSystemEntries(dir);
foreach (string a in all)
{
    Console.WriteLine(a);
}

//ottenere informazioni su tutti i file e le directory in una directory con un filtro
string[] textFiles= DirectoryInfo.GetFiles(dirs, "*.txt");
foreach (string txtFile in textFiles)
{
    Console.WriteLine(txtFile);
}