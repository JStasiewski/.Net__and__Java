using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int numThreads = 4;             // Liczba wątków
        int iterationsPerThread = 35000;// Liczba iteracji na każdy wątek
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

        foreach (Thread thread in threads)
        {
            thread.Join(); // Oczekiwanie na zakończenie działania każdego wątku
        }

        double eulerNumber = 0;  // Obliczenie ostatecznego wyniku z wyników każdego wątku
        foreach (double result in results)
        {
            eulerNumber += result;
        }
        sw.Stop();  // Zatrzymanie stopera
        Console.WriteLine("Euler number using threads: " + eulerNumber); // Wyświetlenie wyniku
        Console.WriteLine("Time taken using threads: " + sw.ElapsedMilliseconds + " ms"); // Wyświetlenie czasu wykonania

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

    static double Factorial(int n) // Funkcja obliczająca silnię
    {
        double result = 1;

        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }
}
