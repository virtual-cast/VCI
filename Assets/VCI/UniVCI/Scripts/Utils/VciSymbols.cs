namespace VCI
{
    /// <summary>
    /// コードの健全性のため #if VCI_DEVELOP の記述をこのクラスに閉じ込めたい
    /// </summary>
    public static class VciSymbols
    {
        public static bool IsDevelopmentEnabled
        {
            get
            {
#if VCI_DEVELOP
                return true;
#else
                return false;
#endif
            }
        }
    }
}
