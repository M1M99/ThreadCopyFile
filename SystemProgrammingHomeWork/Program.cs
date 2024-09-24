//task

//From path (file path)      => "C:\Users\namiqrasullu\Desktop\Untitled.png"
//To Path   (directory path) => "C:\Users\namiqrasullu\Desktop\Books"
//From'da daxil olunan file To path'ne kopyalanmalidir.
//kopyalanmish bytes/Umumi bytes

//30/20042
//bitdikden sonra Kopyalanma tamamlandi deyilir.

//homework

//Desktopdaki butun file ve folderler gorsenir. folder sechilse, folderin icherisindeki folder ve filelar gorsenir. File sechilse, hemin file kopyalanmaq uchu sechilmelidir.
//To da ancaq dekstopdaki folderler gorunur. hansi folder sechilse ora kopyalanma olacaq

//1. TaskIn Class______
//void CopyWithStream(string fromPath, string toPath)
//{
//    try
//    {
//        if (!string.IsNullOrEmpty(fromPath) && !string.IsNullOrEmpty(toPath))
//        {
//            if (!Directory.Exists(toPath) && !Path.HasExtension(toPath))
//            {
//                Directory.CreateDirectory(toPath);
//            }
//            if (File.Exists(fromPath) && Directory.Exists(toPath))
//            {
//                var readyPath = Path.GetExtension(fromPath);
//                if (!Path.HasExtension(toPath))
//                {
//                    toPath = toPath + @"\" + Path.GetFileNameWithoutExtension(fromPath) + "CopyFile" + readyPath; // or interpolation
//                }
//                using (var streamFrom = new FileStream(fromPath, FileMode.Open, FileAccess.Read))
//                {
//                    using (var streamToFile = new FileStream(toPath, FileMode.OpenOrCreate, FileAccess.Write))
//                    {
//                        var totalSize = streamFrom.Length; // download size total bu umumu file size
//                        var sizeOfArray = 10;
//                        var sizeForDownloaded = 0; // bu yuklendi
//                        byte[] buffer = new byte[sizeOfArray];
//                        do
//                        {
//                        sizeOfArray = streamFrom.Read(buffer, 0, sizeOfArray);
//                        streamToFile.Write(buffer, 0, sizeOfArray);
//                            sizeForDownloaded += sizeOfArray;
//                            Console.WriteLine($"{sizeForDownloaded}/{totalSize}");
//                        } while (sizeOfArray > 0);
//                    }

//                }
//                Console.WriteLine("Completed");
//            }
//            else
//            {
//                Console.Clear();
//                throw new Exception("Wrong Path");
//            }
//        }
//        else
//        {
//            Console.WriteLine("Path Can not Be Null");
//        }
//    }
//    catch (Exception ex) { Console.WriteLine(ex.Message); };
//}
//j:
//Console.Write("Enter File Path(From) : ");
//var from = Console.ReadLine();
//Console.Write("Enter Folder Path(To) : ");
//var to = Console.ReadLine();
//while (true)
//{
//    try
//    {
//        if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
//        {
//            try
//            {
//                Thread thread = new(() => CopyWithStream(from, to));
//                try
//                {
//                    thread.Start();
//                    thread.Join(); // bunu commetde qoya bilerik(switching i)  !!!nese sehf getse bunu commete alin zehemet olmasa
//                }
//                catch (Exception ex)
//                {
//                    Console.Clear();
//                    Console.WriteLine(ex.Message);
//                }
//            }
//            catch (Exception ex) { Console.Clear(); Console.WriteLine(ex.Message); }
//        }
//        else { Console.Clear(); Console.WriteLine("Path Can not Be Null. Try Again!"); ; goto j; };
//    }
//    catch (Exception ex)
//    {
//        Console.Clear();
//        Console.WriteLine(ex.Message);
//        goto j;
//    }
//    goto j;
//}

