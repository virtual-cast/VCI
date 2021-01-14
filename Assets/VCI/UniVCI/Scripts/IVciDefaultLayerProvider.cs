namespace VCI
{
    /// <summary>
    /// VCI をロードしたときにデフォルトで設定されるレイヤを提供する。
    /// 背景 VCI としてロードしたときは Location、アイテム VCI としてロードしたときは Item レイヤが設定される。
    /// </summary>
    public interface IVciDefaultLayerProvider
    {
        int Location { get; }
        int Item { get; }
    }
}
