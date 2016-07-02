using System.Collections.Generic;

namespace SuperWordSearch
{
    public class LetterMatrix
    {
        private readonly char[,] _pMatrix;
        private readonly int _pNRows;
        private readonly int _pMColumns;
        public Stack<Position> SearchTrace=new Stack<Position>(); 

        public char this[int n, int m]
        {
            get { return _pMatrix[n, m]; }
            set { _pMatrix[n,m] = value; }
        }

        public LetterMatrix(int n, int m)
        {
            _pMatrix = new char[n,m];
            _pNRows = n;
            _pMColumns = m;
        }

        public bool ExistsWord(string word, WordWrap isWordWrap)
        {
            if (_pMatrix == null || _pMatrix.Length == 0)
                return false;
            var visited = new bool[_pNRows, _pMColumns];
            var position = new Position(_pNRows, _pMColumns, isWordWrap);

            for (var i = 0; i < _pNRows; i++)
            {
                for (var j = 0; j < _pMColumns; j++)
                {
                    position.SetPosition(i, j);
                    if (DFS(word, ref visited, position, 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool DFS(string word, ref bool[,] visited, Position position, int wi)
        {
            if (!position.IsValidPosition)
            {
                return false;
            }

            var row = position.Row;
            var col = position.Column;

            if (!visited[row, col] && _pMatrix[row, col] == word[wi])
            {
                if (wi == word.Length - 1)
                {
                    SearchTrace.Push(position);
                    visited[row, col] = true;
                    return true;
                }

                visited[row, col] = true;
                var down = DFS(word, ref visited, position.GetNeighbor(Direction.Down), wi + 1);
                var right = DFS(word, ref visited, position.GetNeighbor(Direction.Right), wi + 1);
                var up = DFS(word, ref visited, position.GetNeighbor(Direction.Up), wi + 1);
                var left = DFS(word, ref visited, position.GetNeighbor(Direction.Left), wi + 1);

                var upLeft = DFS(word, ref visited, position.GetNeighbor(Direction.TopLeft), wi + 1);
                var upRight = DFS(word, ref visited, position.GetNeighbor(Direction.TopRight), wi + 1);
                var downLeft = DFS(word, ref visited, position.GetNeighbor(Direction.BottomLeft), wi + 1);
                var downRight = DFS(word, ref visited, position.GetNeighbor(Direction.BottomRight), wi + 1);

                visited[row, col] = up || down || left || right 
                                    || upLeft || upRight || downLeft || downRight;
                if (visited[row, col])
                {
                    SearchTrace.Push(position);
                }
                return visited[row, col];
            }

            return false;
        }
    }


}




