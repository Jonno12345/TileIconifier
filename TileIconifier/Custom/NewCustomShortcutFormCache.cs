using System.Drawing;
using System.Linq;
using TileIconifier.Utilities;

namespace TileIconifier.Custom
{
    internal class NewCustomShortcutFormCache
    {
        private byte[] _newIconBytes;
        private byte[] _currentIconBytes;

        private Image _iconCache;
        public string ShortcutName { get; set; }
        public ShortcutUser AllOrCurrentUser { get; set; }

        public void SetIconBytes(byte[] bytes)
        {
            _currentIconBytes = _currentIconBytes ?? bytes;
            _newIconBytes = bytes;
        }

        public Image GetIcon()
        {
            var iconBytesChanged = _currentIconBytes != null && _newIconBytes != null &&
                                   !_newIconBytes.SequenceEqual(_currentIconBytes);
            if (_iconCache == null || iconBytesChanged)
            {
                _currentIconBytes = _newIconBytes?.ToArray();
                _iconCache?.Dispose();
                _iconCache = ImageUtils.ByteArrayToImage(_currentIconBytes);
            }
            return _iconCache;
        }
    }
}