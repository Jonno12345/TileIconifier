// Class for hold "Key" "Value" data
using System;

namespace Callysto
{
    /// <summary>
    /// 'Key' 'Value' Data
    /// </summary>
    public sealed class KeyValuesData : IEquatable<KeyValuesData>, ICloneable
    {
        #region Variables

        /// <summary>
        /// Gets or Sets the Key Name.
        /// </summary>
        public string Key;
        /// <summary>
        /// Gets or Sets the Value of the Key.
        /// </summary>
        public string Value;

        /// <summary>
        /// Gets or Sets Comment for that Key.
        /// </summary>
        public string Comment;
        /// <summary>
        /// Gets or Sets the KeyValues Parent for that Key.
        /// return null if there are no Parent.
        /// </summary>
        public KeyValues Parent;
        private object TagObj;
        public object Tag{
            get { return TagObj; }
            set { TagObj = value; }
        }
        #endregion
        #region Constructors
        public KeyValuesData(string Key, string Value, string Comment, KeyValues parent)
        {
            Tag = null;
            this.Key = Key;
            this.Value = Value;
            this.Comment = Comment;
            Parent = parent;
        }
        public KeyValuesData(string Key, string Value, string Comment) : this(Key, Value, Comment, null)
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
        #region Validators
        /// <summary>
        /// Check if that class is valid, true if Key is not null or empty.
        /// </summary>
        public bool IsValid()
        {
            if(string.IsNullOrEmpty(Key))
                return false;
            return true;
        }
        #endregion
        #region Management
        /// <summary>
        /// Removes that Key from KeyValues Parent.
        /// </summary>
        public bool RemoveMe()
        {
            if(Parent == null)
                return false;
            return Parent.RemoveKeyName(this);
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
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        /// <param name="obj">An object to compare with this object.</param>
        public bool Equals(KeyValuesData obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj.Key, Key) && Equals(obj.Value, Value) && Equals(obj.Parent, Parent);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Key != null ? Key.GetHashCode() : 0);
                result = (result*397) ^ (Value != null ? Value.GetHashCode() : 0);
                result = (result*397) ^ (Parent != null ? Parent.GetHashCode() : 0);
                return result;
            }
        }

        /// <summary>
        /// Clone current Class.
        /// </summary>
        /// <returns>
        /// Clonned Class.
        /// </returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
        /// <summary>
        /// Clone current Class.
        /// </summary>
        /// <returns>
        /// Clonned Class.
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
