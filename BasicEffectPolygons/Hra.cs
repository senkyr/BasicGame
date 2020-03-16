using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicEffectPolygons
{
    public class Hra : Game
    {
        GraphicsDeviceManager SpravceZobrazovacihoZarizeni;
        GraphicsDevice ZobrazovaciZarizeni => GraphicsDevice;

        Rectangle OknoHry = new Rectangle(0, 0, 800, 600);
        Point StredOkna => OknoHry.Center;

        Matrix Kamera;
        Matrix Projekce;

        Polygon Trojuhelnik;

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

            Trojuhelnik = new Polygon(3, 100, StredOkna.ToVector2(), 0, Color.Gray, ZobrazovaciZarizeni);

            base.Initialize();
        }

        protected override void LoadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Trojuhelnik.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            ZobrazovaciZarizeni.Clear(Color.White);

            Trojuhelnik.Draw(Kamera, Projekce, ZobrazovaciZarizeni);

            base.Draw(gameTime);
        }
    }
}
