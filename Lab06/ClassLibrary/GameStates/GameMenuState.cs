﻿
namespace ClassLibrary.GameStates
{
    public class GameMenuState : IGameState
    {
        public void Handle(GameContext context)
        {
            Console.WriteLine();
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Show High Scores");
            Console.WriteLine("3. Main menu");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    context.SetState(new PlayGameState(context.CurrentPlayer, context.PlayerManager));
                    break;
                case "2":
                    context.SetState(new HighScoreState());
                    break;
                case "3":
                    context.SetState(new MainMenuState());
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}
