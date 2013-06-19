
using System.Text;
namespace ezetop.AurisPinLib
{
    public class AurisPin
    {
        public long Pin { get; private set; }
        public int CardID { get; private set; }
        public int BatchNo { get; private set; }
        public int BatchSeq { get; private set; }

        public AurisPin(string pinLine)
        {
            string[] fields = pinLine.Split(new char[1] { ',' });

            this.Initialize(long.Parse(fields[0]), int.Parse(fields[1]),
                int.Parse(fields[2]), int.Parse(fields[3]));
        }

        private void Initialize(long pin, int cardID, int batchNo, int batchSeq)
        {
            this.Pin = pin;
            this.CardID = cardID;
            this.BatchNo = batchNo;
            this.BatchSeq = batchSeq;
        }

        public override string ToString()
        {
            StringBuilder lineBuilder = new StringBuilder(32);

            lineBuilder.Append(this.Pin).Append(',');
            lineBuilder.Append(this.CardID).Append(',');
            lineBuilder.Append(this.BatchNo).Append(',');
            lineBuilder.Append(this.BatchSeq);

            return lineBuilder.ToString();
        }
    }
}
