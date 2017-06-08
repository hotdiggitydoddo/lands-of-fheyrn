using System.Threading;
using LandsOfFheyrn.Engine.Actions;
using LandsOfFheyrn.Engine.Managers;

namespace LandsOfFheyrn.Engine
{
    public class Game
    {
        TimerRegistry _timerRegistry;
        Timer _timer;
        long _lastTime;
        bool _updating;

        CommandManager _cmdMgr;
        ComponentManager _cmpMgr;
        ScriptManager _scriptMgr;
        EntityManager _entMgr;

        public long TimeRunning { get; private set; }
        public long CurrentTime { get; private set; }

        public Game()
        {
            _timerRegistry = new TimerRegistry();
            _scriptMgr = new ScriptManager();
            _entMgr = new EntityManager();
            _cmdMgr = new CommandManager(_scriptMgr);
            _cmpMgr = new ComponentManager(_scriptMgr);

            _scriptMgr.RefreshScripts(ScriptType.Game);
            _scriptMgr.RefreshScripts(ScriptType.LOFComponent);
            _scriptMgr.RefreshScripts(ScriptType.LOFCommand);
        }

        public void Init()
        {
            _cmdMgr.LoadAllCommands();
        }
    }
}