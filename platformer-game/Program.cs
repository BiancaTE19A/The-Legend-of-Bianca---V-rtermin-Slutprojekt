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
            //kommentera på engelska, beskriv en kod snutt så att man vet vad den gör
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
            bool moving = false;
            bool movingGrass = false;
            Random r = new Random();


            //CAMERA VALUES
            Rectangle player = new Rectangle(340, 830, scl / 2, scl / 2);

            Camera2D camera = new Camera2D();
            camera.offset = new Vector2(screenWidth / 2, screenHeight / 2);
            camera.rotation = 0.0f;
            camera.zoom = 1.0f;

            List<string> tileMap = new List<string>()
            {".",".",".","#",".",".",".",".","#",".",".",".",
             ".",".",".","#",".",".",".",".","#",".",".",".",
             ".",".",".","#",".",".",".",".","#",".",".",".",
             ".",".",".","#",".",".",".",".","#",".",".",".",
             ".",".",".","#",".",".",".",".","#",".",".",".",
             "#","#","#","#","#","#","#","#","#","#","#","#",
             ".",".",".",".",".",".",".",".","#",".",".",".",
             ".",".",".",".",".",".",".",".","#",".",".",".",
             ".",".",".",".",".",".",".",".","#",".",".",".",
             ".",".",".",".",".",".",".",".","#",".",".",".",};

            int cols = 12;
            int rows = 10;

            while (!Raylib.WindowShouldClose())
            {
                if (gameState == "game")
                {
                    //values 
                    velocity = 5;
                    playerTile = 0;


                    //grass collision
                    List<Rectangle> grassTiles = new List<Rectangle>();


                    for (int i = 0; i < tileMap.Count; i++)
                    {
                        if (tileMap[i] == ".")
                        {
                            int x = i % cols;
                            int y = i / cols;
                            grassTiles.Add(new Rectangle(x * scl, y * scl, scl, scl));
                        }
                    }
                    for (int i = 0; i < grassTiles.Count; i++)
                    {
                        if (Raylib.CheckCollisionRecs(player, grassTiles[i]) && moving == true)
                        {
                            velocity = 3;
                            playerTile = i;
                            r = generator.Next();
                        }
                    }



                    //controls
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyDown(KeyboardKey.KEY_D))
                    {
                        player.x += velocity;
                        moving = true;
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) || Raylib.IsKeyDown(KeyboardKey.KEY_A))
                    {
                        player.x -= velocity;
                        moving = true;
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) || Raylib.IsKeyDown(KeyboardKey.KEY_S))
                    {
                        player.y += velocity;
                        moving = true;
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_UP) || Raylib.IsKeyDown(KeyboardKey.KEY_W))
                    {
                        player.y -= velocity;
                        moving = true;
                    }//ändra lite på demhär asså

                    //border control
                    int returnValueX = clamp((int)player.x, 0, (int)cols * (int)scl - (int)player.width);
                    player.x = returnValueX;
                    int returnValueY = clamp((int)player.y, 0, (int)rows * (int)scl - (int)player.width);
                    player.y = returnValueY;

                    camera.target = new Vector2(player.x + player.width / 2, player.y + player.height / 2);

                    Raylib.BeginDrawing();
                    Raylib.DrawText("X: " + player.x + " Y: " + player.y, 100, 100, 32, Color.BLACK);
                    Raylib.DrawText("" + playerTile, 100, 10, 20, Color.BLACK);
                    Raylib.ClearBackground(Color.WHITE);
                    Raylib.BeginMode2D(camera);

                    //initial draw level
                    // for (int i = 0; i < tileMap.Count; i++)
                    // {
                    //     if (count >= 12)//next row
                    //     {
                    //         grassTile.y += scl;
                    //         grassTile.x = -860;
                    //         roadTile.y += scl;
                    //         roadTile.x = -860;
                    //         count = 0;

                    //     }
                    //     if (tileMap[i] == ".")
                    //     {
                    //         Raylib.DrawRectangleRec(grassTile, Color.DARKGREEN);
                    //         roadTile.x += scl;
                    //         grassTile.x += scl;
                    //         count++;
                    //         Raylib.DrawText("X: " + grassTile.x + " Y: " + grassTile.y, (int)grassTile.x, (int)grassTile.y, 20, Color.BLACK);

                    //     }
                    //     else if (tileMap[i] == "#")
                    //     {
                    //         Raylib.DrawRectangleRec(roadTile, Color.DARKBROWN);
                    //         roadTile.x += scl;
                    //         grassTile.x += scl;
                    //         count++;
                    //         Raylib.DrawText("X: " + roadTile.x + " Y: " + roadTile.y, (int)roadTile.x, (int)roadTile.y, 20, Color.BLACK);
                    //     }
                    // }

                    //draw level
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if (tileMap[j + i * cols] == ".")
                            {
                                Raylib.DrawRectangle(j * scl, i * scl, scl, scl, Color.DARKGREEN);
                            }
                            else
                            {
                                Raylib.DrawRectangle(j * scl, i * scl, scl, scl, Color.DARKBROWN);
                            }
                        }
                    }

                    //Hjälp linjer
                    for (int i = 0; i < 12; i++)
                    {
                        Raylib.DrawLine(scl * i, 0, scl * i, rows * scl, Color.BLACK);
                        Raylib.DrawLine(0, scl * i, cols * scl, scl * i, Color.BLACK);
                    }

                    //DRAW RESTEN
                    Raylib.DrawRectangleRec(player, Color.RED);

                    Raylib.DrawLine((int)camera.target.X, -screenHeight * 10, (int)camera.target.X, screenHeight * 10, Color.GREEN);
                    Raylib.DrawLine(-screenWidth * 10, (int)camera.target.Y, screenWidth * 10, (int)camera.target.Y, Color.GREEN);
                    Raylib.EndMode2D();
                    Raylib.EndDrawing();
                }
            }
        }
        static int clamp(int value, int min, int max)
        {
            if (value < min)
            {
                value = min;
            }
            if (value > max)
            {
                value = max;
            }
            return value;
        }
    }
}



