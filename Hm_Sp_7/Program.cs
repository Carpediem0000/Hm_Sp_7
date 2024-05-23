using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Hm_Sp_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\Study\C#\СистемноеПрогр\ImgBefore";
            string newPath = @"D:\Study\C#\СистемноеПрогр\ImgAfter";

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            if (Directory.Exists(path))
            {
                string[] pngFiles = Directory.GetFiles(path, "*.png");

                Task[] tasks = new Task[pngFiles.Length];

                for (int i = 0; i < pngFiles.Length; i++)
                {
                    int index = i;
                    tasks[i] = Task.Run(() =>
                    {
                        RotateImg(pngFiles[index], newPath, index);
                    });
                }

                Task.WaitAll(tasks);

                Console.WriteLine("Confirm ;)");
            }

            // Проблема
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() => Console.WriteLine(i));
            }
            Thread.Sleep(3000);
            Console.WriteLine("--------------------------------------");
            // Уже лучше но все-равно выходит за массив. Как оно захватывает числа так и не понял
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Task.Run(() => Console.WriteLine(i));
            }
            Thread.Sleep(3000);
            Console.WriteLine("--------------------------------------");
            // Решение
            for (int i = 0; i < 10; i++)
            {
                int localI = i; 
                Task.Run(() => Console.WriteLine(localI));
            }
            Console.ReadLine();
        }

        private static async void RotateImg(string filePath, string newfolderPath, int index)
        {
            using (Bitmap image = new Bitmap(filePath))
            {
                image.RotateFlip(RotateFlipType.Rotate180FlipX);
                image.Save(newfolderPath + $@"\png{index + 1}.png");
            }
        }
    }
}
