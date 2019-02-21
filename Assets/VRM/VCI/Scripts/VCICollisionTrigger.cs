using UnityEngine;


namespace VCI
{
    public enum EnterStatus
    {
        Enter,
        Stay,
        Exit,
    }

    public struct VCICollisionTrigger
    {
        public EnterStatus Status;
        public string Item; // self
        public string Hit; // hit
        #region Trigger
        public bool IsTrigger;
        #endregion
        #region Collision
        public ContactPoint[] Contacts;
        public Vector3 Impulse;
        #endregion

        public static VCICollisionTrigger Create(VCISubItem item, EnterStatus status, Collision c)
        {
            return new VCICollisionTrigger
            {
                Status = status,
                Item = item.name,
                Hit = c.gameObject.name,
                Contacts = c.contacts,
                Impulse = c.impulse,
            };
        }

        public static VCICollisionTrigger Create(VCISubItem item, EnterStatus status, Collider c)
        {
            return new VCICollisionTrigger
            {
                Status = status,
                Item = item.name,
                Hit = c.name,
                IsTrigger = true,
            };
        }
    }
}