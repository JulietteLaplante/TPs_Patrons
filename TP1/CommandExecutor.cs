using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tp1
{
    public class CommandExecutor
    {
        private static CommandExecutor instance;

        private SemaphoreSlim maxThread;
        private Queue<Command> queue;


        private CommandExecutor(int nbThreads)
        {
            this.maxThread = new SemaphoreSlim(0,nbThreads);
            this.queue = new Queue<Command>();
            Thread thread = new Thread(new ThreadStart(ExecuteCommands));
            thread.Start();
        }
        public static CommandExecutor GetInstance()
        {
            if(instance == null)
            {
                instance = new CommandExecutor(4);
            }
            return instance;
        }
        public static void InitCommandExecutor(int nbThreads)
        {
            if(instance == null)
            {
                instance = new CommandExecutor(nbThreads);
            }
        }

        public void AddToQueue(Command command)
        {
            queue.Enqueue(command);
        }

        private void ExecuteCommands()
        {
            while (true)
            {
                maxThread.Wait();
                Task.Factory.StartNew(() =>
                {
                    queue.Dequeue().Execute();
                }
                    , TaskCreationOptions.LongRunning)
                .ContinueWith((task) => maxThread.Release());
            }
        }

        //https://stackoverflow.com/a/14075286
    }
}
