using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int numThreads = 4;             // Liczba wątków
        int iterationsPerThread = 10000;// Liczba iteracji na każdy wątek
        double[] results = new double[numThreads]; // Tablica wyników dla każdego wątku

        Stopwatch sw = Stopwatch.StartNew();  // Uruchomienie stopera

        Thread[] threads = new Thread[numThreads]; // Tablica wątków
        for (int i = 0; i < numThreads; i++)
        {
            int threadIndex = i;    // Utworzenie lokalnej kopii indeksu wątku dla każdego wątku
            int start = threadIndex * iterationsPerThread; // Początek przedziału iteracji dla wątku
            int end = start + iterationsPerThread - 1;     // Koniec przedziału iteracji dla wątku
            threads[threadIndex] = new Thread(() =>
            {
                for (int j = start; j <= end; j++)
                {
                    results[threadIndex] += 1 / Factorial(j); // Obliczenie wyniku dla danego wątku
                }
            });
            threads[threadIndex].Start(); // Uruchomienie wątku
        }
        

        return K[n, capacity];
    }

        double eulerNumber = 0;  // Obliczenie ostatecznego wyniku z wyników każdego wątku
        foreach (double result in results)
        {
            eulerNumber += result;
        }
        sw.Stop();  // Zatrzymanie stopera
        Console.WriteLine("Euler number using threads: " + eulerNumber); // Wyświetlenie wyniku
        Console.WriteLine("Time taken using threads: " + sw.ElapsedMilliseconds + " ms"); // Wyświetlenie czasu wykonania
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

        int threadsCount = Math.Min(n, 3);
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

        sw = Stopwatch.StartNew(); // Uruchomienie stopera

        double eulerNumber2 = 0; // Obliczenie wyniku bez użycia wątków
        for (int j = 0; j < numThreads * iterationsPerThread; j++)
        {
            eulerNumber2 += 1 / Factorial(j);
        }

        sw.Stop(); // Zatrzymanie stopera
        Console.WriteLine("Euler number without threads: " + eulerNumber2); // Wyświetlenie wyniku
        Console.WriteLine("Time taken without threads: " + sw.ElapsedMilliseconds + " ms"); // Wyświetlenie czasu wykonania
    }


    private static readonly object _lock = new object();

    static void Main(string[] args)
    {
        int n = 1000000;
        int capacity = 1000;
        int[] values = new int[n];
        int[] weights = new int[n];
        Random rnd = new Random();

        for (int i = 2; i <= n; i++)
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
