using System.Text;
using NUnit.Framework;

namespace SuperWordSearch
{

    public class Tests
    {
        [Test]
        public void TestInputProcessor()
        {
            var input = new InputProcessor("Input.txt");

            Assert.AreEqual(3,input.NRows);
            Assert.AreEqual(3, input.MColumns);
            Assert.AreEqual(5, input.PWords);
            Assert.AreEqual("FED",input.WordArray[0]);
            Assert.AreEqual('I', input.LetterMatrix[2,2]);
            Assert.AreEqual("NO_WRAP", input.IsWordWrap.ToString());

            var input2 = new InputProcessor("Input2.txt");

            Assert.AreEqual(4, input2.NRows);
            Assert.AreEqual(3, input2.MColumns);
            Assert.AreEqual(5, input2.PWords);
            Assert.AreEqual("AEIJBFG", input2.WordArray[2]);
            Assert.AreEqual('I', input2.LetterMatrix[2, 2]);
            Assert.AreEqual("WRAP", input2.IsWordWrap.ToString());
        }

        [Test]
        public void TestMatrixSearch()
        {
            var input = new InputProcessor("Input.txt");

            Assert.AreEqual(true, input.LetterMatrix.ExistsWord("FED", WordWrap.WRAP));
            Assert.AreEqual(true, input.LetterMatrix.ExistsWord("DEF", WordWrap.NO_WRAP));
            Assert.AreEqual(true, input.LetterMatrix.ExistsWord("GAD", WordWrap.WRAP));
            Assert.AreEqual(false, input.LetterMatrix.ExistsWord("GAD", WordWrap.NO_WRAP));
            Assert.AreEqual(true, input.LetterMatrix.ExistsWord("BID", WordWrap.WRAP));
            Assert.AreEqual(false, input.LetterMatrix.ExistsWord("BID", WordWrap.NO_WRAP));
        }

        [Test]
        public void TestMatrixSearchTrace()
        {
            var input = new InputProcessor("Input.txt");

            if (input.LetterMatrix.ExistsWord("BID", WordWrap.WRAP))
            {
                var str = @"(0,1)(2,2)(1,0)";
                var strOutput =new StringBuilder();
                while (input.LetterMatrix.SearchTrace.Count > 0)
                {
                    strOutput.Append(input.LetterMatrix.SearchTrace.Pop());
                }
                
                Assert.AreEqual(str, strOutput.ToString());
            }
            else
            {
                Assert.Fail("Not Found.");
            }
        }

        [Test]
        public void TestPosition()
        {
            var position = new Position(3, 3, WordWrap.WRAP);

            position.SetPosition(-1, -1);
            Assert.That(position.Row==2 && position.Column==2);

            position.SetPosition(0, 1);
            var neighbor = position.GetNeighbor(Direction.Up);
            Assert.That(neighbor.Row == 2 && neighbor.Column == 1);
            Assert.That(position.Row == 0 && position.Column == 1);

            position.SetPosition(0, 1);
            neighbor = position.GetNeighbor(Direction.Down);
            Assert.That(neighbor.Row == 1 && neighbor.Column == 1);
            Assert.That(position.Row == 0 && position.Column == 1);

            position.SetPosition(0, 1);
            neighbor = position.GetNeighbor(Direction.Left);
            Assert.That(neighbor.Row == 0 && neighbor.Column == 0);
            Assert.That(position.Row == 0 && position.Column == 1);

            position.SetPosition(0, 1);
            neighbor = position.GetNeighbor(Direction.Right);
            Assert.That(neighbor.Row == 0 && neighbor.Column == 2);
            Assert.That(position.Row == 0 && position.Column == 1);

            position.SetPosition(0, 1);
            neighbor = position.GetNeighbor(Direction.TopLeft);
            Assert.That(neighbor.Row == 2 && neighbor.Column == 0);
            Assert.That(position.Row == 0 && position.Column == 1);

            position.SetPosition(0, 1);
            neighbor = position.GetNeighbor(Direction.TopRight);
            Assert.That(neighbor.Row == 2 && neighbor.Column == 2);
            Assert.That(position.Row == 0 && position.Column == 1);

            position.SetPosition(0, 1);
            neighbor = position.GetNeighbor(Direction.BottomLeft);
            Assert.That(neighbor.Row == 1 && neighbor.Column == 0);
            Assert.That(position.Row == 0 && position.Column == 1);

            position.SetPosition(0, 1);
            neighbor = position.GetNeighbor(Direction.BottomRight);
            Assert.That(neighbor.Row == 1 && neighbor.Column == 2);
            Assert.That(position.Row == 0 && position.Column == 1);
        }
    }
}
