using Raylib_cs;
using System.Numerics;


public class Enemy
{
    public Rectangle rect;
    Texture2D missileUp;
    Texture2D missileDown = Raylib.LoadTexture("img/missileDown.png");
    Texture2D missileLeft = Raylib.LoadTexture("img/missileLeft.png");
    Texture2D missileRight = Raylib.LoadTexture("img/missileRight.png");
    Texture2D missileUpLeft = Raylib.LoadTexture("img/missileUpLeft.png");
    Texture2D missileUpRight = Raylib.LoadTexture("img/missileUpRight.png");
    Texture2D missileDownLeft = Raylib.LoadTexture("img/missileDownLeft.png");
    Texture2D missileDownRight = Raylib.LoadTexture("img/missileDownRight.png");

    Random generator = new Random();
    public Vector2 direction;
    public int speed = 2;
    public int state = 0;
    string xDirection;
    string yDirection;

    public Enemy(int screenWidth, int screenHeight, Vector2 dir)
    {
        int startX = 0;
        int startY = 0;
        direction = dir;

        missileUp = Raylib.LoadTexture("img/missileUp.png");

        int startSide = generator.Next(1, 5);
        if (startSide == 1)
        {
            startX = generator.Next(screenWidth);
            startY -= missileUp.height / 2;
        }
        else if (startSide == 2)
        {
            startX -= missileUp.width / 2;
            startY = generator.Next(screenHeight);
        }
        else if (startSide == 3)
        {
            startX = generator.Next(screenWidth);
            startY = screenHeight - missileUp.height / 2;
        }
        else if (startSide == 4)
        {
            startX = screenWidth - missileUp.width / 2;
            startY = generator.Next(screenHeight);
        }

        rect = new Rectangle(startX, startY, missileUp.width, missileUp.height);
    }

    public int Update(Rectangle planeRect, int screenWidth, int screenHeight)
    {
        if (rect.x > screenWidth / 2 - rect.width / 2)
        {
            rect.x -= speed;
        }
        else if (rect.x < screenWidth / 2 - rect.width / 2)
        {
            rect.x += speed;
        }
        if (rect.y > screenHeight / 2 - rect.height / 2)
        {
            rect.y -= speed;
        }
        else if (rect.y < screenHeight / 2 - rect.height / 2)
        {
            rect.y += speed;
        }

        if (rect.x + rect.width / 2 > planeRect.x && rect.x + rect.width / 2 < planeRect.x + planeRect.width && rect.y + rect.height / 2 > planeRect.y && rect.y + rect.height / 2 < planeRect.y + planeRect.height)
        {
            state = 1;
        }

        return state;
    }

    public void Draw(int screenWidth, int screenHeight)
    {

        if (rect.x > screenWidth / 2 - rect.width / 2 + 20)
        {
            xDirection = "left";
        }
        else if (rect.x < screenWidth / 2 - rect.width / 2 - 20)
        {
            xDirection = "right";
        }
        if (rect.y > screenHeight / 2 - rect.height / 2 + 20)
        {
            yDirection = "up";
        }
        else if (rect.y < screenHeight / 2 - rect.height / 2 - 20)
        {
            yDirection = "down";
        }


        if (xDirection == "left" && yDirection == "up")
        {
            Raylib.DrawTexture(missileUpLeft, (int)rect.x, (int)rect.y, Color.WHITE);
            xDirection = "";
            yDirection = "";
        }
        else if (xDirection == "left" && yDirection == "down")
        {
            Raylib.DrawTexture(missileDownLeft, (int)rect.x, (int)rect.y, Color.WHITE);
            xDirection = "";
            yDirection = "";
        }
        else if (xDirection == "right" && yDirection == "up")
        {
            Raylib.DrawTexture(missileUpRight, (int)rect.x, (int)rect.y, Color.WHITE);
            xDirection = "";
            yDirection = "";
        }
        else if (xDirection == "right" && yDirection == "down")
        {
            Raylib.DrawTexture(missileDownRight, (int)rect.x, (int)rect.y, Color.WHITE);
            xDirection = "";
            yDirection = "";
        }
        else if (xDirection == "left" && yDirection == "")
        {
            Raylib.DrawTexture(missileLeft, (int)rect.x, (int)rect.y, Color.WHITE);
            xDirection = "";
            yDirection = "";
        }
        else if (xDirection == "right" && yDirection == "")
        {
            Raylib.DrawTexture(missileRight, (int)rect.x, (int)rect.y, Color.WHITE);
            xDirection = "";
            yDirection = "";
        }
        else if (xDirection == "" && yDirection == "up")
        {
            Raylib.DrawTexture(missileUp, (int)rect.x, (int)rect.y, Color.WHITE);
            xDirection = "";
            yDirection = "";
        }
        else if (xDirection == "" && yDirection == "down")
        {
            Raylib.DrawTexture(missileDown, (int)rect.x, (int)rect.y, Color.WHITE);
        }
    }



}
