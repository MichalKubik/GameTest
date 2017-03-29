using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.Entities.Components
{
    class CollisionBody:EntityComponent
    {
        public Size2 Size { get; set; }
        //public RectangleF BoundingRectangle => new RectangleF(Entity.Position, Size);
        public BoundingBox BoundingBox { get; set; }

        public CollisionBody (Size2 size)
        {
            Size = size;
        }
    }
}
