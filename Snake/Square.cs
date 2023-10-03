public class Square
{
    public int x { get; init; }
    public int y { get; init; }

    public Square(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Square getAdjacentSquare(Direction d)
    {
        int newX = x;
        int newY = y;
        if (d == Direction.Left) newX -= 2;     // Horizontal adjacent is +/- 2 because a square is 2 display chars wide
        else if (d == Direction.Right) newX += 2;
        else if (d == Direction.Up) newY -= 1;
        else if (d == Direction.Down) newY += 1;
        return new Square(newX, newY);
    }

    public override string ToString() => $"{x},{y}";

    public  bool isSameSquareAs(Square sq)
    {
        return x == sq.x && y == sq.y;
    }
}