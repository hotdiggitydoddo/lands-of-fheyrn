using System;
using LandsOfFheyrn.Engine.Actions;
using MoonSharp.Interpreter;

namespace LandsOfFheyrn.Engine.Objects
{
    public class LOFComponent
    {
		readonly Script _script;
		
        public string Name { get; }
		public LOFEntity Owner { get; private set; }
		public bool IsActive { get; set; }
		
        public LOFComponent(LOFEntity owner, string name, Script script)
		{
			Owner = owner;
			Name = name;
			_script = script;
		}

		public virtual bool DoAction(LOFAction action)
		{
			return _script.Call(_script.Globals["do"], action).Boolean;
		}

		public virtual void Tick(long elapsed)
		{
			if (!IsActive) return;
			_script.Call(_script.Globals["tick"], elapsed);
		}

        public void Remove()
        {
            _script.Call(_script.Globals["remove"]);
        }
    }
}
