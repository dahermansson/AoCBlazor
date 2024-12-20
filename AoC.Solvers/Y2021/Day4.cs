namespace AoC.Solvers.Y2021;

public class Day4(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private IEnumerable<int> BingoInput = InputParsers.GetInputLines(input).First().Split(",").Select(t => int.Parse(t));

    private IEnumerable<Matrix<int>> BingoBoards = InputParsers.GetInputLines(input).Skip(2).Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => t.TrimStart().Replace("  ", " ")).Chunk(5).Select(t => new Matrix<int>(t, true)).ToArray();

    private bool CheckBingo(BingoBoard bingoBoard, List<int> drawnNumbers)
    {
        if (bingoBoard.Bingo)
            return true;
        foreach (var row in bingoBoard.Board.GetAllRows())
            if (row.All(t => drawnNumbers.Contains(t)))
            {
                bingoBoard.Bingo = true;
                return true;
            }
        foreach (var col in bingoBoard.Board.GetAllColumns())
            if (col.All(t => drawnNumbers.Contains(t)))
            {
                bingoBoard.Bingo = true;
                return true;
            }
        return false;
    }

    public int Star1()
    {
        var boards = BingoBoards.Select(t => new BingoBoard(t)).ToArray();
        var drawnNumbers = new List<int>(BingoInput.Take(5));
        foreach (var number in BingoInput.Skip(5).ToArray())
        {
            foreach (var bingoBoard in boards)
                if (CheckBingo(bingoBoard, drawnNumbers))
                    return bingoBoard.Board.GetAllValues().Where(t => !drawnNumbers.Contains(t)).Sum() * drawnNumbers.Last();
            drawnNumbers.Add(number);
        }
        return -1;
    }

    public int Star2()
    {
        var boards = BingoBoards.Select(t => new BingoBoard(t)).ToArray();
        var drawnNumbers = new List<int>(BingoInput.Take(5));
        foreach (var number in BingoInput.Skip(5).ToArray())
        {
            foreach (var bingoBoard in boards.Where(t => !t.Bingo))
                if (CheckBingo(bingoBoard, drawnNumbers) && boards.All(t => t.Bingo))
                    return bingoBoard.Board.GetAllValues().Where(t => !drawnNumbers.Contains(t)).Sum() * drawnNumbers.Last();
            drawnNumbers.Add(number);
        }
        return -1;
    }

    private class BingoBoard
    {
        public BingoBoard(Matrix<int> board)
        {
            Board = board;
        }
        public Matrix<int> Board { get; set; }
        public bool Bingo { get; set; } = false;
    }
}