namespace InterThreadCommunicationInCSharp
{
    internal class Program
    {
        const int numberLimit = 10;
        static readonly object _lockObject = new object();
        static void Main(string[] args)
        {
            #region Example - 1 
            //Thread EvenThread = new Thread(PrintEvenNumbers);
            //Thread OddThread = new Thread(PrintOddNumbers);

            //EvenThread.Start();
            //Thread.Sleep(100);

            //OddThread.Start();

            //OddThread.Join();
            //EvenThread.Join();

            //Console.ReadKey();
            #endregion

            #region Example - 2 Interthread Communication Example using Wait() and Pulse() Methods in C#
            Thread threadObj = new Thread(PrintTable)
            {
                Name="Manual Thread"
            };
            threadObj.Start();

            lock(_lockObject)
            {
                Monitor.Wait(_lockObject);

                Thread th = Thread.CurrentThread;
                th.Name = "Main Thread";

                Console.WriteLine($"{th.Name} Running and Printing the Table of 5.");
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine("5 X " + i + " = " + (5 * i));
                }
            }
            Console.ReadKey();
            #endregion
        }

        #region Example - 1
        static void PrintEvenNumbers()
        {
            try
            {
                Monitor.Enter(_lockObject);
                for (int i = 0; i < numberLimit; i = i + 2)
                {
                    Console.Write($"{i} ");
                    Monitor.Pulse(_lockObject);

                    bool isLast = false;
                    if (i == numberLimit)
                    {
                        isLast = true;
                    }

                    if (!isLast)
                    {
                        Monitor.Wait(_lockObject);
                    }
                }
            }
            finally
            {
                Monitor.Exit(_lockObject);
            }
        }

        static void PrintOddNumbers()
        {
            try
            {
                Monitor.Enter(_lockObject);
                for (int i = 1; i < numberLimit; i = i + 2)
                {
                    Console.Write($"{i} ");
                    Monitor.Pulse(_lockObject);

                    bool isLast = false;
                    if (i == numberLimit - 1)
                    {
                        isLast = true;
                    }

                    if (!isLast)
                    {
                        Monitor.Wait(_lockObject);
                    }
                }
            }
            finally
            {
                Monitor.Exit(_lockObject);
            }
        }

        #endregion

        #region Example - 2 Interthread Communication Example using Wait() and Pulse() Methods in C#
        public static void PrintTable()
        {
            lock (_lockObject)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} Running and Printing the Table of 4.");
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine("4 x " + i + " = " + (4 * i));
                }
                Monitor.Pulse(_lockObject);
            }
        }
        #endregion
    }
}