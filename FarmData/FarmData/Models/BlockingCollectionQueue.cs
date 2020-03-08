using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FarmData.Models
{
    class TaskManager
    {
        private static bool running = false;
        private static List<Task<bool>> Jobs = new List<Task<bool>>();
        private static Dictionary<Task<bool>, bool> Responses = new Dictionary<Task<bool>, bool>();

        public static async void Run()
        {
            Responses = new Dictionary<Task<bool>, bool>();
            running = true;
            foreach(var Job in Jobs)
            {
                bool output = await Job;
                Responses[Job] = output;
            }
            running = false;
            Jobs = new List<Task<bool>>();
        }

        public static void Add(Task<bool> task)
        {
            Jobs.Add(task);
        }
        public static async Task<bool> Get(Task<bool> task)
        {
            while(running){}
            bool temp = true;
            return Responses[task];
        }


    }
    /*class TaskQueue
    {
        private BlockingCollection<Task<object>> _jobs = new BlockingCollection<Task<object>>();

        public TaskQueue()
        {
            var thread = new Thread(new ThreadStart(OnStart));
            thread.IsBackground = true;
            thread.Start();
        }

        public void Enqueue(Task<object> job)
        {
            //Task<object> reponse
            _jobs.Add(job);
        }

        private async void OnStart()
        {
            foreach (var job in _jobs.GetConsumingEnumerable(CancellationToken.None))
            {
                var resposne = await job;
            }
        }
    }*/
}
