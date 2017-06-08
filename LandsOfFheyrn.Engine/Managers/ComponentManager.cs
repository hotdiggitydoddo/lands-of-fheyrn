using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using LandsOfFheyrn.Engine.Actions;
using LandsOfFheyrn.Engine.Objects;
using MoonSharp.Interpreter;

namespace LandsOfFheyrn.Engine.Managers
{
    public class ComponentManager
    {
        List<LOFComponent> HandleFunc(int arg1, List<Objects.LOFComponent> arg2)
        {

        }

        static readonly ComponentManager _instance = new ComponentManager();
		public static ComponentManager Instance => _instance;

		Dictionary<string, string> _componentList;
        ConcurrentDictionary<int, List<LOFComponent>> _entityComponents;

		public ComponentManager()
		{
			_componentList = new Dictionary<string, string>();
            _entityComponents = new ConcurrentDictionary<int, List<LOFComponent>>();
		}

		public void RefreshAllComponents()
		{
			_componentList.Clear();
			_componentList = ScriptManager.Instance.GetComponentScripts();
		}

		public void AssignComponent(int entity, string componentName, params string[] defaults)
		{
			var script = new Script();
			script.Globals["LOFComponent"] = typeof(LOFComponent);
			script.Globals["LOFTrait"] = typeof(LOFTrait);
			script.Globals["LOFEntity"] = typeof(LOFEntity);
			script.DoString(_componentList[componentName]);

            var cmp = (LOFComponent)script.Call(script.Globals["init"], entity, script, defaults).UserData.Object;

            _entityComponents.AddOrUpdate(entity, new List<LOFComponent>(new[] { cmp }), (e, list) => { list.Add(cmp); return list; });
		}

        public bool HasComponent(int entity, string name)
        {
            return _entityComponents.ContainsKey(entity) && _entityComponents[entity].Exists(x => x.Name == name);
        }

        public bool DoAction(int entity, LOFAction action)
        {
            if (!_entityComponents.ContainsKey(entity)) return true;

            foreach (var c in _entityComponents[entity])
			{
				if (c.IsActive && !c.DoAction(action))
					return false;
			}
			return true;
        }

        public void RemoveComponent(int entity, string componentName)
        {
            var cmp = _entityComponents[entity].Find(x => x.Name == componentName);
            if (cmp == null) return;
            cmp.Remove();
            _entityComponents[entity].Remove(cmp);
        }
    }
}
