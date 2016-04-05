#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2016 Johnathon M
// 
//         Permission is hereby granted, free of charge, to any person obtaining a copy
//         of this software and associated documentation files (the "Software"), to deal
//         in the Software without restriction, including without limitation the rights
//         to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//         copies of the Software, and to permit persons to whom the Software is
//         furnished to do so, subject to the following conditions:
// 
//         The above copyright notice and this permission notice shall be included in
//         all copies or substantial portions of the Software.
// 
//         THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//         IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//         FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//         AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//         LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//         OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//         THE SOFTWARE.
// 
// */

#endregion

using System;

namespace TileIconifier.Core.Custom.Steam.KeyValues
{
    /// <summary>
    ///     'Key' 'Value' Data
    /// </summary>
    public sealed class KeyValuesData : IEquatable<KeyValuesData>, ICloneable
    {
        #region Validators

        /// <summary>
        ///     Check if that class is valid, true if Key is not null or empty.
        /// </summary>
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Key))
                return false;
            return true;
        }

        #endregion

        #region Management

        /// <summary>
        ///     Removes that Key from KeyValues Parent.
        /// </summary>
        public bool RemoveMe()
        {
            if (Parent == null)
                return false;
            return Parent.RemoveKeyName(this);
        }

        #endregion

        #region Variables

        /// <summary>
        ///     Gets or Sets the Key Name.
        /// </summary>
        public string Key;

        /// <summary>
        ///     Gets or Sets the Value of the Key.
        /// </summary>
        public string Value;

        /// <summary>
        ///     Gets or Sets Comment for that Key.
        /// </summary>
        public string Comment;

        /// <summary>
        ///     Gets or Sets the KeyValues Parent for that Key.
        ///     return null if there are no Parent.
        /// </summary>
        public KeyValues Parent;

        public object Tag { get; set; }

        #endregion

        #region Constructors

        public KeyValuesData(string key, string value, string comment, KeyValues parent)
        {
            Tag = null;
            Key = key;
            Value = value;
            Comment = comment;
            Parent = parent;
        }

        public KeyValuesData(string key, string value, string comment) : this(key, value, comment, null)
        {
        }

        public KeyValuesData()
        {
            Reset();
        }

        public void Reset()
        {
            Key = null;
            Value = null;
            Comment = null;
            Parent = null;
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (KeyValuesData)) return false;
            return Equals((KeyValuesData) obj);
        }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        /// <param name="obj">An object to compare with this object.</param>
        public bool Equals(KeyValuesData obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj.Key, Key) && Equals(obj.Value, Value) && Equals(obj.Parent, Parent);
        }

        /// <summary>
        ///     Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use
        ///     in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>
        ///     A hash code for the current <see cref="T:System.Object"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            // Getting hash codes from volatile variables doesn't seem a good move... //TODO Find an immutable way?

            var result = Key?.GetHashCode() ?? 0;
            result = (result*397) ^ (Value?.GetHashCode() ?? 0);
            result = (result*397) ^ (Parent?.GetHashCode() ?? 0);
            return result;
        }

        /// <summary>
        ///     Clone current Class.
        /// </summary>
        /// <returns>
        ///     Clonned Class.
        /// </returns>
        object ICloneable.Clone()
        {
            return Clone();
        }

        /// <summary>
        ///     Clone current Class.
        /// </summary>
        /// <returns>
        ///     Clonned Class.
        /// </returns>
        public KeyValuesData Clone()
        {
            return new KeyValuesData(Key, Value, Comment, Parent);
        }

        #endregion

        #region Operators

        public static bool operator ==(KeyValuesData left, KeyValuesData right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(KeyValuesData left, KeyValuesData right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}