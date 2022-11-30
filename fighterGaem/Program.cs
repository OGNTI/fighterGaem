using Raylib_cs;
using System.Numerics;

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

Texture2D projectileUp = Raylib.LoadTexture("img/tracerUpDown.png");
Rectangle projectileRect = new Rectangle(planeRect.x, planeRect.y, projectileUp.width, projectileUp.height);
Texture2D projectileRightUp = Raylib.LoadTexture("img/tracerRightDownLeft.png");
Texture2D projectileLeftUp = Raylib.LoadTexture("img/tracerLeftDownRight.png");
Texture2D projectileSide = Raylib.LoadTexture("img/tracerRightLeft.png");
int showProjectile = -1;
int projectileSpeed = 10;
Vector2 dirProjectile = Vector2.UnitX;

int speed = 5;
string currentScene = "start";
Vector2 dir = Vector2.UnitX;


while (!Raylib.WindowShouldClose())
{
    if (currentScene == "start")
    {
        backRect.y += speed;
        dir.Y = -1;
        dir.X = 0;

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            currentScene = "game";
        }
    }
    else if (currentScene == "game")
    {
        // Movement
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            dir.Y = -1;
            dir.X = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            dir.Y = -1;
            dir.X = +1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            dir.Y = +1;
            dir.X = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            dir.Y = +1;
            dir.X = +1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            dir.Y = -1;
            dir.X = 0;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            dir.Y = +1;
            dir.X = 0;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            dir.Y = 0;
            dir.X = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            dir.Y = 0;
            dir.X = +1;
        }
        backRect.y -= dir.Y * speed;
        backRect.x -= dir.X * speed;

        // attack
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            showProjectile *= -1;
            dirProjectile.X = dir.X;
            dirProjectile.Y = dir.Y;
        }
        if(showProjectile == 1)
        {
            projectileRect.x += dirProjectile.X * projectileSpeed;
            projectileRect.y += dirProjectile.Y * projectileSpeed;
        }
        else if (showProjectile == -1)
        {
            projectileRect.x = planeRect.x;
            projectileRect.y = planeRect.y;
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

        if (dir.Y == -1 && dir.X == -1) // ----------------------------------------------------- Diagonal Plaen
        {
            Raylib.DrawTexture(planeUpLeft, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y == -1 && dir.X == 1)
        {
            Raylib.DrawTexture(planeUpRight, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y == 1 && dir.X == -1)
        {
            Raylib.DrawTexture(planeDownLeft, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y == 1 && dir.X == 1)
        {
            Raylib.DrawTexture(planeDownRight, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y == -1 && dir.X == 0) // ------------------------------------------------- vertical / horizontal
        {
            Raylib.DrawTexture(plane, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y == 1 && dir.X == 0)
        {
            Raylib.DrawTexture(planeDown, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y == 0 && dir.X == -1)
        {
            Raylib.DrawTexture(planeLeft, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y == 0 && dir.X == 1)
        {
            Raylib.DrawTexture(planeRight, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }

        // attack grafik
        if (showProjectile == 1)
        {
            if(dirProjectile.Y != 0 && dirProjectile.X == 0)
            {
                Raylib.DrawTexture(projectileUp, (int)projectileRect.x, (int)projectileRect.y, Color.WHITE);
            }
            else if(dirProjectile.Y == 0 && dirProjectile.X != 0)
            {
                Raylib.DrawTexture(projectileSide, (int)projectileRect.x, (int)projectileRect.y, Color.WHITE);
            }
            else if(dirProjectile.Y == 1 && dirProjectile.X == 1 || dirProjectile.Y == -1 && dirProjectile.X == -1)
            {
                Raylib.DrawTexture(projectileLeftUp, (int)projectileRect.x, (int)projectileRect.y, Color.WHITE);
            }
            else if(dirProjectile.Y == 1 && dirProjectile.X == -1 || dirProjectile.Y == -1 && dirProjectile.X == 1)
            {
                Raylib.DrawTexture(projectileRightUp, (int)projectileRect.x, (int)projectileRect.y, Color.WHITE);
            }
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