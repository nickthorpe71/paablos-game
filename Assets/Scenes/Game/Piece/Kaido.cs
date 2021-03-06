using System.Collections.Generic;
using UnityEngine;

public class Kaido : Piece
{
    void Start()
    {
        moveDistance = 5;
        movePattern = MovementPattern();
    }

    private List<Vector2> MovementPattern()
    {
        return new List<Vector2>
            {
                new Vector2(0, 1),
                new Vector2(0, -1)
            };
    }
}