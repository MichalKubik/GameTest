using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameTest
{
    class Sprite
    {
        public Vector2 Position { get; set; }
        private Texture2D texture;
        public BoundingBox BoundingBox;
        public BoundingSphere BoundingSphere;
        public bool isVisible { get; set; }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            this.isVisible = true;
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
            this.isVisible = true;
        }

        public void setPosition(float X, float Y)
        {
            this.Position = new Vector2(X, Y);
        }

        public void updateBoundingBox()
        {
            BoundingBox = new BoundingBox(new Vector3(Position, 0), new Vector3(Position.X + texture.Width, Position.Y + texture.Height, 0));
        }

        public void updateBoundingSphere()
        {
            float radius;
            if (texture.Height > texture.Width)
                radius = texture.Height;
            else
                radius = texture.Width;

            BoundingSphere = new BoundingSphere(new Vector3(Position.X + texture.Width / 2, Position.Y + texture.Height / 2, 0), radius);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
                spriteBatch.Draw(texture, Position, Color.White);

            updateBoundingBox();
            updateBoundingSphere();
        }

    }
}
