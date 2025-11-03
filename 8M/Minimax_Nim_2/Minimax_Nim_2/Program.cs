using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniMax_Nim_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var initialPiles = new List<int> { 2, 2 };
            bool botStarts = true;

            var game = new NimGame(initialPiles, botStarts);
            GameState state;

            do
            {
                state = game.PlayTurn();
            } while (state == GameState.Ongoing);


            if (state == GameState.BotWon)
                Console.WriteLine("Vyhrál počítač!");
            else
                Console.WriteLine("Gratulujeme! Vyhráli jste!");
        }
    }

    public enum GameState
    {
        Ongoing,
        BotWon,
        HumanWon
    }

    public class NimGameState
    {
        public List<int> Piles { get; private set; }
        public int MatchesInGame { get; private set; }

        public NimGameState(IEnumerable<int> initialPiles)
        {
            Piles = new List<int>(initialPiles);
            MatchesInGame = Piles.Sum();
        }

        public void MakeMove(int pileIndex, byte matchesToRemove)
        {
            if (IsValidMove(pileIndex, matchesToRemove))
            {
                Piles[pileIndex] -= matchesToRemove;
                MatchesInGame -= matchesToRemove;
            }
            else
            {
                throw new ArgumentException("Neplatný tah!");
            }
        }

        private bool IsValidMove(int pileIndex, byte matchesToRemove)
        {
            return pileIndex >= 0 &&
                   pileIndex < Piles.Count &&
                   Piles[pileIndex] >= matchesToRemove &&
                   matchesToRemove > 0;
        }
    }

    public class NimGame
    {
        private NimGameState _state; // pro privátní datové položky používáme podtržítko na začátku jména
        private bool _botStarts;
        private bool _isBotTurn;

        public NimGame(List<int> initialPiles, bool botStarts)
        {
            _state = new NimGameState(initialPiles);
            _botStarts = botStarts;
            _isBotTurn = botStarts;
        }

        public GameState PlayTurn()
        {           
            PrintGameState();

            if (_isBotTurn)
            {
                var botMove = GetBestBotMove();
                MakeAndPrintBotMove(botMove);
            }
            else
            {
                var humanMove = GetHumanInput();
                _state.MakeMove(humanMove.Item1, humanMove.Item2);
            }

            _isBotTurn = !_isBotTurn;

            if (_state.MatchesInGame == 0)
                if (_isBotTurn)
                    return GameState.BotWon;
                else
                    return GameState.HumanWon;
            else
                return GameState.Ongoing;
        }

        private Tuple<int, byte> GetBestBotMove()
        {
            Tuple<int, int, byte> result = minimax(_state.Piles.ToList(), 10, true);
            int score = result.Item1;
            int bestPile = result.Item2;
            byte matchesToRemove = result.Item3;

            List<Tuple<int, byte>> possibleMoves(List<int> piles)
            {
                List<Tuple<int, byte>> result = new List<Tuple<int, byte>>();
                for (int i = 0; i < piles.Count; i++)
                {
                    var pile = piles[i];
                    if (pile > 1)
                    {
                        result.Add(new Tuple<int, byte>(i, 0));
                        result.Add(new Tuple<int, byte>(i, 1));
                    }
                    else if (pile == 1)
                    {
                        result.Add(new Tuple<int, byte>(i, 0));
                    }

                }
                return result;
            }

            Tuple<int, int, byte> minimax(List<int> piles, int depth, bool maximizingPlayer)
            {
                List<Tuple<int, byte>> moves = possibleMoves(piles);
                if(depth == 0 || moves.Count() == 0)
                {
                    return new Tuple<int, int, byte>(ohodnoceni(piles, maximizingPlayer), -1, 0);
                }
                if(maximizingPlayer)
                {
                    Tuple<int, int, byte> best = new Tuple<int, int, byte>(-2, -1, 0);
                    foreach (Tuple<int, byte> i in moves)
                    {
                        List<int> new_piles = piles;
                        new_piles[i.Item1]
                        Tuple<int, int, byte> new_minimax = minimax(piles, depth-1, false);
                    }
                }
            }

            int ohodnoceni(List<int> piles, bool maximizingPlayer)
            {
                if (piles.Sum() == 0 && maximizingPlayer)
                    return -1;
                else if (piles.Sum() == 0 && !maximizingPlayer)
                    return 1;
                else
                    return 0;
            }


            return new Tuple<int, byte>(bestPile, matchesToRemove);
        }

        private void PrintGameState()
        {
            Console.WriteLine("Aktuální stav hry:");
            foreach (var pile in _state.Piles)
                Console.Write(pile + " ");
            Console.WriteLine();
        }

        private void MakeAndPrintBotMove(Tuple<int, byte> move)
        {
            _state.MakeMove(move.Item1, move.Item2);
            Console.WriteLine($"Počítač bere {move.Item2} sirky z hromádky {move.Item1}");
        }

        private Tuple<int, byte> GetHumanInput()
        {

            Console.Write("Z které hromádky chcete brát? (");
            for (int i = 0; i < _state.Piles.Count; i++)
            {
                if (_state.Piles[i] > 0)
                    Console.Write($"{i} ");
            }   
            Console.Write(")");

            int pileIndex = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Kolik sirek chcete vzít? (1-{_state.Piles[pileIndex]})");
            byte matches = Convert.ToByte(Console.ReadLine());

            return new Tuple<int, byte>(pileIndex, matches);
        }
    }

    
}