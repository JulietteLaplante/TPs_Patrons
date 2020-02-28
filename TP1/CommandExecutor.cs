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
            this.maxThread = new SemaphoreSlim(nbThreads);
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
                if (queue.Count > 0)
                {
                    maxThread.Wait();
                    Command c = queue.Dequeue();
                    Task.Factory.StartNew(() =>
                    {
                        c.Execute();
                    }
                        , TaskCreationOptions.LongRunning)
                    .ContinueWith((task) => {
                        maxThread.Release();
                        c.Completed();
                        });
                }
            }
        }

        //https://stackoverflow.com/a/14075286
    }
}