//2. HomeWork ___________
class Program
{
    static void Main() // task2 Main
    {
        string desktopPath = @"C:\Users\ASUS\Desktop"; 
        while (true)
        {
            Console.Clear();
            ShowDesktopFoldersAndFiles(desktopPath);
        }
    }

    static void ShowDesktopFoldersAndFiles(string path)
    {
        Console.WriteLine("Desktop Folders and Files : ");
        var directories = Directory.GetDirectories(path);
        var files = Directory.GetFiles(path);

        for (int i = 0; i < directories.Length; i++)
        {
            Console.WriteLine($"{i + 1}. Folder: {Path.GetFileName(directories[i])}");
        }
        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine($"{i + 1 + directories.Length}. File: {Path.GetFileName(files[i])}");
        }

        Console.WriteLine("\nSelect A Folder Or File (Number):");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0)
        {
            if (choice <= directories.Length)
            {
                ShowFolders(directories[choice - 1]);
            }
            else if (choice <= directories.Length + files.Length)
            {
                CopyFileToDesktopFolder(files[choice - directories.Length - 1], directories);
            }
            else
            {
                Console.WriteLine("Wrong Choice. Try Again.");
                Console.ReadLine();
            }
        }
    }

    static void ShowFolders(string folderPath)
    {
        Console.Clear();
        Console.WriteLine($"Folder Name : {Path.GetFileName(folderPath)}:");

        var directories = Directory.GetDirectories(folderPath);
        var files = Directory.GetFiles(folderPath);

        for (int i = 0; i < directories.Length; i++)
        {
            Console.WriteLine($"{i + 1}. Folder: {Path.GetFileName(directories[i])}");
        }

        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine($"{i + 1 + directories.Length}. File: {Path.GetFileName(files[i])}");
        }

        Console.WriteLine("\nSelect File For Copy Or Write 'q' For Return:");

        var choise = Console.ReadLine();
        if (choise is not null)
        {
            if (choise.ToLower().Equals("q"))
            {
                return;
            }
            if (int.TryParse(choise, out int choice) && choice > 0)
            {
                if (choice <= directories.Length)
                {
                    ShowFolders(directories[choice - 1]);
                }
                else if (choice <= directories.Length + files.Length)
                {
                    CopyFileToDesktopFolder(files[choice - directories.Length - 1], directories);
                }
                else
                {
                    Console.WriteLine("Wrong Choice. Returning To Menu...");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Wrong Input. Please Enter True Num.");
                Console.ReadLine();
            }
        }
        else { Console.WriteLine("Choise Can Not Be Null!"); };
    }

    static void CopyFileToDesktopFolder(string filePath, string[] directories)
    {
        Console.Clear();
        Console.WriteLine("Select Destination Folder (Desktop Folders Only(Enter Name)) : ");

        var desktopDirectories = Directory.GetDirectories("C:\\Users\\ASUS\\Desktop");
        foreach (var directory in desktopDirectories)
        {
            Console.WriteLine($"Folder: {Path.GetFileName(directory)}");
        }

        var destinationInput = Console.ReadLine();
        if (destinationInput is not null)
        {
            var selectedDestination = desktopDirectories.FirstOrDefault(d => Path.GetFileName(d.ToLower()).Equals(destinationInput.ToLower()));
            if (selectedDestination is not null)
            {
                string destinationFile = $"{selectedDestination}\\{Path.GetFileName(filePath)}"; // pathOfCopiedFile
                Thread thread = new(() =>
                {//thread burada istifade etdim. Cunki Copy Proccesini assync - a gore.
                    try
                    {
                        File.Copy(filePath, destinationFile, true);
                        Console.WriteLine($"File Copied To: {destinationFile}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error : {ex.Message}");
                    }
                }
            );
                thread.Start();
                thread.Join();
            }
            else
            {
                Console.WriteLine("Wrong Folder Choice.");
            }
            Console.WriteLine("\nPress Any Key For Return To Menu.");
            Console.ReadKey();
        }
    }
}

