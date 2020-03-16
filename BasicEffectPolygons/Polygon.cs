using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BasicEffectPolygons
{
    class Polygon
    {
        public int N { get; private set; }
        public float Polomer { get; private set; }

        private VertexPositionColor[] Vertexy { get; set; }
        private BasicEffect Transformator { get; set; }

        private Vector2 Umisteni { get; set; }
        private float Otoceni { get; set; }
        private Color Barva { get; set; }

        public Polygon(int n, float polomer, Vector2 umisteni, float otoceni, Color barva, GraphicsDevice zobrazovaciZarizeni)
        {
            if (n < 3 || polomer <= 0)
                throw new Exception("Neplatný n-úhelník.");

            N = n;
            Polomer = polomer;

            Umisteni = umisteni;
            Otoceni = otoceni;
            Barva = barva;

            Transformator = new BasicEffect(zobrazovaciZarizeni);
            Vertexy = PripravitVertexy();
        }

        private VertexPositionColor[] PripravitVertexy()
        {
            float uhlovyPosun = (float)(2 * Math.PI / N);

            Vector2[] vrcholy = new Vector2[N];

            for (int i = 0; i < vrcholy.Length; i++)
                vrcholy[i] = Vector2.Transform(new Vector2(0, Polomer), Matrix.CreateRotationZ(uhlovyPosun * i));

            VertexPositionColor[] vertexy = new VertexPositionColor[N * 3];

            for(int i = 0; i < vertexy.Length; i += 3)
            {
                int tentoVrchol = i / 3;
                int predchoziVrchol = (i / 3 - 1 + N) % N;

                vertexy[i + 0] = new VertexPositionColor(new Vector3(0, 0, 0), Color.White);
                vertexy[i + 1] = new VertexPositionColor(new Vector3(vrcholy[tentoVrchol].X, vrcholy[tentoVrchol].Y, 0), Barva);
                vertexy[i + 2] = new VertexPositionColor(new Vector3(vrcholy[predchoziVrchol].X, vrcholy[predchoziVrchol].Y, 0), Barva);
            }

            return vertexy;
        }
        
        public void Update(GameTime casOdMinulehoUpdatu)
        {
            Otoceni += 90 / N * (float)(Math.PI / 180 * casOdMinulehoUpdatu.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(Matrix kamera, Matrix projekce, GraphicsDevice zobrazovaciZarizeni)
        {
            Transformator.World = Matrix.CreateRotationZ(Otoceni) * Matrix.CreateTranslation(Umisteni.X, Umisteni.Y, 0);
            Transformator.View = kamera;
            Transformator.Projection = projekce;
            Transformator.VertexColorEnabled = true;

            foreach (EffectPass pruchod in Transformator.CurrentTechnique.Passes)
            {
                pruchod.Apply();

                zobrazovaciZarizeni.DrawUserPrimitives(PrimitiveType.TriangleList, Vertexy, 0, N);
            }
        }
    }
}
