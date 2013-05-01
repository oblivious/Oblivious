// NAnt - A .NET build tool
// Copyright (C) 2001-2003 Gerry Shaw
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
// Gerry Shaw (gerry_shaw@yahoo.com)

using System;

namespace NAnt.Core.Attributes {
    /// <summary>
    /// Base class for all validator attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited=true)]
    public abstract class ValidatorAttribute : Attribute {
        #region Public Instance Methods

        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <exception cref="ValidationException">The validation fails.</exception>
        public abstract void Validate(object value);

        #endregion Public Instance Methods
    }
}