**Asynchronized Threading**

namespace HelloWorld
{
    class Program
    {
        static int count = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var thread1 = new Thread(IncrementCount);
            var thread2 = new Thread(IncrementCount);

            thread1.Start();
            Thread.Sleep(500);
            thread2.Start();
        }
        static void IncrementCount()
        {
            while(count < 10)
            {
                int temp = count;
                Thread.Sleep(1000);
                count = temp + 1;
                Console.WriteLine("Thread ID" + Thread.CurrentThread.ManagedThreadId + 
                " Current count is " + count);
                Thread.Sleep(1000);
            }
        }
    }
}

**Basic Synchronization**

namespace HelloWorld
{
    class Program
    {
        static int count = 0;
        static object baton = new object();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var thread1 = new Thread(IncrementCount);
            var thread2 = new Thread(IncrementCount);

            thread1.Start();
            Thread.Sleep(500);
            thread2.Start();
        }
        static void IncrementCount()
        {
            while(count < 10)
            {
                lock(baton)
                {
                    int temp = count;
                    Thread.Sleep(1000);
                    count = temp + 1;
                    Console.WriteLine("Thread ID" + Thread.CurrentThread.ManagedThreadId + 
                    " Current count is " + count);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
