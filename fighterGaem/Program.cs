using Raylib_cs;
using System.Numerics;


Rectangle screen = new Rectangle(0, 0, 1000, 1000);
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
Texture2D projectileRightUp = Raylib.LoadTexture("img/tracerRightDownLeft.png");
Texture2D projectileLeftUp = Raylib.LoadTexture("img/tracerLeftDownRight.png");
Texture2D projectileSide = Raylib.LoadTexture("img/tracerRightLeft.png");
Rectangle projectileRect = new Rectangle(planeRect.x, planeRect.y, projectileUp.width, projectileUp.height);

int speed = 5;
string currentScene = "start";
Vector2 dir = Vector2.UnitX;

int timer = 0;
int enemyTimer = 2 * 60;

List<Projectile> projectiles = new List<Projectile>();
List<Enemy> enemies = new List<Enemy>();

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
        timer++;

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
        Vector2 dir0 = Vector2.Normalize(dir);
        backRect.y -= dir0.Y * speed;
        backRect.x -= dir0.X * speed;

        // attack
        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) || Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            projectiles.Add(new Projectile(projectileRect, dir0));
        }
        foreach (Projectile p in projectiles)
        {
            p.Update();
        }
        projectiles.RemoveAll(p => p.rect.x > screen.width || p.rect.x + p.rect.width < 0 || p.rect.y > screen.height || p.rect.y + p.rect.height < 0);

        // spawn and handle enemy
        if (timer == enemyTimer)
        {
            enemies.Add(new Enemy((int)screen.width, (int)screen.height, dir0));
            enemyTimer += 3 * 60;
        }
        foreach (Enemy e in enemies)
        {
            e.Update(planeRect, (int)screen.width, (int)screen.height);
        }
        enemies.RemoveAll(e => e.state == 1);


    }
    else if (currentScene == "end")
    {

    }

    // Keep background image on screen
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
        // draw dependent on plane direction
        if (dir.Y == -1 && dir.X == -1)
        {
            Raylib.DrawTexture(planeUpLeft, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y < 0 && dir.X > 0)
        {
            Raylib.DrawTexture(planeUpRight, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y > 0 && dir.X < 0)
        {
            Raylib.DrawTexture(planeDownLeft, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y > 0 && dir.X > 0)
        {
            Raylib.DrawTexture(planeDownRight, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y < 0 && dir.X == 0)
        {
            Raylib.DrawTexture(plane, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y > 0 && dir.X == 0)
        {
            Raylib.DrawTexture(planeDown, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y == 0 && dir.X < 0)
        {
            Raylib.DrawTexture(planeLeft, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }
        else if (dir.Y == 0 && dir.X > 0)
        {
            Raylib.DrawTexture(planeRight, (int)planeRect.x, (int)planeRect.y, Color.WHITE);
        }

        // draw projectiles and enemies
        foreach (Projectile p in projectiles)
        {
            p.Draw();
        }
        foreach (Enemy e in enemies)
        {
            e.Draw((int)screen.width, (int)screen.height);
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