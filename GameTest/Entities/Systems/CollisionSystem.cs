using GameTest.Entities.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities.Components;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.Entities.Systems
{
    class CollisionSystem : ComponentSystem
    {
        private List<CollisionBody> _bodies = new List<CollisionBody>();


        protected override void OnComponentAttached(EntityComponent component)
        {
            var body = component as CollisionBody;

            if (body != null)
                _bodies.Add(body);

            base.OnComponentAttached(component);
        }

        protected override void OnComponentDetached(EntityComponent component)
        {
            var body = component as CollisionBody;

            if (body != null)
                _bodies.Remove(body);

            base.OnComponentDetached(component);
        }

        public override void Update(GameTime gameTime)
        {
            var deltaTime = gameTime.GetElapsedSeconds();

            foreach(var bodyA in _bodies)
            {
               // bodyA.Entity
            }
        }


    }
}
