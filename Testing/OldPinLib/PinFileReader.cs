using System;
using System.Collections.Generic;
using System.Collections;

namespace ezetop.AurisPinLib
{
    public class PinFileReader : IEnumerable<AurisPin>
    {
        private Uri Location;
        private IEnumerator<AurisPin> Enumerator;

        public PinFileReader(Uri location)
        {
            this.Location = location;
            this.Enumerator = new PinFileEnumerator(location);
        }

        #region IEnumerable<AurisPin> Members

        public IEnumerator<AurisPin> GetEnumerator()
        {
            return this.Enumerator;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Enumerator;
        }

        #endregion
    }
}
