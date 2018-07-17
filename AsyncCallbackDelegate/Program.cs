using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCallbackDelegate
{
    public delegate int BinaryOp(int x, int y);

    class Program
    {
        private static bool isDone = false;




        //#region Default StaticMain Method
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("***** AsyncCallbackDelegate Example *****");
        //    Console.WriteLine("Main() invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);

        //    BinaryOp b = new BinaryOp(Add);
        //    IAsyncResult itfAR = b.BeginInvoke(10, 10, new AsyncCallback(AddComplete), null);

        //    // Assume other work is performed here...
        //    while (!isDone)
        //    {
        //        Thread.Sleep(1000);
        //        Console.WriteLine("Working....");
        //    }
        //    Console.ReadLine();
        //}

        //#endregion

        #region Modified StaticMain Method
        static void Main(string[] args)
        {
            Console.WriteLine("***** AsyncCallbackDelegate Example *****");
            Console.WriteLine("Main() invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);

            BinaryOp b = new BinaryOp(Add);
            IAsyncResult itfAR = b.BeginInvoke(10, 10, new AsyncCallback(AddComplete),
            "Main() thanks you for adding these numbers.");

            // Assume other work is performed here...
            while (!isDone)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Working....");
            }
            Console.ReadLine();
        }

        #endregion

        static int Add(int x, int y)
        {
            Console.WriteLine("Add() invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
            return x + y;
        }


        #region Default AddComplete Method
        //static void AddComplete(IAsyncResult itfAR)
        //{
        //    Console.WriteLine("AddComplete() invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);
        //    Console.WriteLine("Your addition is complete");
        //    isDone = true;
        //}
        #endregion

        #region AsyncResultClass
        //The Role of the AsyncResult Class
        //static void AddComplete(IAsyncResult itfAR)
        //{
        //    Console.WriteLine("AddComplete() invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);
        //    Console.WriteLine("Your addition is complete");

        //    // Now get the result.
        //    AsyncResult ar = (AsyncResult)itfAR;
        //    BinaryOp b = (BinaryOp)ar.AsyncDelegate;
        //    Console.WriteLine("10 + 10 is {0}.", b.EndInvoke(itfAR));
        //    isDone = true;
        //}

        #endregion

        #region Modified-AsyncResultClass
        //The Role of the AsyncResult Class
        static void AddComplete(IAsyncResult itfAR)
        {
            Console.WriteLine("AddComplete() invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Your addition is complete");

            // Now get the result.
            AsyncResult ar = (AsyncResult)itfAR;
            BinaryOp b = (BinaryOp)ar.AsyncDelegate;
            Console.WriteLine("10 + 10 is {0}.", b.EndInvoke(itfAR));
            isDone = true;

            // Retrieve the informational object and cast it to string.
            string msg = (string)itfAR.AsyncState;
            Console.WriteLine(msg);
            isDone = true;
        }

        #endregion
    }
}
