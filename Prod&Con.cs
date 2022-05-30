namespace HelloWorld
{
    class Program
    {
        static Queue<int> q = new Queue<int>();
        static int[] val = new int[3];
        static void Producer()
        {
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                q.Enqueue(rand.Next(10));
                Thread.Sleep(1000);
            }
        }
        static void Consumer(object id) 
        {
            DateTime t = DateTime.Now;
            int idn = (int) id;
            int sum = 0;
            while((DateTime.Now - t).Seconds < 11)
            {
                lock(q)
                {
                    if(q.Count != 0)
                    {
                        int num = q.Dequeue();
                        sum += num;
                        Console.WriteLine("Thread No. " +
                        Thread.CurrentThread.ManagedThreadId + " added this value: " + num + 
                        " and thread sum becomes: " + sum);
                    }
                }
            }
            val[idn] = sum;
        }
        static void Main(string[] arg)
        {
            Thread producerT = new Thread(Producer);
            producerT.Start();

            Thread[] consumerT = new Thread[3];

            for(int i = 0; i < consumerT.Length; i++)
            {
                consumerT[i] = new Thread(Consumer);
                consumerT[i].Start(i);
            }
            for (int i = 0; i < consumerT.Length; i++)
                consumerT[i].Join();
            int res = 0;
            for(int i = 0; i < consumerT.Length; i++)
                res += val[i];
            Console.WriteLine("Toatal Sum: " + res);
        }
    }
}
