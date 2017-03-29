using GameTest.Entities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.Entities
{
    class EntityFactory
    {
        private EntityComponentSystem _entityComponentSystem;
        private TextureAtlas _tilesetAtlas;

        public EntityFactory(EntityComponentSystem entityComponentSystem)
        {
            _entityComponentSystem = entityComponentSystem;

            var texture = Game1.Instance.Content.Load<Texture2D>("free-tileset");
            _tilesetAtlas = TextureAtlas.Create("tiny-characters-atlas", texture, 128, 128, 100, 2, 2);

        }

        public Entity createPlayer(Vector2 position,Size2 size)
        {
            Entity entity = _entityComponentSystem.CreateEntity("Player", position);

            entity.AttachComponent(new TransformableComponent<Sprite>(new Sprite(Game1.Instance.Content.Load<Texture2D>("magus_small"))));
            entity.AttachComponent(new CollisionBody(size));
    
            return entity;
        }

        public Entity createCollisionArea(Vector2 position, Size2 size)
        {
            Entity entity = _entityComponentSystem.CreateEntity(position);

            entity.AttachComponent(new CollisionBody(size));

            return entity;
        }

        public Entity createScaledSprite(Vector2 position, Size2 size, int gID)
        {
            Entity entity = _entityComponentSystem.CreateEntity(position);

            Texture2D entityTexture = _tilesetAtlas.Texture;
            Sprite entitySprite = new Sprite(entityTexture);
            entitySprite.TextureRegion = _tilesetAtlas[gID];

            entity.AttachComponent(new TransformableComponent<Sprite>(entitySprite));

            entity.Position = new Vector2(entity.Position.X + size.Width / 2, entity.Position.Y - size.Height / 2);
            entity.Scale = new Vector2(size.Width/128.0f, size.Height/128.0f);

            return entity;
        }


    }
}
