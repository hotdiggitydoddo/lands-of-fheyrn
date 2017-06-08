using System;
using System.Collections.Generic;
using System.Linq;

namespace LandsOfFheyrn.Engine.Objects
{
    public class LOFEntity
    {
        List<LOFTrait> _traits;
        
        public int Id { get; }
        public string Name { get; }

        //for serialization
        public List<LOFTrait> AllTraits => new List<LOFTrait>(_traits);        

        public LOFEntity(int id, string name)
        {
            Id = id;
            Name = name;
            _traits = new List<LOFTrait>();
        }

        public bool HasTrait(string name)
        {
            return _traits.Exists(x => x.Name == name);
        }

        public LOFTrait SetTrait(string name, string val)
        {
            var trait = new LOFTrait(name, val);
            if(!_traits.Exists(x => x.Name == name))
                _traits.Add(trait);
            else
                _traits.Single(x => x.Name == name).Value = val;
            return trait;
        }

        public LOFTrait GetTrait(string name)
        {
            return _traits.SingleOrDefault(x => x.Name == name);
        }

        public void RemoveTrait(string name)
        {
            var trait = _traits.SingleOrDefault(x => x.Name == name);
            if (trait == null) return;
            _traits.Remove(trait);
        }
    }
}
