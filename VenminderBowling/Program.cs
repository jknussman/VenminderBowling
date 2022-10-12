// See https://aka.ms/new-console-template for more information
using VenminderBowling.Domain;

Console.WriteLine("Enter Values for scores when prompted or enter a command");
Console.WriteLine("Commands: score - see current score for game");
Console.WriteLine("help - see list of commands");
Console.WriteLine("reset - start over with a new game");
Console.WriteLine("exit - close the app");

var game = new Game();
var exit = false;
Console.WriteLine("New Game Started");
while (!exit)
{
    if(game.GameComplete == false)
    {
        Console.WriteLine($"What is your next roll for Frame {game.CurrentFrame}?");
    }
    else
    {
        Console.WriteLine("Game Complete:");
        Console.WriteLine(game.ToString());
        Console.WriteLine("");
        Console.WriteLine("New Game Started");
        game = new Game();
    }

    var command = Console.ReadLine();

    switch(command)
    {
        case "help":
            Console.WriteLine("Commands: score - see current score for game");
            Console.WriteLine("help - see list of commands");
            Console.WriteLine("reset - start over with a new game");
            Console.WriteLine("exit - close the app");
            break;
        case "score":
            Console.WriteLine(game.ToString());
            break;
        case "reset":
            game = new Game();
            Console.WriteLine("New Game Started");
            break;
        case "exit":
            exit = true;
            break;
        default:
           if( int.TryParse(command, out int score))
            {
                try
                {
                    game.addRoll(score);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"An Invalid command was entered, please try again. {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("An Invalid command was entered, please try again.");
            }
            break;
    }
}