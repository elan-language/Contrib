class ConsoleUI
{
    const Colour bodyColour = Colour.Green;
    const Colour appleColour = Colour.Red;

    public static void Main()
    {
        var charMap = new CharMapLive();
        printLine(welcome);
        var k = readKey();
        bool newGame = true;
        while (newGame)
        {
            playGame(charMap);
            print("Do you want to play again (y/n)?");
            char ans;
            do
            {
                ans = readKey();
            } while (ans != 'y' && ans != 'n');
            if (ans == 'n') newGame = false;
        }
    }

    private static void playGame(CharMap charMap)
    {
        charMap.fillBackground();
        var currentDirection = Direction.Right;
        var game = new Game(charMap.width, charMap.height, currentDirection);
        bool gameOn = true;
        while (gameOn)
        {
            var head = game.head;
            charMap.putBlock(head.x, head.y, bodyColour);
            charMap.putBlock(head.x + 1, head.y, bodyColour);

            var apple = game.apple;
            charMap.putBlock(apple.x, apple.y, appleColour);
            charMap.putBlock(apple.x + 1, apple.y, appleColour);

            var priorTail = game.tail;
            pause(200);

            if (keyHasBeenPressed())
            {
                var k = readKey();
                currentDirection = (k == 'w') ? Direction.Up :
                                   (k == 's') ? Direction.Down :
                                   (k == 'a') ? Direction.Left :
                                   (k == 'd') ? Direction.Right :
                                   currentDirection; // if no recognised key pressed keep same direction
            }

            gameOn = game.clockTick(currentDirection);
            if (!game.tail.isSameSquareAs(priorTail))
            {
                charMap.clear(priorTail.x, priorTail.y);
                charMap.clear(priorTail.x + 1, priorTail.y); //Need to clear two blocks cover one square
            }
            //charMap.display(); //needed if using a CharacterMapBuffered
        }
        clearKeyBuffer();
        charMap.setCursor(0, 0);
        printLine($"Game Over! Score: {game.GetScore()}");
    }

    const string welcome = $@"Welcome to the Snake game. 

Use the w,a,s, and d keys to control the direction of the snake. Letting the snake get to any edge will lose you the game, as will letting the snake's head pass over its body. Eating an apple will
cause the snake to grow by 

If you want to re-size the window, please do so now, before starting the game.

Click on this window to get 'focus' (and see a flashing cursor). Then press any key to start..";
}

