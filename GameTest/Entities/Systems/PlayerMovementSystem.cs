using GameTest.Entities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.Entities.Systems
{
    class PlayerMovementSystem : ComponentSystem
    {
        private const float _speed = 500;
        private Vector2 _velocity;
        private CollisionBody _playerBody;
        private List<CollisionBody> _bodies = new List<CollisionBody>();

        private Boolean isCollidingFromRight = false;
        private Boolean isCollidingFromLeft = false;
        private Boolean isCollidingFromUp = false;
        private Boolean isCollidingFromDown = false;

        protected override void OnComponentAttached(EntityComponent component)
        {
            var body = component as CollisionBody;

            if (body != null)
            {
                if (body.Entity.Name == "Player")
                    _playerBody = body;
                else
                {
                    _bodies.Add(body);
                    body.BoundingBox = new BoundingBox(new Vector3(body.Entity.Position, 0), new Vector3(body.Entity.Position.X + body.Size.Width, body.Entity.Position.Y + body.Size.Height, 0));
                }
            }

            base.OnComponentAttached(component);
        }

        protected override void OnComponentDetached(EntityComponent component)
        {
            var body = component as CollisionBody;

            if (body != null)
            {
                if (_bodies.Contains(body))
                    _bodies.Remove(body);
                else
                    _playerBody = null;
            }

            base.OnComponentDetached(component);
        }

        public override void Update(GameTime gameTime)
        {
            if (_playerBody.Entity == null)
                return;

            /*  _velocity = new Vector2(0, 0);
              _playerBody.BoundingBox = new BoundingBox(new Vector3(_playerBody.Entity.Position - new Vector2(_playerBody.Size.Width / 2, _playerBody.Size.Height / 2), 0), new Vector3(_playerBody.Entity.Position.X + _playerBody.Size.Width / 2, _playerBody.Entity.Position.Y + _playerBody.Size.Height / 2, 0));

              resetCollisions();

              foreach (var body in _bodies)
              {
                  if (body.BoundingBox.Contains(_playerBody.BoundingBox) == ContainmentType.Intersects)
                  {
                      if (_playerBody.Entity.Position.X <= body.Position.X &&
                         (_playerBody.Entity.Position.Y + _playerBody.Size.Height/2 > body.Position.Y && _playerBody.Entity.Position.Y - _playerBody.Size.Height/2 < body.Position.Y + body.Size.Height)
                         )
                          isCollidingFromRight = true;
                      if (_playerBody.Entity.Position.X >= body.Position.X &&
                         (_playerBody.Entity.Position.Y + _playerBody.Size.Height / 2 > body.Position.Y && _playerBody.Entity.Position.Y < body.Position.Y + body.Size.Height)
                         )
                          isCollidingFromLeft = true;
                      if (_playerBody.Entity.Position.Y - _playerBody.Size.Height / 2 < body.Position.Y &&
                         (_playerBody.Entity.Position.X + _playerBody.Size.Width / 2 > body.Position.X && _playerBody.Entity.Position.X - _playerBody.Size.Width < body.Position.X + body.Size.Width) 
                         )
                          isCollidingFromDown = true;
                      if (_playerBody.Entity.Position.Y - _playerBody.Size.Height / 2 > body.Position.Y &&
                         (_playerBody.Entity.Position.X + _playerBody.Size.Width / 2 > body.Position.X && _playerBody.Entity.Position.X - _playerBody.Size.Width < body.Position.X + body.Size.Width) // is correct ATM
                         )
                          isCollidingFromUp = true;

                  }
              }



              var state = Keyboard.GetState();

              if (state.IsKeyDown(Keys.A) && !isCollidingFromLeft)
              {
                  _velocity.X = -(_speed);
              }
              if (state.IsKeyDown(Keys.D) && !isCollidingFromRight)
              {
                  _velocity.X = _speed;
              }
              if (state.IsKeyDown(Keys.S) && !isCollidingFromDown)
              {
                  _velocity.Y = _speed;
              }
              if (state.IsKeyDown(Keys.W) && !isCollidingFromUp)
              {
                  _velocity.Y = -(_speed);
              }



              _playerBody.Entity.Position += _velocity * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;*/

            // ATTEMPT 2
            /*
            _velocityX = 0;
            _velocityY = 0;
            collisionX = false;
            collisionY = false;
            var state = Keyboard.GetState();
            _playerBody.BoundingBox = new BoundingBox(new Vector3(_playerBody.Entity.Position - new Vector2(_playerBody.Size.Width / 2, _playerBody.Size.Height / 2), 0), new Vector3(_playerBody.Entity.Position.X + _playerBody.Size.Width / 2, _playerBody.Entity.Position.Y + _playerBody.Size.Height / 2, 0));

            if (state.IsKeyDown(Keys.A))
            {
                _velocityX = -(_speed);
            }
            if (state.IsKeyDown(Keys.D))
            {
                _velocityX = _speed;
            }

            _playerBody.Entity.Position += new Vector2(_velocityX, 0) * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            foreach (var body in _bodies)
            {
                if (body.BoundingBox.Contains(_playerBody.BoundingBox) == ContainmentType.Intersects)
                {
                    collisionX = true;
                    if (_playerBody.Entity.Position.X < body.Entity.Position.X)
                        _playerBody.Entity.Position = new Vector2(body.Entity.Position.X - _playerBody.Size.Width / 2 - 300, _playerBody.Position.Y);
                    else
                        _playerBody.Entity.Position = new Vector2(body.Entity.Position.X + body.Size.Width - _playerBody.Size.Width / 2, _playerBody.Position.Y);
                }
            }

            //_playerBody.Entity.Position += new Vector2(_velocityX, 0) * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            if (state.IsKeyDown(Keys.W))
            {
                _velocityY = -(_speed);
            }
            if (state.IsKeyDown(Keys.S))
            {
                _velocityY = _speed;
            }

            _playerBody.Entity.Position += new Vector2(0, _velocityY) * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            foreach (var body in _bodies)
            {
                if (body.BoundingBox.Contains(_playerBody.BoundingBox) == ContainmentType.Intersects)
                {
                    if (_playerBody.Entity.Position.Y < body.Entity.Position.Y)
                        _playerBody.Entity.Position = new Vector2(_playerBody.Position.X, body.Entity.Position.Y - _playerBody.Size.Height / 2);
                    else
                        _playerBody.Entity.Position = new Vector2(_playerBody.Position.X, body.Entity.Position.Y + body.Size.Height - _playerBody.Size.Height / 2);
                }
            }

            //_playerBody.Entity.Position += new Vector2(0, _velocityY) * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

    */
            _velocity = new Vector2(0, 0);
            var playerPosition = _playerBody.Entity.Position;
            var playerSize = _playerBody.Size;
            resetCollisions();

            var playerBoundingBoxUp = new BoundingBox(new Vector3(playerPosition.X - playerSize.Width / 2, playerPosition.Y - playerSize.Height / 2 - 5, 0),
                                                      new Vector3(playerPosition.X + playerSize.Width / 2, playerPosition.Y - playerSize.Height / 2 - 1, 0));
            var playerBoundingBoxDown = new BoundingBox(new Vector3(playerPosition.X - playerSize.Width / 2, playerPosition.Y + playerSize.Height / 2 + 1, 0),
                                                        new Vector3(playerPosition.X + playerSize.Width / 2, playerPosition.Y + playerSize.Height / 2 + 5, 0));
            var playerBoundingBoxLeft = new BoundingBox(new Vector3(playerPosition.X - playerSize.Width / 2 - 5, playerPosition.Y - playerSize.Height / 2, 0),
                                                        new Vector3(playerPosition.X - playerSize.Width / 2 - 1, playerPosition.Y + playerSize.Height / 2, 0));
            var playerBoundingBoxRight = new BoundingBox(new Vector3(playerPosition.X + playerSize.Width / 2 + 1, playerPosition.Y - playerSize.Height / 2, 0),
                                                         new Vector3(playerPosition.X + playerSize.Width / 2 + 5, playerPosition.Y + playerSize.Height / 2, 0));

            foreach ( var body in _bodies )
            {
                var bodyPosition = body.Entity.Position;
                var bodySize = body.Size;

                var bodyBoundingBoxUp = new BoundingBox(new Vector3(bodyPosition.X, bodyPosition.Y - 5, 0),
                                                        new Vector3(bodyPosition.X + bodySize.Width, bodyPosition.Y - 1, 0));
                var bodyBoundingBoxDown = new BoundingBox(new Vector3(bodyPosition.X, bodyPosition.Y + bodySize.Height + 1, 0),
                                                            new Vector3(bodyPosition.X + bodySize.Width , bodyPosition.Y + bodySize.Height + 5, 0));
                var bodyBoundingBoxLeft = new BoundingBox(new Vector3(bodyPosition.X - 5, bodyPosition.Y, 0),
                                                            new Vector3(bodyPosition.X - 1, bodyPosition.Y + bodySize.Height, 0));
                var bodyBoundingBoxRight = new BoundingBox(new Vector3(bodyPosition.X + bodySize.Width + 1, bodyPosition.Y, 0),
                                                             new Vector3(bodyPosition.X + bodySize.Width + 5, bodyPosition.Y + bodySize.Height, 0));

                if (bodyBoundingBoxUp.Contains(playerBoundingBoxDown) == ContainmentType.Intersects)
                    isCollidingFromDown = true;
                if (bodyBoundingBoxDown.Contains(playerBoundingBoxUp) == ContainmentType.Intersects)
                    isCollidingFromUp = true;
                if (bodyBoundingBoxLeft.Contains(playerBoundingBoxRight) == ContainmentType.Intersects)
                    isCollidingFromRight = true;
                if (bodyBoundingBoxRight.Contains(playerBoundingBoxLeft) == ContainmentType.Intersects)
                    isCollidingFromLeft = true;
            }

            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.A) && !isCollidingFromLeft)
            {
                _velocity.X = -(_speed);
            }
            if (state.IsKeyDown(Keys.D) && !isCollidingFromRight)
            {
                _velocity.X = _speed;
            }
            if (state.IsKeyDown(Keys.S) && !isCollidingFromDown)
            {
                _velocity.Y = _speed;
            }
            if (state.IsKeyDown(Keys.W) && !isCollidingFromUp)
            {
                _velocity.Y = -(_speed);
            }

            _playerBody.Entity.Position += _velocity * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

        }

        private void resetCollisions()
        {
            isCollidingFromRight = false;
            isCollidingFromLeft = false;
            isCollidingFromUp = false;
            isCollidingFromDown = false;
         }
    }
}




/* foreach (var bodyA in _movingBodies)
            {
                bodyA.Velocity += _gravity * deltaTime;
                bodyA.Entity.Position += bodyA.Velocity * deltaTime;

                foreach (var bodyB in _staticBodies.Concat(_movingBodies))
                {
                    if (bodyA == bodyB)
                        continue;

                    var depth = IntersectionDepth(bodyA.BoundingRectangle, bodyB.BoundingRectangle);

                    if (depth != Vector2.Zero)
                    {
                        var collisionHandlers = bodyA.Entity.GetComponents<BasicCollisionHandler>();

                        foreach (var collisionHandler in collisionHandlers)
                            collisionHandler.OnCollision(bodyA, bodyB, depth);
                    }
                }
            }*/
