using System;
using System.Collections.Generic;
using System.Linq;
using LandsOfFheyrn.Engine.Actions;
using MoonSharp.Interpreter;

namespace LandsOfFheyrn.Engine.Managers
{
    public class CommandManager
    {
        private Dictionary<int, List<string>> _entityCommands;
        private Dictionary<string, Script> _commandSet;
        private ScriptManager _scripts;
        public CommandManager(ScriptManager scripts)
        {
            _commandSet = new Dictionary<string, Script>();
            _entityCommands = new Dictionary<int, List<string>>();
            _scripts = scripts;
        }

         public void LoadAllCommands()
        {
            _commandSet.Clear();
            var allScripts = _scripts.GetCommandScripts();

            foreach (var kvp in allScripts)
            {
                var script = new Script();
                script.Globals["Game"] = typeof(Game);
                script.Globals["MudAction"] = typeof(LOFAction);

                try
                {
                    script.DoString(kvp.Value);
                }
                catch (Exception ex)
                {

                }
                _commandSet.Add(kvp.Key, script);
            }
        }

        public void AssignCommand(int entityId, string cmdName)
        {
            if (!_entityCommands.ContainsKey(entityId))
                _entityCommands.Add(entityId, new List<string> { cmdName });
            else if (!_entityCommands[entityId].Contains(cmdName))
                _entityCommands[entityId].Add(cmdName);
        }

        //for serialization
        public List<string> GetCommands(int entityId)
        {
            if (!_entityCommands.ContainsKey(entityId))
                return new List<string>();
            return new List<string>(_entityCommands[entityId]);
        }

        public void Process(int entityId, string input)
        {
            var parts = input.Trim().Split(' ').ToList();
            var verb = parts[0].ToLower();
            parts.RemoveAt(0);
            
            if (!Parse(entityId, ref verb))
                return;

            Execute(entityId, verb, string.Join(" ", parts));
        }

        private bool Parse(int entityId, ref string verb)
        {
            //this method should really return a custom object that has the command name and
            //typed and named arguments

            if (!_entityCommands.ContainsKey(entityId))
                return false;

            if (verb == "commands")
            {
                //need to communicate available commands to player.
                Game.Instance.DoAction(new MudAction("infotoplayer", entityId, 0, string.Join(", ", GetCommands(entityId))));
                return false;
            }
            if (!_commandSet.ContainsKey(verb))
                //Send error to client -- command doesn't exist
                return false;
            if (!_entityCommands[entityId].Contains(verb))
                //entity doesn't know this command
                return false;
            return true;
        }

        private void Execute(int entityId, string cmdName, params string[] args)
        {
            var script = _commandSet[cmdName];
            try
            {
                script.Call(script.Globals["execute"], entityId, args);
            }
            catch (ScriptRuntimeException ex)
            {

            }
        }
    }
}
