namespace NumberWang
{
    public interface IGameEngine
    {
        int[,] Board { get; set; }
        int[,] MoveMatrix { get; set; }
        int NextNumber { get; set; }
        
        bool GameOver();
        int GetMaxNumber();
        bool Move(Direction dir);
        int Score();
    }
}