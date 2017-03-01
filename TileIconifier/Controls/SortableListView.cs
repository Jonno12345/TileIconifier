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

using System.Collections;
using System.Windows.Forms;

namespace TileIconifier.Controls
{
    internal class SortableListView : SkinnableListView
    {
        protected int SortColumn;

        public SortableListView()
        {
            ColumnClick += SortableListView_ColumnClick;
            ColumnWidthChanging += OnColumnWidthChanging;
            HideSelection = false;
        }

        private void OnColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = Columns[e.ColumnIndex].Width;
        }

        private void SortableListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var listView = (SortableListView) sender;
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != SortColumn)
            {
                // Set the sort column to the new column.
                SortColumn = e.Column;
                // Set the sort order to ascending by default.
                listView.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                listView.Sorting = listView.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listView.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            listView.ListViewItemSorter = new ListViewItemComparer(e.Column, listView.Sorting);
        }
    }

    internal class ListViewItemComparer : IComparer
    {
        private readonly int _col;
        private readonly SortOrder _order;

        public ListViewItemComparer()
        {
            _col = 0;
            _order = SortOrder.Ascending;
        }

        public ListViewItemComparer(int column, SortOrder order)
        {
            _col = column;
            _order = order;
        }

        public int Compare(object x, object y)
        {
            var returnVal = string.CompareOrdinal(((ListViewItem) x).SubItems[_col].Text,
                ((ListViewItem) y).SubItems[_col].Text);
            // Determine whether the sort order is descending.
            if (_order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }
    }
}