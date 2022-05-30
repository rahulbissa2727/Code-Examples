using System.Diagnostics;
namespace HelloWorld
{
    class Program
    {
        static byte[] val = new byte[500000000];
        static long[] segVal = new long[Environment.ProcessorCount];
        static int segSize = val.Length / segVal.Length;
        static void GenerateVal() 
        {
            var rand = new Random(1000);
            for (int i = 0; i < val.Length; i++)
            {   
                val[i] = (byte)rand.Next(10);
            }
        }
        static void SegSum(object id) 
        {
            long res = 0;
            int idN = (int)id;
            for (int i = idN * segSize; i < (idN+1) * segSize; i++)
            {
                res += val[i];
            }
            segVal[idN] = res;
        }
        static void Main(string[] args)
        {
            GenerateVal();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            long sum = 0;
            for (int i = 0; i < val.Length; i++)
            {
                sum += val[i];
            }
            watch.Stop();

            Console.WriteLine("Sum without threading is : " + sum + 
            ". And time taken is " + watch.Elapsed);

            watch.Reset();
            watch.Start();

            Thread[] threads = new Thread[Environment.ProcessorCount];
            for (int i = 0;i < segVal.Length; i++)
            {
                threads[i] = new Thread(SegSum);
                threads[i].Start(i);   
            }
            for (int i = 0; i < segVal.Length; i++)
            {
                threads[i].Join();
            }
            long res = 0;
            for (int i = 0; i < segVal.Length; i++)
            {
                res += segVal[i];
            }

            watch.Stop();

            Console.WriteLine("Sum of all values is " + res + 
            ". And Time taken is " + watch.Elapsed);
        }
       
    }
}
