namespace SuperWordSearch
{
    public class Position
    {
        public int Column { get; private set; }
        public int Row { get; private set; }
        private readonly int _pNRows;
        private readonly int _pMColumns; 
        public WordWrap IsWordWrap { get; private set; }
        public bool IsValidPosition { get; private set; }

        public Position(int nRows, int mColumns, WordWrap isWordWrap)
        {
            _pNRows = nRows;
            _pMColumns = mColumns;
            IsWordWrap = isWordWrap;
        }

        public void SetPosition(int row, int col)
        {
            if (IsWordWrap == WordWrap.NO_WRAP)
            {
                Row = row;
                Column = col;
                if (row < 0 || row > _pNRows - 1 ||
                    col < 0 || col > _pMColumns - 1)
                {
                    IsValidPosition = false;
                }
                else
                {
                    IsValidPosition = true;
                }
                
            }
            else if (IsWordWrap == WordWrap.WRAP)
            {
                Row = row;
                Column = col;
                if (row < 0 || row > _pNRows - 1)
                {
                    Row = (row % _pNRows + _pNRows) % _pNRows;
                }
                if (col < 0 || col > _pMColumns - 1)
                {
                    Column = (col %_pMColumns + _pMColumns) % _pMColumns;
                }
                IsValidPosition = true;
            }
        }

        public Position GetNeighbor(Direction direction)
        {
            var neighbor = new Position(_pNRows,_pMColumns,IsWordWrap);
            var row = Row;
            var col = Column;

            switch (direction)
            {
                case Direction.Up:
                    neighbor.SetPosition(row - 1, col);
                    break;
                case Direction.Down:
                    neighbor.SetPosition(row + 1, col);
                    break;
                case Direction.Left:
                    neighbor.SetPosition(row, col - 1);
                    break;
                case Direction.Right:
                    neighbor.SetPosition(row, col + 1);
                    break;
                case Direction.TopLeft:
                    neighbor.SetPosition(row - 1, col - 1);
                    break;
                case Direction.TopRight:
                    neighbor.SetPosition(row - 1, col + 1);
                    break;
                case Direction.BottomLeft:
                    neighbor.SetPosition(row + 1, col - 1);
                    break;
                case Direction.BottomRight:
                    neighbor.SetPosition(row + 1, col + 1);
                    break;
            }

            return neighbor;
        }

        public override string ToString()
        {
            return "(" + Row + "," + Column + ")";
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        TopLeft,
        BottomLeft,
        TopRight,
        BottomRight
    }

    public enum WordWrap
    {
        NO_WRAP = 0,
        WRAP
    }
}