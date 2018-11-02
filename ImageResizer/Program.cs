using PhotoSauce.MagicScaler;
using System;
using System.IO;

namespace ImageResizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string folder;
            do
            {
                Console.Clear();
                Console.WriteLine("Source Folder <empty to exit>: ");
                folder = Console.ReadLine();

                var fileList = Directory.GetFiles(folder);
                if (fileList.Length > 0)
                {
                    Console.WriteLine($"\t{fileList.Length} files available.");

                    Console.WriteLine($"Inform destination folder:");
                    string destination = Console.ReadLine();

                    if (string.IsNullOrEmpty(destination))
                    {
                        destination = Path.Combine("D:\\sandbox", (new DirectoryInfo(folder)).Name);
                        Console.WriteLine($"\tAssuming default Sandbox\\<folder>");
                    }

                    if (!Directory.Exists(destination))
                        Directory.CreateDirectory(destination);

                    foreach (var sourceFile in fileList)
                    {

                        using (var outStream = new FileStream(Path.Combine(destination, $"{Path.GetFileNameWithoutExtension(sourceFile)}_small.JPG"), FileMode.Create))
                        {
                            ImageFileInfo mi = new ImageFileInfo(sourceFile);

                            Console.SetCursorPosition(1, 10);
                            Console.Write($"\tReducing {Path.GetFileName(sourceFile)} => {Path.GetFileName(outStream.Name)} ");

                            if (mi.Frames[0].Height > mi.Frames[0].Width)
                                MagicImageProcessor.ProcessImage(sourceFile, outStream, new ProcessImageSettings { Width = 2000, Height = 2992 });
                            else
                                MagicImageProcessor.ProcessImage(sourceFile, outStream, new ProcessImageSettings { Width = 2992, Height = 1800 });

                            mi = null;
                            //Console.ForegroundColor = ConsoleColor.Green;
                            //Console.SetCursorPosition(1, 10);
                            //Console.Write($"[OK]");
                        }
                    }
                }
                else
                    Console.WriteLine($"\t No files selected, closing tool.");
            }
            while (!string.IsNullOrEmpty(folder));

        }
    }
}
