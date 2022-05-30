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
                count++;
                Console.WriteLine("Thread ID" + Thread.CurrentThread.ManagedThreadId + 
                " Current count is " + count);
                Thread.Sleep(1000);
            }
        }
    }
}
