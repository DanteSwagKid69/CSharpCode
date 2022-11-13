using Raylib_cs;
using System.Numerics;

//creating window
Raylib.InitWindow(800, 480, "Window");

Vector2 testV = new Vector2(200, 200);
MyRay ray1 = new MyRay(200, 200);
Boundary wall = new Boundary(600, 100, 600, 300);


while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.BLACK);
    bool pt = ray1.Cast(wall);

    Console.WriteLine(pt);
    ray1.Draw();
    wall.Draw();

    Raylib.EndDrawing();
}

Raylib.CloseWindow();

public class Boundary
{
    Vector2 startPoint;
    Vector2 endPoint;

    public Boundary(int _x1, int _y1, int _x2, int _y2)
    {
        startPoint = new Vector2(_x1, _y1);
        endPoint = new Vector2(_x2, _y2);
    }

    public Vector2 Point1()
    {
        return startPoint;
    }

    public Vector2 Point2()
    {
        return endPoint;
    }

    public void Draw()
    {
        Raylib.DrawLineV(startPoint, endPoint, Color.WHITE);
    }
}

public class MyRay
{
    Vector2 pos;
    Vector2 dir;
    Vector2 localPos;
    public MyRay(float _x, float _y)
    {
        pos = new Vector2(_x, _y);
        dir = new Vector2(this.pos.X + 1, this.pos.Y + 0);
    }

    public float x()
    {
        return pos.X;
    }

    public float y()
    {
        return pos.Y;
    }

    public void Draw()
    {
        Raylib.DrawLineV(pos, dir, Color.WHITE);
    }

    public bool Cast(Boundary wall)
    {
        float x1 = wall.Point1().X;
        float y1 = wall.Point1().Y;
        float x2 = wall.Point2().X;
        float y2 = wall.Point2().Y;

        float x3 = this.pos.X;
        float y3 = this.pos.Y;
        float x4 = (this.pos.X + this.dir.X);
        float y4 = (this.pos.Y + this.dir.Y);

        float denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
        if (denominator == 0)
        {
            return false;
        }

        float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / denominator;
        float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / denominator;

        if (t > 0 && t < 1 && u > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}