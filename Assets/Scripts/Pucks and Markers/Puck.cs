using System;
using UnityEngine;

[Serializable]
public struct Puck
{
    public int puckID;
    public Vector2 position;
    public float rotation;

    public Puck(int puckID, Vector2 position, float rotation)
    {
        this.puckID = puckID;
        this.position = position;
        this.rotation = rotation;
    }
}