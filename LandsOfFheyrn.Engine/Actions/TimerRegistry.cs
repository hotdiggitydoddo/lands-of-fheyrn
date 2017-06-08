using System;

namespace LandsOfFheyrn.Engine.Actions
{
    public class TimerRegistry : PriorityQueue<TimedLOFAction>
    {
        public void Add(TimedLOFAction action)
        {
            Enqueue(action);
        }

        public void Dispatch()
        {
            if (Count() == 0 || Peek().DispatchTime > DateTime.UtcNow.Ticks)
                return;

            while (Count() > 0)
            {
                while (Peek().DispatchTime <= DateTime.UtcNow.Ticks)
                {
                    var action = Dequeue();

                    if (!action.IsValid)
                        break;
                    
                    action.Unhook();
                    Game.Instance.DoAction(action);
                        break;
                }
            }
        }
    }
}