using TileIconifier.Skinning.Skins;

namespace TileIconifier.Skinning
{
    public static class SkinHandler
    {
        public delegate void SkinChangedEventArgs();

        private static BaseSkin _currentBaseSkin = new BaseSkin();

        public static event SkinChangedEventArgs SkinChanged;

        public static BaseSkin GetCurrentSkin()
        {
            return _currentBaseSkin;
        }

        public static void SetCurrentSkin(BaseSkin baseSkin)
        {
            _currentBaseSkin = baseSkin;
            SkinChanged?.Invoke();
        }
    }
}