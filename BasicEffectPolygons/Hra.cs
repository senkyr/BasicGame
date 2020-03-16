using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicEffectPolygons
{
    public class Hra : Game
    {
        GraphicsDeviceManager SpravceZobrazovacihoZarizeni;
        GraphicsDevice ZobrazovaciZarizeni => GraphicsDevice;

        Rectangle OknoHry => new Rectangle(0, 0, 800, 600);
        Point StredOkna => OknoHry.Center;

        Rectangle LevyHorniKvadrant => new Rectangle(0, StredOkna.Y, StredOkna.X, StredOkna.Y);
        Rectangle PravyHorniKvadrant => new Rectangle(StredOkna.X, StredOkna.Y, StredOkna.X, StredOkna.Y);
        Rectangle LevyDolniKvadrant => new Rectangle(0, 0, StredOkna.X, StredOkna.Y);
        Rectangle PravyDolniKvadrant => new Rectangle(StredOkna.X, 0, StredOkna.X, StredOkna.Y);

        Matrix Kamera;
        Matrix Projekce;

        Polygon[] Polygony;

        public Hra()
        {
            SpravceZobrazovacihoZarizeni = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            SpravceZobrazovacihoZarizeni.PreferredBackBufferWidth = OknoHry.Width;
            SpravceZobrazovacihoZarizeni.PreferredBackBufferHeight = OknoHry.Height;
            SpravceZobrazovacihoZarizeni.IsFullScreen = false;
            SpravceZobrazovacihoZarizeni.ApplyChanges();

            RasterizerState nastaveniRasterizeru = new RasterizerState();
            nastaveniRasterizeru.CullMode = CullMode.None;
            ZobrazovaciZarizeni.RasterizerState = nastaveniRasterizeru;

            Vector3 UmisteniKamery = new Vector3(StredOkna.X, StredOkna.Y, 1);
            Vector3 NamireniKamery = new Vector3(StredOkna.X, StredOkna.Y, 0);
            Vector3 NatoceniKamery = Vector3.UnitY;

            Kamera = Matrix.CreateLookAt(UmisteniKamery, NamireniKamery, NatoceniKamery);
            Projekce = Matrix.CreateOrthographic(OknoHry.Width, OknoHry.Height, 0.1f, 1.0f);

            Polygony = new Polygon[] {
                new Polygon(3, 100, LevyHorniKvadrant.Center.ToVector2(), 0, Color.LightBlue, ZobrazovaciZarizeni),
                new Polygon(4, 100, PravyHorniKvadrant.Center.ToVector2(), 0, Color.LightPink, ZobrazovaciZarizeni),
                new Polygon(5, 100, LevyDolniKvadrant.Center.ToVector2(), 0, Color.GreenYellow, ZobrazovaciZarizeni),
                new Polygon(6, 100, PravyDolniKvadrant.Center.ToVector2(), 0, Color.LightGray, ZobrazovaciZarizeni),
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Polygon polygon in Polygony)
                polygon.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            ZobrazovaciZarizeni.Clear(Color.White);

            foreach (Polygon polygon in Polygony)
                polygon.Draw(Kamera, Projekce, ZobrazovaciZarizeni);

            base.Draw(gameTime);
        }
    }
}
