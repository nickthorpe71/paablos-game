using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class Gargoyle : Piece
    {
        public Gargoyle(bool isGold) : base(isGold)
        {
            moveDistance = 1;
            movePattern = MovementPattern();
        }

        private List<Vector2> MovementPattern()
        {
            return new List<Vector2>
            {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, -1),
                new Vector2(0, -1),
                new Vector2(-1, -1),
                new Vector2(-1, 1)
            };
        }
    }
}