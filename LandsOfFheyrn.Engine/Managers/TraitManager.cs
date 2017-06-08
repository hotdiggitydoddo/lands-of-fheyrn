using System;
using System.Collections.Generic;

namespace LandsOfFheyrn.Engine.Managers
{
    public class TraitManager
    {
        readonly Dictionary<int, Dictionary<string, string>> _traits;

        public TraitManager()
        {
            _traits = new Dictionary<int, Dictionary<string, string>>();
           
        }

        public void Set(int entity, string name, string val)
        {
            
            if (!_traits.ContainsKey(entity))
                _traits.Add(entity, new Dictionary<string, string>());

            if (_traits[entity].ContainsKey(name))
                _traits[entity][name] = val;
            else _traits[entity].Add(name, val);
        }

        public string Get(int entity, string name)
        {
            return _traits[entity]?[name];
        }

        public void Delete(int entity, string name)
        {
            if (_traits.ContainsKey(entity) && _traits[entity].ContainsKey(name))
                _traits[entity].Remove(name);
        }
    }
}
