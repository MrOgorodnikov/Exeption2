using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Caching;

namespace Exeption2
{
    class Program
    {       
        static void Main(string[] args)
        {
            var files = Directory.GetFiles(".");
            string newName;
            var formats = new List<string>();
            formats.Add(ImageFormat.Bmp.ToString());
            formats.Add(ImageFormat.Emf.ToString());
            formats.Add(ImageFormat.Exif.ToString());
            formats.Add(ImageFormat.Gif.ToString());
            formats.Add(ImageFormat.Icon.ToString());
            formats.Add(ImageFormat.Jpeg.ToString());
            formats.Add(ImageFormat.MemoryBmp.ToString());
            formats.Add(ImageFormat.Png.ToString());
            formats.Add(ImageFormat.Tiff.ToString());
            formats.Add(ImageFormat.Wmf.ToString());
            int successCount = 0, errorCount = 0;
            ThreadPool.SetMaxThreads(20, 10);
            var time = new Stopwatch();
            time.Start();
            Parallel.ForEach(files, (file) =>
                                    {
                                        try
                                        {
                                            Bitmap image = new Bitmap(file);
                                            image.RotateFlip(RotateFlipType.Rotate180FlipX);
                                            newName = Path.GetFileNameWithoutExtension(file) + "-mirrored";  

                                            image.Save($@".\mirr\{newName}.gif", ImageFormat.Gif);
                                           // image.Save($@"D:\Test\mirr\{newName}.gif", ImageFormat.Gif);
                                            image.Dispose();
                                            GC.Collect();
                                            successCount++;                                            
                                            
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine($"{file.ToString()} Success");
                                            Console.ResetColor();
                                        }
                                        catch (Exception e)
                                        {
                                            GC.Collect();
                                            errorCount++;
                                            var x = Path.GetExtension(file);
                                            if (formats.Contains(x))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine($"{file} is not image");
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine($"{file}");
                                            }
                                            
                                            Console.ResetColor();
                                        }
                                    });


            //foreach (var file in files)
            //{
            //    try
            //    {
            //        Bitmap image = new Bitmap(file);
            //        image.RotateFlip(RotateFlipType.Rotate180FlipX);

            //        var newName = Path.GetFileNameWithoutExtension(file);
            //        //var newName = file.Remove(file.LastIndexOf('.')) + "-mirrored";

            //        //image.Save($@".\mirr\{newName}.gif", ImageFormat.Gif);
            //        image.Save($@"D:\Test\mirr\{newName}.gif", ImageFormat.Gif);
            //        image.Dispose();

            //        Console.ForegroundColor = ConsoleColor.Green;
            //        Console.WriteLine($"{file.ToString()} Success");
            //        Console.ResetColor();
            //    }
            //    catch (Exception e)
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine($"{file} is not image");
            //        Console.ResetColor();
            //    }
            //}


            time.Stop();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("           ");
            Console.WriteLine($"{time.ElapsedTicks} ticks");
            Console.WriteLine($"{time.ElapsedMilliseconds} milliseconds");
            //double success = successCount / files.Count();
            Console.WriteLine($"success: {successCount }");
            Console.WriteLine($"error: {errorCount}");
            Console.WriteLine($"total: {files.Count()}");
            Console.ReadLine();
        }
    }
}
