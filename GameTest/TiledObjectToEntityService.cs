using GameTest.Entities;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.Components
{
    class TiledObjectToEntityService
    {
        private EntityFactory _entityFactory;
        private Dictionary<string, Func<TiledMapObject, Entity >> createEntity;

        public TiledObjectToEntityService(EntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
            createEntity = new Dictionary<string, Func<TiledMapObject, Entity>>
            {
                {"Player", tiledObject => _entityFactory.createPlayer(tiledObject.Position,tiledObject.Size)},
                {"CollisionArea", tiledObject => _entityFactory.createCollisionArea(tiledObject.Position,tiledObject.Size) },
                {"ScaledSprite", tiledObject => _entityFactory.createScaledSprite(tiledObject.Position, tiledObject.Size, Int32.Parse(tiledObject.Name))}
            };
        }

        public void createEntities(TiledMapObject[] tiledObjects)
        {
            foreach (TiledMapObject tiledObject in tiledObjects)
                createEntity[tiledObject.Type](tiledObject);
        }

    }
}
