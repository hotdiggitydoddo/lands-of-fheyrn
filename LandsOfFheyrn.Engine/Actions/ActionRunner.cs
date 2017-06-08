using LandsOfFheyrn.Engine.Scripting;
using MoonSharp.Interpreter;

namespace LandsOfFheyrn.Engine.Actions
{
    public class ActionRunner
    {
        private Script _script;
        public string ActionName {get;}

        public ActionRunner(string name)
        {
            ActionName = name;
        }

        public void Init()
        {
            var script = new Script();
            script.Globals["MudAction"] = typeof(LOFAction);
            script.Globals["TimedMudAction"] = typeof(TimedLOFAction);
            script.DoString(ScriptManager.Instance.GetScript(ScriptType.ActionRunner, ActionName));
            _script = script;
        }
        
        public void Run(LOFAction action)
        {
            _script.Call(_script.Globals["run"], action);
        }

    }
}