public class Snake
{
    private Queue<Square> body = new Queue<Square>();
    public Square head { get; private set; }
    public Square tail()  =>  body.Peek();

    public Snake(int x, int y, Direction startingDirection)
    {
        var tail = new Square(x, y);
        body.Enqueue(tail);
        head = tail.getAdjacentSquare(startingDirection);
    }

    public bool hasHitItself() => bodyCovers(head);

    public bool bodyCovers(Square sq) => body.Any(seg => seg.isSameSquareAs(sq));

    public void advanceHeadOneSquare(Direction d)
    {
        body.Enqueue(head);
        head = head.getAdjacentSquare(d);
    }

    public void advanceTailOneSquare()
    {
        body.Dequeue();
    }

    public int Length() => body.Count;
}
