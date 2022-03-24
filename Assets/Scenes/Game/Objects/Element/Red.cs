using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class Red : Element
    {
        public Red()
        {
            destroysOccupant = true;
            hasKnockback = false;
            environment = new FireEnv();
            spellPattern = new List<Vector2>
            {
                new Vector2(0, 1),
                new Vector2(0, -1)
            };
        }
    }
}