using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AssignmentDay2
{
    public class AsyncPrimeNumber
    {
        List<int> numbers = new List<int>();
        List<int> primeNumbers = new List<int>();

        public async Task ShowMenu()
        {
            int nFrom = 0;
            int nTo = 0;
            Console.WriteLine("Range started");
            Console.Write("From:");
            if (int.TryParse(Console.ReadLine(), out int from))
            {
                nFrom = from;
            }
            Console.Write("To:");
            if (int.TryParse(Console.ReadLine(), out int to))
            {
                nTo = to;
            }
            GetNumbers(nFrom, nTo);

            // Tạo 1 mảng mang giá trị true/false và có số phần tử = với số phần tử của numbers.
            Task<bool>[] primeCheckTasks = new Task<bool>[numbers.Count];

            // Vòng lặp kiểm tra sô nguyên tố, lưu giá trị true/false vào mảng.
            for (int i = 0; i < numbers.Count; i++)
            {
                primeCheckTasks[i] = IsPrimeAsync(numbers[i]);
            }

            // WhenAll() - Trả về MẢNG các giá trị true/false.
            bool[] results = await Task.WhenAll(primeCheckTasks);

            // In ra các số nguyên tố
            for (int i = 0; i < numbers.Count; i++)
            {
                if (results[i])
                {
                    Console.Write(numbers[i] + " ");
                }
            }

            Console.WriteLine("\n");

            // WaitAll() - Trả về kết quả khi tất cả các Tasks đã hoàn thành.
            Task.WaitAll(primeCheckTasks);
            Console.WriteLine("All tasks have completed");

            // WhenAny() - Trả về giá trị true/false của Task đầu tiên hoàn thành.
            Task<bool> firstCompletedTask = await Task.WhenAny(primeCheckTasks);
            Console.WriteLine($"First task have completed and {(firstCompletedTask.Result ? "it is" : "it is not")} a prime number");

            // WaitAny() - Trả về index của Task đầu tiên hoàn thành.
            int completedTaskIndex = Task.WaitAny(primeCheckTasks);
            Console.WriteLine($"First completed task is number {numbers[completedTaskIndex]} and {(primeCheckTasks[completedTaskIndex].Result ? "it is" : "it is not")} a prime number");

        }

        public void GetNumbers(int from, int to)
        {
            for ( int i = from; i < to; i++ )
            {
                numbers.Add(i);
            }
        }

        // Asynchronous function to check if a number is prime
        static async Task<bool> IsPrimeAsync(int number)
        {
            // Perform the prime check in a separate task to avoid blocking
            return await Task.Run(() =>
            {
                if (number < 2) return false;

                // Check for factors from 2 to square root of the number
                for (int i = 2; i <= Math.Sqrt(number); i++)
                {
                    if (number % i == 0)
                    {
                        // If a factor is found, the number is not prime
                        return false;
                    }
                }
                // If no factors found, the number is prime
                return true;
            });
        }
    }
}
