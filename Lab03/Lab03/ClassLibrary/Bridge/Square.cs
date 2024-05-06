﻿
namespace ClassLibrary.Bridge
{
    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer)
        {
        }

        public override void Draw()
        {
            renderer.Render("Square");
        }
    }
}
