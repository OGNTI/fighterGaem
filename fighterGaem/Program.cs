using Raylib_cs;

// Initial Idea:
// landscape as background, moves according to plaen direction
// still plaen with different textures depending on direction
// plaen shoot tracing rounds
// enemies = missiles, fly sporadic
// teleport to other side of map when reached end
// texture for being hit/crash, explosion on missile death
// dropping bombs on bases/airfields? some amount of bases, randomly scattered, outside ceratin distance of each other

// wanted features:
// not being able to do 180 degree turn, do two 90 degree (four 45 degree), which happens when holding s/d then pressing w/a
// having the direction you're going take priorty over the opposite, basically if w is pressed while s is held then w take priorty but s was held before, because of if statements
// if all keys released, plaen keeps going the last direction it was going


Rectangle screen = new Rectangle(0, 0, 1000, 800);
Raylib.InitWindow((int)screen.width, (int)screen.height, "Gaem");
Raylib.SetTargetFPS(60);


Texture2D backG = Raylib.LoadTexture("img/landscape.png");
Texture2D plane = Raylib.LoadTexture("img/FighterUp.png");
Texture2D planeLeft = Raylib.LoadTexture("img/FighterLeft.png");
Texture2D planeDown = Raylib.LoadTexture("img/FighterDown.png");
Texture2D planeRight = Raylib.LoadTexture("img/FighterRight.png");
Texture2D planeUpRight = Raylib.LoadTexture("img/FighterUpRight.png");
Texture2D planeUpLeft = Raylib.LoadTexture("img/FighterUpLeft.png");
Texture2D planeDownRight = Raylib.LoadTexture("img/FighterDownRight.png");
Texture2D planeDownLeft = Raylib.LoadTexture("img/FighterDownLeft.png");
Rectangle backRect = new Rectangle(screen.width / 2 - backG.width / 2, screen.height / 2 - backG.height / 2, backG.width, backG.height);
Rectangle planeRect = new Rectangle(screen.width / 2 - plane.width / 2, screen.height / 2 - plane.height / 2, plane.width, plane.height);



int speed = 5;
string currentScene = "start";

while (!Raylib.WindowShouldClose())
{
    if (currentScene == "start")
    {
        backRect.y += speed;

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            currentScene = "game";
        }
    }
    else if (currentScene == "game")
    {
        // Movement
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            backRect.y += speed;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            backRect.y -= speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            backRect.x += speed;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            backRect.x -= speed;
        }

    }
    else if (currentScene == "end")
    {

    }

    // Border
        if (backRect.x > 0)
        {
            backRect.x = screen.width - backG.width;
        }
        else if (backRect.x - screen.width < -backG.width)
        {
            backRect.x = 0;
        }
        if (backRect.y > 0)
        {
            backRect.y = screen.height - backG.height;
        }
        else if (backRect.y - screen.height < -backG.height)
        {
            backRect.y = 0;
        }



    Raylib.BeginDrawing();  // ------------------------------------------------------------------------------------------------------------
    Raylib.ClearBackground(Color.WHITE);
    Raylib.DrawTexture(backG, (int)backRect.x, (int)backRect.y, Color.WHITE);

    if (currentScene == "start")
    {
        Raylib.DrawTexture(plane, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        Raylib.DrawText("Press Enter to start gaem.", (int)screen.width / 5, (int)screen.height / 4 * 3, 40, Color.BLACK);
    }
    else if (currentScene == "game")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && Raylib.IsKeyDown(KeyboardKey.KEY_A)) // --------------------- Diagonal
        {
            Raylib.DrawTexture(planeUpLeft, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            Raylib.DrawTexture(planeUpRight, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            Raylib.DrawTexture(planeDownLeft, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            Raylib.DrawTexture(planeDownRight, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) // ------------------------------------------------------- vertical / horizontal
        {
            Raylib.DrawTexture(plane, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            Raylib.DrawTexture(planeDown, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            Raylib.DrawTexture(planeLeft, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            Raylib.DrawTexture(planeRight, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }

    }
    else if (currentScene == "end")
    {

    }
    else
    {
        Raylib.DrawText("Erorr", 0, 0, 200, Color.BLACK);
    }



    Raylib.EndDrawing();    // ------------------------------------------------------------------------------------------------------------
}