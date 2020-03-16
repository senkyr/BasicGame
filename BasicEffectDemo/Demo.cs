using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicEffectDemo
{
    public class Demo : Game
    {
        GraphicsDeviceManager SpravceZobrazovacihoZarizeni { get; set; }
        GraphicsDevice ZobrazovaciZarizeni => GraphicsDevice;

        Point RozliseniOkna => new Point(800, 600);

        Matrix Kamera { get; set; }
        Matrix Projekce { get; set; }
        Polygon[] Polygony { get; set; }

        Rectangle OknoHry => new Rectangle(new Point(0,0), RozliseniOkna);
        Point StredOkna => OknoHry.Center;

        Point VelikostKvadrantu => new Point(OknoHry.Width / 2, OknoHry.Height / 2);
        Rectangle LevyHorniKvadrant => new Rectangle(new Point(OknoHry.X, StredOkna.Y), VelikostKvadrantu);
        Rectangle PravyHorniKvadrant => new Rectangle(new Point(StredOkna.X, StredOkna.Y), VelikostKvadrantu);
        Rectangle LevyDolniKvadrant => new Rectangle(new Point(OknoHry.X, OknoHry.Y), VelikostKvadrantu);
        Rectangle PravyDolniKvadrant => new Rectangle(new Point(StredOkna.X, OknoHry.Y), VelikostKvadrantu);

        public Demo()
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
