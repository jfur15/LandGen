using Microsoft.Win32.SafeHandles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Nations
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 

    public static class Ran
    {
        public static Random dom = new Random();
        public static List<Color> colors = new List<Color>() { Color.Red, Color.Orange, Color.Yellow, Color.LimeGreen, Color.Blue, Color.Violet, Color.Brown, Color.Gray };

    }
    public class Nation
    {
        public Color color = new Color(200, Ran.dom.Next(0, 255), Ran.dom.Next(0, 255));

        public Point center;

        public List<City> cities = new List<City>();
        public City capital;
        public int id;

        public Nation(int x, int y, int id)
        {
            this.id = id;
            Point point = new Point(x, y);
            center = point;
        }
    }
    public class City
    {
        public Color color = new Color(200,Ran.dom.Next(0,255),Ran.dom.Next(0,255));
        public List<Point> points = new List<Point>();
        public Point center;

        public City(int x, int y)
        {
            Point point = new Point(x, y);
            center = point;
            points.Add(point);
        }

    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int mapWidth = 256;
        int mapHeight = 256;

        int waterLevel = 130;

        Texture2D cell;
        RenderTarget2D renderTarget;
        int[,] mapCells;
        int[,] nationCells;
        List<Nation> nations = new List<Nation>();

        SpriteFont spriteFont;

        int mapBorder = 10;
        int pGrid = 2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        bool isInside(int circle_x, int circle_y,
                              int rad, int x, int y)
        {
            // Compare radius of circle with 
            // distance of its center from 
            // given point 
            if ((x - circle_x) * (x - circle_x) +
                (y - circle_y) * (y - circle_y) <= rad * rad)
                return true;
            else
                return false;
        }
        protected override void Initialize()
        {
            base.Initialize();

            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000; 
            graphics.ApplyChanges();


           
            //Initialize land arrays
            mapCells = new int[mapWidth, mapHeight];
            nationCells = new int[mapWidth, mapHeight];
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    mapCells[x, y] = 0;
                    nationCells[x, y] = 0;
                }
            }

            //Assign heights, minus border
            Perlin.PerlinNoise perlinNoise = new Perlin.PerlinNoise(800, pGrid);
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    // Generate noise value.
                    var noiseValue = perlinNoise.Noise2D(i, j);

                    // Transform from [-1..+1] to [0..1].
                    var transformedNoiseValue = (noiseValue + 1) / 2;

                    // Transform to greyscale.
                    var grey = (int)Math.Floor(255 * transformedNoiseValue);

                    mapCells[i, j] = grey;

                }
            }


            //Take a circle in middle of map, set all heights outside it to 0
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    if (!isInside(mapWidth/2, mapHeight/2, mapWidth/2-mapBorder, i, j))
                    {

                        mapCells[i, j] = 0;
                    }

                }
            }
            


            renderTarget = new RenderTarget2D(
                 GraphicsDevice,
                 mapWidth,
                 mapHeight,
                 false,
                 GraphicsDevice.PresentationParameters.BackBufferFormat,
                 DepthFormat.Depth24);


            //Reset nations list
            nations = new List<Nation>();

            //Create new nations
            for (int i = 0; i < Ran.dom.Next(3,7); i++)
            {
                NewNation();
            }

            //Grow nations
            List<(List<Point>, Nation)> tps = new List<(List<Point>, Nation)>();
            foreach (Nation nation1 in nations)
            {
                tps.Add( (SurroundPoints(nation1.center), nation1) );
            }

            bool allNationsEmpty = false;
            while (!allNationsEmpty)
            {

                allNationsEmpty = true;
                foreach (var item in tps)
                {
                    if (item.Item1.Count > 0)
                    {

                        int idx = Ran.dom.Next(item.Item1.Count);
                        Point pointGet = item.Item1[idx];
                        if (!PointTaken(pointGet.X, pointGet.Y))
                        {
                            nationCells[pointGet.X, pointGet.Y] = item.Item2.id;
                            item.Item1.AddRange(SurroundPoints(pointGet));
                        }
                        item.Item1.RemoveAt(idx);
                    }
                    if (item.Item1.Count != 0)
                    {
                        allNationsEmpty = false;
                    }
                }
            }

            //Add cities
            foreach (Nation nation in nations)
            {
                List<Point> myLand = new List<Point>();

                for (int x = 0; x < mapWidth; x++)
                {
                    for (int y = 0; y < mapHeight; y++)
                    {
                        if (nationCells[x,y] == nation.id)
                        {
                            myLand.Add(new Point(x, y));
                        }
                    }
                }

                Point c = myLand[Ran.dom.Next(myLand.Count)];
                City t = new City(c.X, c.Y);
                int cSize = Ran.dom.Next(0,4);
                for (int x = -cSize; x <= cSize; x++)
                {
                    for (int y = -cSize; y <= cSize; y++)
                    {
                        Point tryPoint = new Point(c.X + x, c.Y + y);
                        if (myLand.Contains(tryPoint) && !t.points.Contains(tryPoint))
                        {
                            t.points.Add(tryPoint);
                        }
                    }
                }
                nation.cities.Add(t);

            }
            

            DrawScene(renderTarget);
        }

        bool AboveWater(int x, int y)
        {
            return mapCells[x, y] > waterLevel;
        }

        //Return all points that are above water and with no nation
        public List<Point> SurroundPoints(Point p)
        {
            List<Point> ppoint = new List<Point>(); 
            List<Point> rpoint = new List<Point>();
            ppoint.Add(new Point(p.X - 1, p.Y));
            ppoint.Add(new Point(p.X + 1, p.Y));
            ppoint.Add(new Point(p.X, p.Y - 1));
            ppoint.Add(new Point(p.X, p.Y + 1));

            foreach (Point pointy in ppoint)
            {
                if (PointWithinBounds(pointy.X, pointy.Y))
                {
                    if (AboveWater(pointy.X, pointy.Y) && !PointTaken(pointy.X, pointy.Y))
                    {
                        rpoint.Add(pointy);
                    }
                }
            }
            return rpoint;
        }
        bool PointWithinBounds(int x, int y)
        {
            if (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight)
            {
                return true;
            }
            return false;
        }
        bool PointTaken(int x, int y)
        {
            if (nationCells[x,y] == 0)
            {
                return false;
            }
            return true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            cell = Content.Load<Texture2D>("cell");
            spriteFont = Content.Load<SpriteFont>("DefaultFont");
        }

        protected override void UnloadContent()
        {
        }

        void NewNation()
        {
            bool possible = false;
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (AboveWater(x,y))
                    {
                        possible = true;
                    }
                }
            }
            if (possible == false)
            {
                return;
            }
            Point center;
            do
            {
                center = new Point(Ran.dom.Next(0, mapWidth), Ran.dom.Next(0, mapHeight));
            } while (!AboveWater(center.X, center.Y));
            Nation c = new Nation(center.X, center.Y, nations.Count+1);
            c.color = Ran.colors[nations.Count];
            nationCells[center.X, center.Y] = c.id;
            nations.Add(c);
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (waterLevel < 255)
                {
                    waterLevel++;
                    DrawScene(renderTarget);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (waterLevel >0)
                {
                    waterLevel--;
                    DrawScene(renderTarget);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                int newPgrid = pGrid;
                do
                {
                    newPgrid--;
                } while (mapWidth % newPgrid != 0);

                if (newPgrid > 0)
                {
                    pGrid = newPgrid;
                    Initialize();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                int newPgrid = pGrid;
                do
                {
                    newPgrid++;
                } while (mapWidth % newPgrid != 0);

                pGrid = newPgrid;
                Initialize();
                
            }
            base.Update(gameTime);
        }

        protected void DrawScene(RenderTarget2D renderTarget)
        {
            // Set the render target
            GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

            // Draw the scene
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (mapCells[x, y] <= waterLevel)
                    {

                        spriteBatch.Draw(cell, new Vector2(x,y), Color.CornflowerBlue);
                    }
                    else
                    {
                        spriteBatch.Draw(cell, new Vector2(x, y), new Color(0, mapCells[x,y], 0));
                    }

                    if (nationCells[x,y] != 0)
                    {
                        spriteBatch.Draw(cell, new Vector2(x, y), nations[nationCells[x, y]-1].color);
                    }
                }
            }
            foreach (var item in nations.Aggregate(new List<City>(), (x,y) => x.Concat(y.cities).ToList()))
            {
                foreach (var p in item.points)
                {
                    spriteBatch.Draw(cell, p.ToVector2(), item.color);
                }
            }
            

            spriteBatch.End();

            // Drop the render target
            GraphicsDevice.SetRenderTarget(null);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(default,default,SamplerState.PointClamp,default,default,default,Matrix.CreateScale(3f)); 
            spriteBatch.Draw(renderTarget, new Vector2(0), Color.White);
            spriteBatch.DrawString(spriteFont, "Waterlevel: " + waterLevel + "\nGrid: " + pGrid, new Vector2(5), Color.LawnGreen);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
