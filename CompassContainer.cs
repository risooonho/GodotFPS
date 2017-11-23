using Godot;
using System;

public class CompassContainer : Container
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
    public override void _Ready() 
    {
        //TextureRect compass = (TextureRect)GetNode("Compass");
        //compass.RectPosition = new Vector2(-compass.RectSize.x/2, compass.RectPosition.y);
    }
    public override void  _Draw() 
    {
        VisualServer.CanvasItemSetClip(GetCanvasItem(),true);
    }
}
