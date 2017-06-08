using System;
namespace LandsOfFheyrn.Engine.Objects
{
    public class LOFTrait
    {
        public string Name { get; }
        public string Value { get; set; }

        public LOFTrait(string name, string val)
        {
            Name = name;
            Value = val;
        }
    }
}
