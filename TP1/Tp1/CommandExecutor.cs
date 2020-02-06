using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Tp1
{
    public class CommandExecutor
    {
        private static CommandExecutor instance;

        private Semaphore maxThread;
        private List<Command> queue;


        private CommandExecutor(int nbThreads)
        {
            this.maxThread = new Semaphore(0,nbThreads);
            this.queue = new List<Command>();
        }
        public static CommandExecutor GetInstance()
        {
            if(instance == null)
            {
                throw new Exception("Command Executor hasn't been initialized");
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
            queue.Add(command);
        }

        //https://stackoverflow.com/a/14075286
    }
}
