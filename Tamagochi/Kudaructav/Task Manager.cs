using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace MyprocessSample
{
    class MyProcess
    {
        static void DrawTextProgressBar(int progress, int total)
        {
            Console.CursorLeft = 0;
            Console.Write("[");
            Console.CursorLeft = 32;
            Console.Write("]");
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        static void Main(string[] args)
        {
            PerformanceCounter cpuCounter;
            PerformanceCounter ramCounter;
            Process[] processes = Process.GetProcesses();
            var counters = new List<PerformanceCounter>();
            int Cnt = 0;
            int CPUUsage = 0;
            int AvailableRAM = 0;
            Console.Write("Choose mode:\n1 - Show CPUUsage and Available RAM;\n2 - Show all Information about processes\n");
            int k = Convert.ToInt16(Console.ReadLine());
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            double getCurrentCpuUsage()
            {
                return cpuCounter.NextValue();
            }

            double getAvailableRam()
            {
                return ramCounter.NextValue();
            }

            switch (k)
            {
                case 1:
                    while (Console.KeyAvailable == false)
                    {

                        CPUUsage = Convert.ToInt16(getCurrentCpuUsage());
                        AvailableRAM = Convert.ToInt16(getAvailableRam());
                        Console.WriteLine("Press any key to exit");
                        Console.WriteLine("CPUUsage: {0}", CPUUsage + " %");
                        DrawTextProgressBar(CPUUsage, 100);
                        Console.WriteLine("\n\nAvailable RAM: {0} ", getAvailableRam() + " MB");
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                    break;

                case 2:
                    Console.WriteLine("Press any key to exit");
                    Console.WriteLine($"{"ID",5}{"Name",50}{"RAM",11}{"CPUUsage",15}\n");

                    foreach (Process process in Process.GetProcesses())
                    {

                        var counter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
                        counter.NextValue();
                        counters.Add(counter);
                    }

                    Thread.Sleep(1000);

                    try
                    {
                        foreach (var counter in counters)
                        {
                            if (processes[Cnt].Id == 0)
                                Console.WriteLine("");
                            else
                            {
                                Console.WriteLine("{0,6}{1,50}{2,10}{3,12:0}", processes[Cnt].Id, processes[Cnt].ProcessName,
                                    (processes[Cnt].WorkingSet64 / 1000) + " KB", Convert.ToInt16(counter.NextValue()) + " %");
                                Cnt++;
                            }
                        }
                    }

                    catch (System.IndexOutOfRangeException)
                    { }
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            Console.ReadKey();
        }
    }
}


