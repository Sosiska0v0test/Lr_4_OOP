using System.Text;
using modified_Lr_4.Commands;
using modified_Lr_4.Repository;
using modified_Lr_4.Service;

namespace modified_Lr_4
{
    public abstract class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            DbContext dbContext = new DbContext();
            PlayerRepository playerRepository = new PlayerRepository(dbContext.Players);
            GameRepository gameRepository = new GameRepository(dbContext.Games);
            GameService gameService = new GameService(playerRepository, gameRepository);

            ICommand addPlayerCommand = new AddPlayerCommand(gameService);
            addPlayerCommand.Execute();

            ICommand createGameCommand = new CreateGameCommand(gameService);
            createGameCommand.Execute();

            ICommand playerStatisticsCommand = new PlayerStatisticsCommand(gameService);
            playerStatisticsCommand.Execute();

            ICommand displayPlayersCommand = new DisplayPlayersCommand(gameService);
            displayPlayersCommand.Execute();

            ICommand allGamesCommand = new AllGamesCommand(gameService);
            allGamesCommand.Execute();
        }
    }
}