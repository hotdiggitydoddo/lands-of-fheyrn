using System;
namespace LandsOfFheyrn.Engine.Objects
{
    public class LOFEntity
    {
        public int Id { get; }
        public string Name { get; }

        public LOFEntity(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
