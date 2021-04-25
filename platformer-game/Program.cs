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

            //INITIATE VALUES
            int screenWidth = 1920;
            int screenHeight = 1000;
            Raylib.InitWindow(screenWidth, screenHeight, "The Legend of Bianca");
            Raylib.SetTargetFPS(60);

            //GAME VALUES
            string gameState = "game";
            int count = 0;
            int scl = 200;

            //PLATFORM LISTS
            Rectangle roadTile = new Rectangle(-465, -100, scl, scl);
            Rectangle grassTile = new Rectangle(-500, -400, scl, scl);
            Rectangle testTile = new Rectangle(-500, -400, scl, scl);

            //CAMERA VALUES
            Rectangle player = new Rectangle(340, 600, scl, scl);

            Camera2D camera = new Camera2D();
            camera.offset = new Vector2(screenWidth / 2, screenHeight / 2);
            camera.rotation = 0.0f;
            camera.zoom = 1.0f;

            //COLLISION CHECK
            // !Raylib.CheckCollisionRecs(grassTile, player)


            while (!Raylib.WindowShouldClose())
            {
                if (gameState == "game")
                {
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                    {
                        player.x += 5;
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                    {
                        player.x -= 5;
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
                    {
                        player.y += 5;
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                    {
                        player.y -= 5;
                    }//ändra lite på demhär asså

                    camera.target = new Vector2(player.x + player.width / 2, player.y + player.height / 2);

                    //level
                    testTile = new Rectangle(0, 0, scl, scl);
                    grassTile = new Rectangle(-500, -400, scl, scl);
                    roadTile = new Rectangle(-500, -400, scl, scl);

                    List<string> one = new List<string>()
                    {".",".",".","#",".",".",".",".","#",".",".",".",
                     ".",".",".","#",".",".",".",".","#",".",".",".",
                     ".",".",".","#",".",".",".",".","#",".",".",".",
                     ".",".",".","#",".",".",".",".","#",".",".",".",
                     "#","#","#","#","#","#","#","#","#","#","#","#",
                     ".",".",".",".",".",".",".",".","#",".",".",".",
                     ".",".",".",".",".",".",".",".","#",".",".",".",
                     ".",".",".",".",".",".",".",".","#",".",".",".",
                     ".",".",".",".",".",".",".",".",".",".",".",".",};

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.WHITE);
                    Raylib.BeginMode2D(camera);


                    //DRAW LEVEL

                    for (int i = 0; i < one.Count; i++)
                    {
                        if (count >= 12)//next row
                        {
                            grassTile.y += scl;
                            grassTile.x = -860;
                            roadTile.y += scl;
                            roadTile.x = -860;
                            count = 0;

                        }
                        if (one[i] == ".")
                        {
                            Raylib.DrawRectangleRec(grassTile, Color.DARKGREEN);
                            roadTile.x += scl;
                            grassTile.x += scl;
                            count++;
                            Raylib.DrawText("X: " + grassTile.x + " Y: " + grassTile.y, (int)grassTile.x, (int)grassTile.y, 20, Color.BLACK);

                        }
                        else if (one[i] == "#")
                        {
                            Raylib.DrawRectangleRec(roadTile, Color.DARKBROWN);
                            roadTile.x += scl;
                            grassTile.x += scl;
                            count++;
                            Raylib.DrawText("X: " + roadTile.x + " Y: " + roadTile.y, (int)roadTile.x, (int)roadTile.y, 20, Color.BLACK);
                        }
                    }


                    for (int i = 0; i < 12; i++)
                    {
                        Raylib.DrawLine(-660 + scl * i, -screenWidth, -660 + scl * i, screenWidth * 2, Color.BLACK);
                        Raylib.DrawLine(-screenWidth, 0 + scl * i, screenWidth * 2, 0 + scl * i, Color.BLACK);
                    }


                    //array [#, #, #, X, #,
                    //  #, X, X, X, #]

                    //Lista av Rectangles
                    //List.Add(RectangleRec(20, 50, 10, 10))
                    //*4
                    //for (int i = 0; i < List.Count; i++)
                    // {
                    //     if (RaylibCollision(player, List[i])) 
                    //     dont let the player walk etc.    
                    // }
                    // foreach (int item in one)
                    // {
                    //     if (item = 1)
                    //     {

                    //     }
                    //     Raylib.DrawRectangleRec(testTile, Color.PINK);
                    //     testTile.x += scl;
                    // }


                    //DRAW RESTEN
                    Raylib.DrawText("X: " + player.x + " Y: " + player.y, 100, 100, 32, Color.BLACK);

                    Raylib.DrawRectangleRec(player, Color.RED);

                    // Raylib.DrawRectangleRec(roadTile, Color.BLUE);
                    // Raylib.DrawRectangleRec(grassTile, Color.GREEN);

                    Raylib.DrawLine((int)camera.target.X, -screenHeight * 10, (int)camera.target.X, screenHeight * 10, Color.GREEN);
                    Raylib.DrawLine(-screenWidth * 10, (int)camera.target.Y, screenWidth * 10, (int)camera.target.Y, Color.GREEN);
                    Raylib.EndMode2D();
                    Raylib.EndDrawing();
                }
            }
        }



    }
}



