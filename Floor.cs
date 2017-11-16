using Godot;
using System;

public class Floor : MeshInstance
{
    public override void _Ready()
    {
        this.CreateConvexCollision();
    }
}
