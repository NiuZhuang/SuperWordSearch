using System;
using System.IO;

namespace SuperWordSearch
{
    public class InputProcessor
    {
        public int NRows { get;private set; }
        public int MColumns { get; private set; }
        public LetterMatrix LetterMatrix { get; private set; }
        public WordWrap IsWordWrap { get; private set; }
        public int PWords { get; private set; }
        public string[] WordArray { get; private set; }
        
        public InputProcessor(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File " + fileName + " doesn't exist.");
                throw new Exception("File " + fileName + " doesn't exist.");
            }
            else
            {
                using (var sr = File.OpenText(fileName))
                {
                    var s = "";
                    var i = 0;
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (i==0)
                        {
                            var nm = s.Split(' ');
                            NRows = int.Parse(nm[0]);
                            MColumns = int.Parse(nm[1]);
                            LetterMatrix = new LetterMatrix(NRows,MColumns);
                        }
                        if (i>0 && i <= NRows)
                        {
                            var r = i - 1;
                            for (var j = 0; j < s.Length; j++)
                            {
                                LetterMatrix[r, j] = s[j];
                            }
                        }
                        if (i == NRows+1)
                        {
                            IsWordWrap = (WordWrap) Enum.Parse(typeof(WordWrap),s);
                        }
                        if (i==NRows+2)
                        {
                            PWords = int.Parse(s);
                            WordArray = new string[PWords];
                        }
                        if (i> NRows+2 && i <= NRows+PWords+2)
                        {
                            var w = i - 3 - NRows;
                            WordArray[w] = s;
                        }
                        i++;
                    }
                }

            }
        }

        public void DisplayResult()
        {
            foreach (var word in WordArray)
            {
                if (LetterMatrix.ExistsWord(word, IsWordWrap))
                {
                    Console.Write(LetterMatrix.SearchTrace.Pop());
                    while (LetterMatrix.SearchTrace.Count>0)
                    {
                        if (LetterMatrix.SearchTrace.Count == 1)
                            Console.Write(LetterMatrix.SearchTrace.Pop());
                        else
                            LetterMatrix.SearchTrace.Pop();
                    }
                    Console.Write(Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("NOT FOUND");
                }
            }
        }
    }

    
}
