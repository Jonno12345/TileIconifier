using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier
{
    /// <summary>
    /// Allows typing first few letters in a list box to jump to that item - a little hacky
    /// </summary>
    public class ListBoxWithTyping : ListBox
    {
        private DateTime _lastKeyPress;
        private string _searchString;

        public ListBoxWithTyping()
        {
            KeyPress += SearchByCharacters_KeyPress;
        }

        private void SearchByCharacters_KeyPress(object sender, KeyPressEventArgs e)
        {
            var newDate = DateTime.Now;
            var diff = newDate - _lastKeyPress;

            //reset search after 1.5 seconds
            if (diff.TotalSeconds >= 1.5)
                _searchString = string.Empty;
            _searchString += e.KeyChar.ToString().ToLower();

            //loop through all items in order
            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i].ToString();
                if (item.ToLower().StartsWith(_searchString))
                {
                    SelectedIndex = i;
                    break;
                }
            }
            _lastKeyPress = newDate;
            e.Handled = true;
        }
    }
}
