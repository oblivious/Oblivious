using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ezetop.AurisPinLib
{
    internal class PinFileEnumerator : IEnumerator<AurisPin>
    {
        private Uri Location;
        private StreamReader Reader;
        private AurisPin current;

        public PinFileEnumerator(Uri location)
        {
            this.Location = location;
            this.Recycle();
        }

        private void Recycle()
        {
            this.Reader = new StreamReader(this.Location.LocalPath, Encoding.ASCII);
            /* skips first line */
            this.Reader.ReadLine();
        }

        #region IEnumerator<string> Members

        public AurisPin Current
        {
            get { return current; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.Reader.Close();
            this.Reader.Dispose();
        }

        #endregion

        #region IEnumerator Members

        object System.Collections.IEnumerator.Current
        {
            get { return current; }
        }

        public bool MoveNext()
        {
            bool readyToMove = !this.Reader.EndOfStream;

            if (readyToMove)
            {
                this.current = new AurisPin(this.Reader.ReadLine());
                Console.WriteLine("New line = {0},{1},{2},{3}.",
                    this.current.Pin,
                    this.current.CardID,
                    this.current.BatchNo,
                    this.current.BatchSeq);
            }

            return readyToMove;
        }

        public void Reset()
        {
            this.Recycle();
        }

        #endregion
    }
}
