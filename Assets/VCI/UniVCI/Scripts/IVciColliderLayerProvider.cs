namespace VCI
{
    /// <summary>
    /// VCI のコライダレイヤ設定を提供する。
    /// VCI の GameObject のうち Collider がついたもののレイヤ設定に関与する。
    /// </summary>
    public interface IVciColliderLayerProvider
    {
        int Default { get; }
        int Location { get; }
        int PickUp { get; }
        int Accessory { get; }
        int Item { get; }
    }
}
