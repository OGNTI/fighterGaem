using Raylib_cs;

public class Projectile
{
    public Rectangle rect;
    Texture2D image;

    public Projectile(Rectangle projectileRect)
    {
        image = Raylib.LoadTexture("img/tracerUpDown.png");
        rect = new Rectangle(projectileRect.x, projectileRect.y, image.width, image.height);

    }

    public void Draw()
    {
        Raylib.DrawTexture(image, (int)rect.x, (int)rect.y, Color.WHITE);
    }

}
