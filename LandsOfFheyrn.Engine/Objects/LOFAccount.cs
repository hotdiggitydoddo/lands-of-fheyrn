using System;
using System.Collections.Generic;
using System.Linq;

namespace LandsOfFheyrn.Engine.Objects
{
    public class LOFAccount
    {
        public int Id { get; }
        public int UserId { get; }
        public List<LOFEntity> Characters {get; private set;}
        public LOFEntity CurrentCharacter {get; private set;}

        public LOFAccount(int id, int userId)
        {
            Id = id;
            UserId = userId;
            Characters = new List<LOFEntity>();
        }
    }
}
