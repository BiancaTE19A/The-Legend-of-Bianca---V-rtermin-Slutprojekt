using System;
using System.Numerics;
using Raylib_cs;
using System.Collections.Generic;


namespace platformer_game
{
    class Program
    {
        static void Main(string[] args)
        {
            //beskriv en kod snutt så att man vet vad den gör
            //utvärderqa varför du valt en specifik grej, tex varför jag valt en while loop här ist för en for osv
            //skapa fiender i gräset - här kan metoder, klasser, random generator osv användas
            //kolla checklista för november projekt
            //sätt in lite fler saker i metoder
            //gräs fiender - när inGrass == true och spelar GÅR --> random generator för fiende, ksk ha ett visst antal fiender
            //när man stöter på en fiende --> byt gamestate 


            //INITIATE VALUES
            int screenWidth = 1920;
            int screenHeight = 1000;
            Raylib.InitWindow(screenWidth, screenHeight, "The Legend of Bianca");
            Raylib.SetTargetFPS(60);

            //GAME VALUES
            string gameState = "game";
            int scl = 200;
            int velocity = 5;
            int rows = 10;
            int cols = 12;

            //CAMERA AND PLAYER VALUES
            Rectangle player = new Rectangle(340, 830, scl / 2, scl / 2);
            Camera2D camera = CreateCamera(player);

            //TILE VALUES
            List<string> tileMap = new List<string>(GetMap());
            List<Rectangle> grassTiles = new List<Rectangle>(CreateGrassTiles(tileMap, cols, scl));

            while (!Raylib.WindowShouldClose())
            {
                if (gameState == "game")
                {
                    velocity = 5;

                    //GRASS COLLISION
                    velocity = GetGrassCollision(grassTiles, player, velocity);

                    //MOVEMENT CONTROLS
                    (float posX, float posY, int vel) returnMovement = GetMovement(player.x, player.y, velocity);
                    player.x = returnMovement.posX;
                    player.y = returnMovement.posY;
                    velocity = returnMovement.vel;

                    //BORDER CONTROL
                    player.x = GetBorderControl((int)player.x, 0, (int)cols * (int)scl - (int)player.width);
                    player.y = GetBorderControl((int)player.y, 0, (int)rows * (int)scl - (int)player.width);

                    //CAMERA 
                    camera.target = new Vector2(player.x + player.width / 2, player.y + player.height / 2);

                    //BEGIN DRAW
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.WHITE);
                    Raylib.BeginMode2D(camera);

                    //DRAW LEVEL
                    DrawLevel(tileMap, rows, cols, scl);

                    //DRAW OUTLINES
                    DrawOutlines(rows, cols, scl);

                    //DRAW PLAYER
                    DrawPlayer(player, camera, screenWidth, screenHeight);

                    Raylib.EndMode2D();
                    Raylib.EndDrawing();
                }
            }
        }
        static Camera2D CreateCamera(Rectangle player)
        {
            //create camera and assign values 
            Camera2D cam = new Camera2D();
            cam.offset = new Vector2(1920 / 2, 1000 / 2);
            cam.target = new Vector2(player.x + player.width / 2, player.y + player.height / 2);
            cam.rotation = 0.0f;
            cam.zoom = 1.0f;
            return cam;
        }
        static List<string> GetMap()
        {
            //create map blueprint and add to list
            List<string> map = new List<string>
             {
             ".",".",".","#",".",".",".",".","#",".",".",".",
             ".",".",".","#",".",".",".",".","#",".",".",".",
             ".",".",".","#",".",".",".",".","#",".",".",".",
             ".",".",".","#",".",".",".",".","#",".",".",".",
             ".",".",".","#",".",".",".",".","#",".",".",".",
             "#","#","#","#","#","#","#","#","#","#","#","#",
             ".",".",".",".",".",".",".",".","#",".",".",".",
             ".",".",".",".",".",".",".",".","#",".",".",".",
             ".",".",".",".",".",".",".",".","#",".",".",".",
             ".",".",".",".",".",".",".",".","#",".",".",".",};
            return map;
        }
        static List<Rectangle> CreateGrassTiles(List<string> map, int cols, int scl)
        {
            List<Rectangle> grass = new List<Rectangle>();
            for (int i = 0; i < map.Count; i++)
            {
                //go through map list and locate grass, then set x and y values for each rectangle
                //then add rectangle to list
                if (map[i] == ".")
                {
                    int x = i % cols;
                    int y = i / cols;
                    grass.Add(new Rectangle(x * scl, y * scl, scl, scl));
                }
            }
            return grass;
        }
        static int GetGrassCollision(List<Rectangle> grass, Rectangle player, int vel)
        {
            for (int i = 0; i < grass.Coxunt; i++)
            {
                //when player collides with grass, make player move slower
                if (Raylib.CheckCollisionRecs(player, grass[i]))
                {
                    vel = 3;
                }
            }
            return vel;
        }
        static (float, float, int) GetMovement(float posX, float posY, int vel)
        {
            //move in x-axis
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                posX += vel;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) || Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                posX -= vel;
            }
            //move in y-axis
            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) || Raylib.IsKeyDown(KeyboardKey.KEY_S))
            {
                posY += vel;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP) || Raylib.IsKeyDown(KeyboardKey.KEY_W))
            {
                posY -= vel;
            }
            return (posX, posY, vel);
        }
        static int GetBorderControl(int pos, int min, int max)
        {
            //prevent player from moving outside border
            if (pos < min)
            {
                pos = min;
            }
            if (pos > max)
            {
                pos = max;
            }
            return pos;
        }

        static void DrawLevel(List<string> map, int rows, int cols, int scl)
        {
            //go through each row then each column on that row, to determen wether it's a grass- or dirt-string
            //then draw out tiles
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (map[i * cols + j] == ".")
                    {
                        Raylib.DrawRectangle(j * scl, i * scl, scl, scl, Color.DARKGREEN);
                    }
                    else
                    {
                        Raylib.DrawRectangle(j * scl, i * scl, scl, scl, Color.DARKBROWN);
                    }
                }
            }
        }
        static void DrawOutlines(int rows, int cols, int scl)
        {
            //draw the outlines of the tiles 
            for (int i = 0; i < cols; i++)
            {
                Raylib.DrawLine(scl * i, 0, scl * i, rows * scl, Color.BLACK);
                Raylib.DrawLine(0, scl * i, cols * scl, scl * i, Color.BLACK);
            }
        }
        static void DrawPlayer(Rectangle player, Camera2D camera, int screenWidth, int screenHeight)
        {
            //draw player and helping lines along x-axis and y-axis
            Raylib.DrawRectangleRec(player, Color.RED);
            Raylib.DrawLine((int)camera.target.X, -screenHeight * 10, (int)camera.target.X, screenHeight * 10, Color.GREEN);
            Raylib.DrawLine(-screenWidth * 10, (int)camera.target.Y, screenWidth * 10, (int)camera.target.Y, Color.GREEN);
        }
    }
}



