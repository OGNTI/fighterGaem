using Raylib_cs;
using System.Numerics;


public class Projectile
{
    public Rectangle rect;
    Texture2D projectileUp;
    Texture2D ProjectileRight;
    Texture2D projectileUpRight;
    Texture2D projectileUpLeft;

    Vector2 direction;
    int speed = 17;

    public Projectile(Rectangle projectileRect, Vector2 dir)
    {
        projectileUp = Raylib.LoadTexture("img/tracerUpDown.png");
        ProjectileRight = Raylib.LoadTexture("img/tracerRightLeft.png");
        projectileUpRight = Raylib.LoadTexture("img/tracerRightDownLeft.png");
        projectileUpLeft = Raylib.LoadTexture("img/tracerLeftDownRight.png");
        rect = new Rectangle(projectileRect.x, projectileRect.y, projectileUp.width, projectileUp.height);
        direction = dir;
    }

    public void Update()
    {
        // move in direction with speed
        rect.x += direction.X * speed;
        rect.y += direction.Y * speed;
    }

    public void Draw()
    {
        // draw dependent on projectile direction
        if (direction.Y < 0 && direction.X < 0)
        {
            Raylib.DrawTexture(projectileUpLeft, (int)rect.x, (int)rect.y, Color.WHITE);
        }
        else if (direction.Y < 0 && direction.X > 0)
        {
            Raylib.DrawTexture(projectileUpRight, (int)rect.x, (int)rect.y, Color.WHITE);
        }
        else if (direction.Y > 0 && direction.X < 0)
        {
            Raylib.DrawTexture(projectileUpRight, (int)rect.x, (int)rect.y, Color.WHITE);
        }
        else if (direction.Y > 0 && direction.X > 0)
        {
            Raylib.DrawTexture(projectileUpLeft, (int)rect.x, (int)rect.y, Color.WHITE);
        }
        else if (direction.Y < 0 && direction.X == 0)
        {
            Raylib.DrawTexture(projectileUp, (int)rect.x, (int)rect.y, Color.WHITE);
        }
        else if (direction.Y > 0 && direction.X == 0)
        {
            Raylib.DrawTexture(projectileUp, (int)rect.x, (int)rect.y, Color.WHITE);
        }
        else if (direction.Y == 0 && direction.X < 0)
        {
            Raylib.DrawTexture(ProjectileRight, (int)rect.x, (int)rect.y, Color.WHITE);
        }
        else if (direction.Y == 0 && direction.X > 0)
        {
            Raylib.DrawTexture(ProjectileRight, (int)rect.x, (int)rect.y, Color.WHITE);
        }
    }
}
