
using System;
using System.Diagnostics;
using System.Threading.Tasks;

class KnapsackProblem
{
    static int KnapsackWithoutThreads(int[] values, int[] weights, int capacity)
    {
        int n = values.Length;
        int[,] K = new int[n + 1, capacity + 1];

        for (int i = 0; i <= n; i++)
        {
            for (int w = 0; w <= capacity; w++)
            {
                if (i == 0 || w == 0)
                    K[i, w] = 0;
                else if (weights[i - 1] <= w)
                    K[i, w] = Math.Max(values[i - 1] + K[i - 1, w - weights[i - 1]], K[i - 1, w]);
                else
                    K[i, w] = K[i - 1, w];
            }
        }

        return K[n, capacity];
    }

    //          Wersia z równoległym wykonywaniem ale na Parallel a nie Thread
    //static int KnapsackWithThreads(int[] values, int[] weights, int capacity)
    //{
    //    int n = values.Length;
    //    int[,] K = new int[n + 1, capacity + 1];

    //    for (int i = 0; i <= n; i++)
    //    {
    //        Parallel.For(0, capacity + 1, w =>
    //        {
    //            if (i == 0 || w == 0)
    //                K[i, w] = 0;
    //            else if (weights[i - 1] <= w)
    //                K[i, w] = Math.Max(values[i - 1] + K[i - 1, w - weights[i - 1]], K[i - 1, w]);
    //            else
    //                K[i, w] = K[i - 1, w];
    //        });
    //    }

    //    return K[n, capacity];
    //}

    static int KnapsackWithThreads(int[] values, int[] weights, int capacity)
    {
        int n = values.Length;
        int[,] K = new int[n + 1, capacity + 1];

        int threadsCount = Math.Min(n, 14);
        int rowsPerThread = n / threadsCount;

        List<Thread> threads = new List<Thread>();

        for (int t = 0; t < threadsCount; t++)
        {
            int start = t * rowsPerThread;
            int end = (t == threadsCount - 1) ? n : start + rowsPerThread;

            Thread thread = new Thread(() =>
            {
                for (int i = start + 1; i <= end; i++)
                {
                    for (int w = 0; w <= capacity; w++)
                    {
                        if (weights[i - 1] <= w)
                            K[i, w] = Math.Max(values[i - 1] + K[i - 1, w - weights[i - 1]], K[i - 1, w]);
                        else
                            K[i, w] = K[i - 1, w];
                    }
                }
            });

            threads.Add(thread);
            thread.Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        return K[n, capacity];
    }



    static void Main(string[] args)
    {
        int n = 10000;
        int capacity = 1000;
        int[] values = new int[n];
        int[] weights = new int[n];
        Random rnd = new Random();

        for (int i = 0; i < n; i++)
        {
            values[i] = rnd.Next(1, 101);
            weights[i] = rnd.Next(1, 101);
        }


        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        int resultWithoutThreads = KnapsackWithoutThreads(values, weights, capacity);
        stopwatch.Stop();
        Console.WriteLine("Wynik bez wątków: {0}, czas wykonania: {1}ms", resultWithoutThreads, stopwatch.ElapsedMilliseconds);

        stopwatch.Reset();

        stopwatch.Start();
        int resultWithThreads = KnapsackWithThreads(values, weights, capacity);
        stopwatch.Stop();
        Console.WriteLine("Wynik z wątkami: {0}, czas wykonania: {1}ms", resultWithThreads, stopwatch.ElapsedMilliseconds);
    }
}