/****************************************************************************
 *  Library: KeyValues 
 *  Version: 1.0.0.0
 *  By: Callysto.net (http://www.callysto.net)
 *  Target: .NET FRAMEWORK 2.0
 *  
 * 
 *  This library is inspired on "Source Engine" (HL2SDK) KeyValues Class
 *  http://developer.valvesoftware.com/wiki/KeyValues_class (SourceSDK KeyValues Class)
 *  It was used to save data and for send data to a entity.
 *  It can have unlimited keys and subkeys inside a KeyValue
 *  SourceSDK KeyValues metods: http://svn.alliedmods.net/viewvc.cgi/hl2sdk/public/tier1/KeyValues.h?view=co&root=sourcemm
 *  Diferences: 
 *  * More Metods
 *  ** More easy to loop all keys
 *  *** Easy to update or add features
 *
 *  
 *  
 * ***************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Callysto
{
    /// <summary>
    /// KeyValues Class
    /// Can contain many keys and subkeys
    /// </summary>
    public sealed class KeyValues : IEquatable<KeyValues>, ICloneable
    {
        #region Variables

        private object TagObj;
        public object Tag
        {
            get { return TagObj; }
            set { TagObj = value; }
        }

        private string szName;
        public string Name
        {
            get { return szName; } 
            set { szName = value; }
        }

        private string szComment;
        public string Comment
        {
            get { return szComment; }
            set { szComment = value; }
        }

        private List<KeyValuesData> KeyNameValuesList;
        public List<KeyValuesData> KeyNameValues
        {
            get { return KeyNameValuesList; }
            private set { KeyNameValuesList = value; }
        }

        private List<KeyValues> KeyChildsList;
        public List<KeyValues> KeyChilds
        {
            get { return KeyChildsList; }
            private set { KeyChildsList = value; }
        }

        private List<string> IncludedFilesList;
        public List<string> IncludedFiles
        {
            get { return IncludedFilesList; }
            private set { IncludedFilesList = value; }
        }

        private KeyValues FirstParentKV;
        public KeyValues FirstParent
        {
            get { return FirstParentKV; }
            private set { FirstParentKV = value; }
        }

        private KeyValues ParentKV;
        public KeyValues Parent
        {
            get { return ParentKV; }
            private set { ParentKV = value; }
        }

        private uint iNextSubKeyIndex;
        public uint NextSubKeyIndex
        {
            get { return iNextSubKeyIndex; }
            private set { iNextSubKeyIndex = value; }
        }

        private uint iNextKeyValueIndex;
        public uint NextKeyValueIndex
        {
            get { return iNextKeyValueIndex; }
            private set { iNextKeyValueIndex = value; }
        }

        private uint iDistanceFromTop;
        public uint DistanceFromTop
        {
            get { return iDistanceFromTop; }
            private set { iDistanceFromTop = value; }
        }

        #endregion

        #region Constructors

        public KeyValues(string setName)
        {
            Init();
            Name = setName;
        }

        public KeyValues(string setName, string setComment)
            : this(setName)
        {
            Comment = setComment;
        }

        public KeyValues(string setName, string firstKey, string firstValue, string firstComment)
            : this(setName)
        {
            SetString(firstKey, firstValue);
            SetComment(firstKey, firstComment);
        }

        public KeyValues(string setName, string firstKey, string firstValue, string firstComment, string secoundKey, string secoundValue, string secoundComment)
            : this(setName)
        {
            SetString(firstKey, firstValue);
            SetComment(firstKey, firstComment);
            SetString(secoundKey, secoundValue);
            SetComment(secoundKey, secoundComment);
        }

        public KeyValues(string setName, KeyValuesData keyValue)
            : this(setName)
        {
            Set(keyValue);
        }

        public KeyValues(string setName, KeyValuesData[] keyValues)
            : this(setName)
        {
            SetRange(keyValues);
        }

        public KeyValues(string setName, KeyValues subKey)
            : this(setName)
        {
            AddSubKey(subKey);
        }

        public KeyValues(string setName, KeyValues[] subKeys)
            : this(setName)
        {
            AddSubKey(subKeys);
        }

        /// <summary>
        /// Prepare class to be used.
        /// </summary>
        private void Init()
        {
            IncludedFiles = new List<string>();
            KeyNameValues = new List<KeyValuesData>();
            Name = null;
            Comment = null;
            KeyChilds = new List<KeyValues>();
            FirstParent = this;
            Parent = null;
            NextSubKeyIndex = 0;
            NextKeyValueIndex = 0;
            DistanceFromTop = 0;
            Tag = null;
        }

        #endregion

        #region Remove KeyValues
        /// <summary>
        /// Clean and ReInit class
        /// </summary>
        public void Reset()
        {
            Clean();
            Init();
        }
        /// <summary>
        /// Clean all keyvalues and thier subkeys.
        /// </summary>
        public void Clean()
        {
            CleanFromKv(this, false);
        }
        /// <summary>
        /// Clean all keyvalues and/or thier subkeys.
        /// </summary>
        /// <param name="onlySubKeys">true if you want to remove only subkeys and keep keyvalues; otherwise clean both.</param>
        public void Clean(bool onlySubKeys)
        {
            CleanFromKv(this, onlySubKeys);
        }
        /// <summary>
        /// Start Cleaning a KeyValues and loop all thier childs.
        /// </summary>
        /// <param name="kv">Current KeyValues to clean.</param>
        /// <param name="onlySubKeys">true if you want to remove only subkeys and keep keyvalues; otherwise clean both.</param>
        private static void CleanFromKv(KeyValues kv, bool onlySubKeys)
        {
            if (!onlySubKeys)
            {
                for (int i = 0; i < kv.KeyNameValues.Count; i++)
                    kv.KeyNameValues[i].Parent = null;
                kv.KeyNameValues.Clear();
            }
            for (int i = 0; i < kv.KeyChilds.Count; i++)
            {
                CleanFromKv(kv.KeyChilds[i], onlySubKeys);
                kv.KeyChilds[i].Parent = null;
                kv.KeyChilds[i].FirstParent = null;
                kv.KeyChilds[i].DistanceFromTop = 0;
                kv.KeyChilds[i].NextSubKeyIndex = 0;
                kv.KeyChilds[i].NextKeyValueIndex = 0;
            }
            kv.KeyChilds.Clear();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        /// <param name="obj">An object to compare with this object.</param>
        public bool Equals(KeyValues obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj.Name, Name) && Equals(obj.KeyNameValues, KeyNameValues) &&
                   Equals(obj.KeyChilds, KeyChilds) && Equals(obj.FirstParent, FirstParent) &&
                   Equals(obj.Parent, Parent) && obj.NextSubKeyIndex == NextSubKeyIndex &&
                   obj.NextKeyValueIndex == NextKeyValueIndex && obj.DistanceFromTop == DistanceFromTop;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (KeyValues)) return false;
            return Equals((KeyValues) obj);
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
                int result = (Name != null ? Name.GetHashCode() : 0);
                result = (result*397) ^ (KeyNameValues != null ? KeyNameValues.GetHashCode() : 0);
                result = (result*397) ^ (KeyChilds != null ? KeyChilds.GetHashCode() : 0);
                result = (result*397) ^ (FirstParent != null ? FirstParent.GetHashCode() : 0);
                result = (result*397) ^ (Parent != null ? Parent.GetHashCode() : 0);
                result = (result*397) ^ NextSubKeyIndex.GetHashCode();
                result = (result*397) ^ NextKeyValueIndex.GetHashCode();
                result = (result*397) ^ DistanceFromTop.GetHashCode();
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
        public KeyValues Clone()
        {
            KeyValues cloneKV = new KeyValues(Name);
            cloneKV.DistanceFromTop = DistanceFromTop;
            cloneKV.Parent = Parent;
            cloneKV.Comment = Comment;
            cloneKV.NextKeyValueIndex = NextKeyValueIndex;
            cloneKV.NextSubKeyIndex = NextSubKeyIndex;
            cloneKV.IncludedFiles = new List<string>(IncludedFiles);
            cloneKV.KeyChilds = new List<KeyValues>(KeyChilds);
            cloneKV.KeyNameValues = new List<KeyValuesData>(KeyNameValues);
            return cloneKV;
        }

        /// <summary>
        /// Return KeyValues Name.
        /// </summary>
        /// <returns>
        /// KeyValues Name.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Operators

        public static bool operator ==(KeyValues left, KeyValues right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(KeyValues left, KeyValues right)
        {
            return !Equals(left, right);
        }

        public static KeyValues operator +(KeyValues kv1, KeyValues kv2)
        {
            KeyValues newKv = kv1.Clone();
            newKv.AddSubKey(kv2);
            return newKv;
        }

        public static KeyValues operator +(KeyValues kv1, KeyValuesData kv2)
        {
            KeyValues newKv = kv1.Clone();
            newKv.KeyNameValues.Add(kv2);
            return newKv;
        }

        public static KeyValues operator +(KeyValues kv1, string name)
        {
            KeyValues newKv = kv1.Clone();
            newKv.AddSubKey(new KeyValues(name));
            return newKv;
        }

        public static KeyValues operator -(KeyValues kv1, KeyValues kv2)
        {
            KeyValues newKv = kv1.Clone();
            newKv.RemoveSubKey(kv2);
            return newKv;
        }

        public static KeyValues operator -(KeyValues kv1, KeyValuesData kv2)
        {
            KeyValues newKv = kv1.Clone();
            newKv.RemoveKeyName(kv2);
            return newKv;
        }

        public static KeyValues operator -(KeyValues kv1, string name)
        {
            KeyValues newKv = kv1.Clone();
            newKv.RemoveSubKey(kv1.FindKey(name));
            return newKv;
        }

        #endregion

        #region Validate Tools
        /// <summary>
        /// Update a KeyValues child.
        /// This will update (First Parent, DistanceFromTop).
        /// </summary>
        /// <param name="child">Current child to update</param>
        /// <param name="distanceFromTop">Assert DistanceFromTop according with parent class</param>
        private void UpdateChilds(KeyValues child, uint distanceFromTop)
        {
            for(int i = 0; i < child.KeyChilds.Count; i++)
            {
                child.KeyChilds[i].FirstParent = FirstParent;
                child.KeyChilds[i].DistanceFromTop = distanceFromTop;
                UpdateChilds(child.KeyChilds[i], distanceFromTop+1);
            }
        }
        /// <summary>
        /// Update a new KeyValues and make child of this.
        /// This will update (First Parent, Parent, DistanceFromTop).
        /// This is for optional run, current functions automatic ajust and update all Propreities.
        /// Only run that 1 time in correct Child when necessary!
        /// This can be used for fix any bad update bug.
        /// </summary>
        /// <param name="newChild">SubKey to update and make child of this</param>
        /// <param name="appendedSubKey">true if newChild have an parent and you want apeendit to current Class; otherwise false</param>
        public void Update(KeyValues newChild, bool appendedSubKey)
        {
            if (appendedSubKey)
            {
                newChild.FirstParent = FirstParent;
                newChild.Parent = this;
                newChild.DistanceFromTop = DistanceFromTop + 1;
                UpdateChilds(newChild, DistanceFromTop + 2);
                return;
            }
            newChild.FirstParent = this;
            newChild.Parent = null;
            newChild.DistanceFromTop = 0;
            UpdateChilds(newChild, DistanceFromTop + 1);
        }
        /// <summary>
        /// Check if KeyValues object is valid
        /// </summary>
        /// <returns>
        /// true if the name != null; otherwise, false.
        /// </returns>
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Name))
                return false;
            return true;
        }
        /// <summary>
        /// Check if a KeyName Value is empty or NULL
        /// </summary>
        /// <returns>
        /// true if the value is null or empty; otherwise, false.
        /// </returns>
        /// <param name="keyName">An keyname for the value.</param>
        public bool IsEmpty(string keyName)
        {
            for (int i = 0; i < KeyNameValues.Count; i++)
            {
                if (KeyNameValues[i].Key == keyName)
                    return string.IsNullOrEmpty(KeyNameValues[i].Value);
            }
            return true;
        }
        /// <summary>
        /// Check if a filename is already in the list
        /// </summary>
        /// <returns>
        /// true if exists; otherwise, false.
        /// </returns>
        /// <param name="fileName">fileName to check.</param>
        public bool ExistsInclude(string fileName)
        {
            for (int i = 0; i < IncludedFiles.Count; i++)
            {
                if(IncludedFiles[i] == fileName)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Check if exists a KeyName under that KeyValues Class
        /// </summary>
        /// <returns>
        /// true if exists; otherwise, false.
        /// </returns>
        /// <param name="keyName">keyName to check.</param>
        public bool ExistsKey(string keyName)
        {
            for (int i = 0; i < KeyNameValues.Count; i++)
            {
                if (KeyNameValues[i].Key == keyName)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Check if exists a SubKey under that KeyValues Class
        /// </summary>
        /// <returns>
        /// true if exists; otherwise, false.
        /// </returns>
        /// <param name="subKeyName">SubKey Name to check.</param>
        public bool ExistsSubKey(string subKeyName)
        {
            for (int i = 0; i < KeyChilds.Count; i++)
            {
                if (KeyChilds[i].Name == subKeyName)
                    return true;
            }
            return false;
        }
        #endregion

        #region Load From HardDrive
        /// <summary>
        /// Parse a line and return a KeyValuePair
        /// </summary>
        /// <returns>
        /// KeyValuePair
        /// </returns>
        /// <param name="line">Current Line to parse.</param>
        /// <param name="wasQuoted">True if Key have quotes " so is not a { or }.</param>
        /// <param name="wasComment">True if Current line is a comment.</param>
        private static KeyValuePair<string, string> ReadToken(string line, ref bool wasQuoted, ref bool wasComment)
        {
            wasQuoted = false;
            wasComment = false;
            KeyValuePair<string, string> kvPair = new KeyValuePair<string, string>(null, null);
            if (string.IsNullOrEmpty(line))
                return kvPair;

            // eating white spaces
            line = line.Trim();
            if (string.IsNullOrEmpty(line))
                return kvPair;

            // return if is a comment
            if (line.StartsWith("//"))
            {
                wasComment = true;
                //return line.Remove(0, 2).TrimStart(' ');
                kvPair = new KeyValuePair<string, string>("//", line.Remove(0, 2).TrimStart(' '));
                return kvPair;
            }
            if (line.StartsWith("{") || line.StartsWith("}"))
            {
                // it's a control char, just add this one char and stop reading
                kvPair = new KeyValuePair<string, string>(line.Substring(0, 1), null);
                return kvPair;
                //s_pTokenBuf[1] = 0;
            }
            string key = null;
            uint keyCount = 0;
            // read quoted strings specially
            if (line.StartsWith("\""))
            {
                wasQuoted = true;
                line = line.Remove(0, 1);
                string[] delimeedString = line.Split(new char[] {'"'}, StringSplitOptions.RemoveEmptyEntries);
                bool assertSplit = false;
                foreach (string s in delimeedString)
                {
                    //if (string.IsNullOrEmpty(s.Trim())) continue;
                    if (assertSplit)
                    {
                        assertSplit = false;
                        continue;
                    }
                    if (keyCount == 0)
                    {
                        assertSplit = true;
                        key = s;
                        string lineCopy = line;
                        line = line.Replace(s, "").Trim();
                        if (!line.StartsWith("\""))
                        {
                            line = lineCopy;
                            break;
                        }
                        line = line.Remove(0, 1);
                    }
                    else if (keyCount == 1)
                    {
                        kvPair = new KeyValuePair<string, string>(key, s);
                        return kvPair;
                    }
                    keyCount++;
                }
            }
            if (keyCount == 0)
                key = "";
            string value = "";
            // read in the token until we hit a whitespace or a control character
            foreach (char c in line)
            {
                // break if any control character appears in non quoted tokens
                // break on whitespace
                if (c == '{' || c == '}')
                    break;
                if (c == '"' || c == ' ' || c == '\t')
                {
                    // This will keep eating '"' '' '\t' util get a valid text
                    if (keyCount != 0 && string.IsNullOrEmpty(value))
                        continue;
                    keyCount++; // Pass From Key to Value
                    continue;
                }
                // Set String Chat By Char
                if (keyCount == 0)
                    key += c;
                else if (keyCount == 1)
                    value += c;
                else
                    break;
            }
            // Empty or NULL Checks (To return valid data)
            if (string.IsNullOrEmpty(key))
            {
                key = null;
                value = null;
            }
            else if (string.IsNullOrEmpty(value))
            {
                value = null;
            }
            kvPair = new KeyValuePair<string, string>(key, value);
            return kvPair;
        }
        /// <summary>
        /// Search for a string on a list and return thier index
        /// </summary>
        /// <returns>
        /// true if the sucessfully load (KeyValue is valid); otherwise, false.
        /// </returns>
        /// <param name="lines">List Collection (Target).</param>
        /// <param name="search">Search for that text.</param>
        /// <param name="startIndex">Start search on that location.</param>
        private uint RetriveIndex(List<string> lines, string search, uint startIndex)
        {
            if(string.IsNullOrEmpty(search)) return 0;
            bool wasQuote = false;
            bool wasComment = false;
            for (int i = (int)startIndex; i < lines.Count; i++)
            {
                KeyValuePair<string,string> kvPair = ReadToken(lines[i], ref wasQuote, ref wasComment);
                if (kvPair.Key == search && !wasComment && !wasQuote)
                    return (uint)i;
            }
            return 0;
        }
        /// <summary>
        /// Create a KeyValue from File
        /// This will load KeyValues data from a file stored in HDD
        /// </summary>
        /// <returns>
        /// true if the sucessfully load (KeyValue is valid); otherwise, false.
        /// </returns>
        /// <param name="fileName">Path of the file to load.</param>
        public bool LoadFromFile(string fileName)
        {
            //byte[] buffer = Utils.ReadFile(fileName);
//            StreamReader sr = new StreamReader(fileName);
            uint endPos = 0;
            return LoadFromList(Utils.ReadFileToList(fileName), 0, ref endPos);
        }
        /// <summary>
        /// Create a KeyValue from List wich contains file lines
        /// </summary>
        /// <returns>
        /// true if the sucessfully load (KeyValue is valid); otherwise, false.
        /// </returns>
        /// <param name="stream">List collection wich contain file lines</param>
        /// <param name="startPos">Start parse List in that position</param>
        /// <param name="endPos">Return end position index</param>
        private bool LoadFromList(List<string> stream, uint startPos, ref uint endPos)
        {
            if (stream == null) return false;
            bool wasQuoted = false;
            bool wasComment = false;
            string lastComment = null;
            bool wasName = false;
            endPos = 0;
            for (uint i = startPos; i < stream.Count; i++)
            {
                KeyValuePair<string, string> kvPair = ReadToken(stream[(int) i], ref wasQuoted, ref wasComment);
                if (string.IsNullOrEmpty(kvPair.Key)) continue;
                endPos = i;
                // Is the end of KeyValues Class?
                if (kvPair.Key == "}")
                {
                    endPos = i + 1;
                    return Name == null ? false : true;
                }
                // This line is a comment
                if (wasComment)
                {
                    lastComment = kvPair.Value;
                    continue;
                }
                // KeyValue Name not yet seted!
                if (!wasName)
                {
                    if (!string.IsNullOrEmpty(kvPair.Value)) return false;
                    Name = kvPair.Key;
                    if (!string.IsNullOrEmpty(lastComment))
                        Comment = lastComment;
                    wasName = true;
                    lastComment = null;
                    uint beganIndex = RetriveIndex(stream, "{", i + 1);
                    if (beganIndex == 0) return false;
                    i = beganIndex;
                    continue;
                }
                // special include macro (not a key name)
                if (kvPair.Key.StartsWith("#include") && !wasQuoted)
                {
                    lastComment = null;
                    if (string.IsNullOrEmpty(kvPair.Value)) continue;
                    AddIncludeFile(kvPair.Value, true, true);
                    continue;
                }
                // Begin a new KeyValue Class
                if (kvPair.Key == "{")
                {
                    if (!wasQuoted)
                    {
                        lastComment = null;
                        KeyValues kvChild = new KeyValues("NewName");
                        if (!kvChild.LoadFromList(stream, i - 1, ref endPos)) continue;
                        Update(kvChild, true);
                        KeyChilds.Add(kvChild);
                        if (endPos >= stream.Count) return true;
                        i = endPos;
                        continue;
                    }
                }
                // Is a KeyValue    "Key"       "Value"
                if (string.IsNullOrEmpty(kvPair.Value)) continue;
                KeyValuesData kvData = new KeyValuesData(kvPair.Key, kvPair.Value, lastComment, this);
                KeyNameValues.Add(kvData);
                lastComment = null;
            }
            return true;
        }
        /// <summary>
        /// Include or just save fileName into current KeyValues Class      
        /// </summary>
        /// <returns>
        /// true if the sucessfully include file (KeyValue is valid); otherwise, false.
        /// </returns>
        /// <param name="fileName">File to include on current KeyValues Class</param>
        /// <param name="loadToKeyValues">if true will load file and in current KeyValues Class; otherwise false.</param>
        /// <param name="addToList">if true on save KeyValues to HDD will put a #include macro for the fileName on correct place; otherwise false.</param>
        public bool AddIncludeFile(string fileName, bool loadToKeyValues, bool addToList)
        {
            if (addToList)
                if (!ExistsInclude(fileName))
                    IncludedFiles.Add(fileName);
            if (loadToKeyValues)
            {
                KeyValues incKv = new KeyValues("include");
                if (!incKv.LoadFromFile(fileName)) return false;
                /*incKv.FirstParent = Parent == null ? this : Parent.FirstParent;
                incKv.Parent = this;
                incKv.DistanceFromTop = DistanceFromTop + 1;*/
                Update(incKv, true);
                KeyChilds.Add(incKv);
            }
            return true;
        }
        /// <summary>
        /// Include a KeyValues file into current KeyValues Class    
        /// </summary>
        /// <returns>
        /// true if the sucessfully include file (KeyValue is valid); otherwise, false.
        /// </returns>
        /// <param name="fileName">File to include on current KeyValues Class</param>
        /// <param name="addToList">if true on save KeyValues to HDD will put a #include macro for the fileName on correct place; otherwise false.</param>
        public bool AddIncludeFile(string fileName, bool addToList)
        {
            return AddIncludeFile(fileName, addToList);
        }
        #endregion

        #region Save To HardDrive
        /// <summary>
        /// Save KeyValue to a file on HDD
        /// </summary>
        /// <returns>
        /// true if file was sucessfully saved to hdd; otherwise, false.
        /// </returns>
        /// <param name="fileName">Path to store file.</param>
        public bool SaveToFile(string fileName)
        {
            try
            {
                StreamWriter sw = new StreamWriter(fileName);
                SaveFromChild(sw, this, 0);
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Continue saving a KeyValue to a file.
        /// </summary>
        /// <param name="sw">The file Writer object to Write contents on file.</param>
        /// <param name="kv">Current KeyValues to print on file.</param>
        /// <param name="tabSpaces">Tab Spaces to give in each line (Depend on each KeyValue Level).</param>
        private static void SaveFromChild(StreamWriter sw, KeyValues kv, uint tabSpaces)
        {
            if (!string.IsNullOrEmpty(kv.Comment))
            {
                Utils.makeTabs(sw, tabSpaces);
                sw.WriteLine("// {0}", kv.Comment);
            }
            Utils.makeTabs(sw, tabSpaces);
            sw.WriteLine("\"{0}\"", kv.Name);
            Utils.makeTabs(sw, tabSpaces);
            sw.WriteLine("{");
            foreach (KeyValuesData data in kv.KeyNameValues)
            {
                if (!string.IsNullOrEmpty(data.Comment))
                {
                    Utils.makeTabs(sw, tabSpaces);
                    sw.WriteLine("\t// {0}", data.Comment);
                }
                Utils.makeTabs(sw, tabSpaces);
                sw.WriteLine("\t\"{0}\"\t\t\"{1}\"", data.Key, data.Value);
            }
            foreach (KeyValues child in kv.KeyChilds)
            {
                SaveFromChild(sw, child, tabSpaces + 1);
            }
            Utils.makeTabs(sw, tabSpaces);
            sw.WriteLine("}");
        }

        #endregion

        #region SubKeys Management
        /// <summary>
        /// Add a SubKey to the current KeyValues
        /// </summary>
        /// <param name="pSubkey">KeyValue to Append.</param>
        public void AddSubKey(KeyValues pSubkey)
        {
            if (!pSubkey.IsValid()) return;
            Update(pSubkey, true);
            KeyChilds.Add(pSubkey);
        }

        public void AddSubKey(KeyValues[] pSubkeys)
        {
            foreach (KeyValues subkey in pSubkeys)
            {
                AddSubKey(subkey);
            }
        }

        public bool RemoveMe()
        {
            if (Parent == null) return false;
            return Parent.RemoveSubKey(this);
        }

        public bool RemoveSubKey(int index)
        {
            try
            {
                KeyChilds.RemoveAt(index);
                Update(KeyChilds[index], false);
                return true;
            }
            catch(ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        public bool RemoveSubKey(KeyValues pSubkey)
        {
            if (!KeyChilds.Remove(pSubkey)) return false;
            Update(pSubkey, false);
            return true;
        }

        public bool RemoveSubKey(string keyName)
        {
            if (string.IsNullOrEmpty(keyName)) return false;
            for (int i = 0; i < KeyChilds.Count; i++)
            {
                if (KeyChilds[i].Name == keyName)
                    return RemoveSubKey(i);
            }
            return false;
        }

        public KeyValues FindKey(string keyName, bool create)
        {
            for (int i = 0; i < KeyChilds.Count; i++)
            {
                if (KeyChilds[i].Name == keyName)
                    return KeyChilds[i];
            }
            if (!create) return null;
            KeyValues newKv = new KeyValues(keyName);
            newKv.FirstParent = Parent == null ? this : Parent.FirstParent;
            newKv.Parent = this;
            newKv.DistanceFromTop = DistanceFromTop + 1;
            return newKv;
        }

        public KeyValues FindKey(string keyName)
        {
            return FindKey(keyName, false);
        }

        public KeyValues FindKey(int index)
        {
            if (index < KeyChilds.Count && index >= 0)
                return KeyChilds[index];
            return null;
        }

        #endregion

        #region SubKey and KeyValues GetFirst/Next

        public KeyValues GetFirstSubKey(bool resetNextSubKeyCount)
        {
            if (resetNextSubKeyCount)
                NextSubKeyIndex = 1;
            else if (NextSubKeyIndex <= 0)
                NextSubKeyIndex = 1;
            if (KeyChilds.Count > 0)
                return KeyChilds[0];
            return null;
        }

        public KeyValues GetNextSubKey()
        {
            if (NextSubKeyIndex < KeyChilds.Count)
            {
                NextSubKeyIndex++;
                return KeyChilds[(int) NextSubKeyIndex - 1];
            }
            return null;
        }

        public KeyValuesData GetFirstKeyValue(bool resetNextKeyValueCount)
        {
            if (resetNextKeyValueCount)
                NextKeyValueIndex = 1;
            else if (NextKeyValueIndex <= 0)
                NextKeyValueIndex = 1;
            if (KeyNameValues.Count > 0)
                return KeyNameValues[0];
            return null;
        }

        public KeyValuesData GetNextKeyValue()
        {
            if (NextKeyValueIndex < KeyNameValues.Count)
            {
                NextKeyValueIndex++;
                return KeyNameValues[(int) NextKeyValueIndex - 1];
            }
            return null;
        }

        #endregion

        #region KeyValues Management

        public uint RemoveAllKeyNames()
        {
            return RemoveAllKeyNames(0);
        }

        public uint RemoveAllKeyNames(uint startPos)
        {
            uint count = 0;
            for (uint i = startPos; i < KeyNameValues.Count; i++)
            {
                KeyNameValues[(int)i].Parent = null;
                count++;
            }
            KeyNameValues.Clear();
            return count;
        }

        public bool RemoveKeyName(int index)
        {
            if (index >= KeyNameValues.Count || index < 0) return false;
            KeyNameValues[index].Parent = null;
            KeyNameValues.RemoveAt(index);
            return true;
        }

        public bool RemoveKeyName(string keyName)
        {
            if (string.IsNullOrEmpty(keyName)) return false;
            for (int i = 0; i < KeyNameValues.Count; i++)
            {
                if (KeyNameValues[i].Key == keyName)
                {
                    KeyNameValues[i].Parent = null;
                    KeyNameValues.RemoveAt(i);
                    return true;
                }
            }
            return true;
        }

        public bool RemoveKeyName(KeyValuesData keyData)
        {
            if (KeyNameValues.Remove(keyData))
            {
                keyData.Parent = null;
                return true;
            }
            return false;
        }

        #endregion

        #region Set Values
        public bool SetComment(string keyName, string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            for (int i = 0; i < KeyNameValues.Count; i++)
            {
                if (KeyNameValues[i].Key == keyName)
                {
                    KeyNameValues[i].Comment = value;
                    return true;
                }
            }
            return false;
        }

        public bool SetComment(uint index, string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            if(index >= KeyNameValues.Count)
                return false;
            KeyNameValues[(int) index].Comment = value;
            return true;
        }

        public void Set(KeyValuesData kvData)
        {
            if (!kvData.IsValid()) return;
            kvData.Parent = this;
            KeyNameValues.Add(kvData);
        }

        public void SetRange(KeyValuesData[] range)
        {
            foreach (KeyValuesData data in range)
                Set(data);
        }

        public void SetString(string keyName, string value)
        {
            if (string.IsNullOrEmpty(value))
                value = "";
            for (int i = 0; i < KeyNameValues.Count; i++)
            {
                if (KeyNameValues[i].Key == keyName)
                {
                    KeyNameValues[i].Value = value;
                    return;
                }
            }
            KeyValuesData kvValue = new KeyValuesData(keyName, value, null, this);
            KeyNameValues.Add(kvValue);
        }

        public void SetString(KeyValuePair<string, string> kvPair)
        {
            SetString(kvPair.Key, kvPair.Value);
        }

        public void SetChar(string keyName, char value)
        {
            SetString(keyName, value.ToString());
        }

        public void SetByte(string keyName, byte value)
        {
            SetString(keyName, value.ToString());
        }

        public void SetBool(string keyName, bool value)
        {
            SetString(keyName, value.ToString());
        }

        public void SetShort(string keyName, short value)
        {
            SetString(keyName, value.ToString());
        }

        public void SetInt(string keyName, int value)
        {
            SetString(keyName, value.ToString());
        }

        public void SetLong(string keyName, long value)
        {
            SetString(keyName, value.ToString());
        }

        public void SetFloat(string keyName, float value)
        {
            SetString(keyName, value.ToString());
        }

        public void SetDecimal(string keyName, decimal value)
        {
            SetString(keyName, value.ToString());
        }

        public void SetDouble(string keyName, double value)
        {
            SetString(keyName, value.ToString());
        }

        public void SetDateTime(string keyName, DateTime value)
        {
            SetString(keyName, value.ToString());
        }

        public void SetColor(string keyName, Color value)
        {
            SetString(keyName, value.ToKnownColor().ToString());
        }

        #endregion

        #region Get Values

        public string GetComment(string keyName)
        {
            return GetComment(keyName, null);
        }

        public string GetComment(string keyName, string defaultValue)
        {
            if (string.IsNullOrEmpty(keyName))
                return defaultValue;
            for (int i = 0; i < KeyNameValues.Count; i++)
                if (KeyNameValues[i].Key == keyName)
                    return KeyNameValues[i].Comment;
            return defaultValue;
        }

        public string GetValue(string keyName)
        {
            return GetValue(keyName, null);
        }

        public string GetValue(string keyName, string defaultValue)
        {
            if (string.IsNullOrEmpty(keyName))
                return defaultValue;
            for (int i = 0; i < KeyNameValues.Count; i++)
                if (KeyNameValues[i].Key == keyName)
                    return KeyNameValues[i].Value;
            return defaultValue;
        }

        public char GetValue(string keyName, char defaultValue)
        {
            return Convert.ToChar(GetValue(keyName, defaultValue.ToString()));
        }

        public byte GetValue(string keyName, byte defaultValue)
        {
            return Convert.ToByte(GetValue(keyName, defaultValue.ToString()));
        }

        public bool GetValue(string keyName, bool defaultValue)
        {
            return Convert.ToBoolean(GetValue(keyName, defaultValue.ToString()));
        }

        public short GetValue(string keyName, short defaultValue)
        {
            return Convert.ToInt16(GetValue(keyName, defaultValue.ToString()));
        }

        public int GetValue(string keyName, int defaultValue)
        {
            return Convert.ToInt32(GetValue(keyName, defaultValue.ToString()));
        }

        public long GetValue(string keyName, long defaultValue)
        {
            return Convert.ToInt64(GetValue(keyName, defaultValue.ToString()));
        }

        public float GetValue(string keyName, float defaultValue)
        {
            return Convert.ToSingle(GetValue(keyName, defaultValue.ToString()));
        }

        public decimal GetValue(string keyName, decimal defaultValue)
        {
            return Convert.ToDecimal(GetValue(keyName, defaultValue.ToString()));
        }

        public double GetValue(string keyName, double defaultValue)
        {
            return Convert.ToDouble(GetValue(keyName, defaultValue.ToString()));
        }

        public DateTime GetValue(string keyName, DateTime defaultValue)
        {
            return Convert.ToDateTime(GetValue(keyName, defaultValue.ToString()));
        }

        public Color GetValue(string keyName, Color defaultValue)
        {
            return Color.FromName(GetValue(keyName, defaultValue.ToString()));
        }

        #endregion

        #region Utilities
        /// <summary>
        /// Create a new KeyValue with indexed Alphabet.
        /// </summary>
        public static KeyValues UTIL_CreateAlphabet()
        {
            KeyValues alphabet = new KeyValues("Alphabet");
            alphabet.SetChar("1", 'A');
            alphabet.SetChar("2", 'B');
            alphabet.SetChar("3", 'C');
            alphabet.SetChar("4", 'D');
            alphabet.SetChar("5", 'E');
            alphabet.SetChar("6", 'F');
            alphabet.SetChar("7", 'G');
            alphabet.SetChar("8", 'H');
            alphabet.SetChar("9", 'I');
            alphabet.SetChar("10", 'J');
            alphabet.SetChar("11", 'K');
            alphabet.SetChar("12", 'L');
            alphabet.SetChar("13", 'M');
            alphabet.SetChar("14", 'N');
            alphabet.SetChar("15", 'O');
            alphabet.SetChar("16", 'P');
            alphabet.SetChar("17", 'Q');
            alphabet.SetChar("18", 'R');
            alphabet.SetChar("19", 'S');
            alphabet.SetChar("20", 'T');
            alphabet.SetChar("21", 'U');
            alphabet.SetChar("22", 'V');
            alphabet.SetChar("23", 'W');
            alphabet.SetChar("24", 'X');
            alphabet.SetChar("25", 'Y');
            alphabet.SetChar("26", 'Z');
            return alphabet;
        }
        #endregion
    }
}