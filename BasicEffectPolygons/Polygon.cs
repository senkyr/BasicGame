using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicEffectPolygons
{
    class Polygon
    {
        public int N { get; private set; }
        public float Polomer { get; private set; }

        private VertexPositionColor[] Vertexy { get; set; }
        private BasicEffect Efekt { get; set; }

        private Vector2 Pozice { get; set; }
        private float Rotace { get; set; }
        private Color Barva { get; set; }

        public Polygon(int n, float polomer, Vector2 pozice, float rotace, Color barva, GraphicsDevice zobrazovaciZarizeni)
        {
            if (n < 3 || polomer <= 0)
                throw new Exception("Neplatný n-úhelník.");

            N = n;
            Polomer = polomer;
            Pozice = pozice;
            Rotace = rotace;
            Barva = barva;

            Efekt = new BasicEffect(zobrazovaciZarizeni);
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
                int soucasny = i / 3;
                int predchozi = (i / 3 - 1 + N) % N;

                vertexy[i + 0] = new VertexPositionColor(new Vector3(0, 0, 0), Color.White);
                vertexy[i + 1] = new VertexPositionColor(new Vector3(vrcholy[soucasny].X, vrcholy[soucasny].Y, 0), Barva);
                vertexy[i + 2] = new VertexPositionColor(new Vector3(vrcholy[predchozi].X, vrcholy[predchozi].Y, 0), Barva);
            }

            return vertexy;
        }

        public void Update(GameTime casOdMinulehoUpdatu)
        {
            Rotace += 5 * (float)(Math.PI / 180 * casOdMinulehoUpdatu.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(Matrix kamera, Matrix projekce, GraphicsDevice zobrazovaciZarizeni)
        {
            Efekt.World = Matrix.CreateRotationZ(Rotace) * Matrix.CreateTranslation(Pozice.X, Pozice.Y, 0);
            Efekt.View = kamera;
            Efekt.Projection = projekce;
            Efekt.VertexColorEnabled = true;

            foreach (EffectPass pruchod in Efekt.CurrentTechnique.Passes)
            {
                pruchod.Apply();

                zobrazovaciZarizeni.DrawUserPrimitives(PrimitiveType.TriangleList, Vertexy, 0, N);
            }
        }
    }
}
