namespace VCI
{
    public enum VciValidationErrorType
    {
        // Export menu
        GameObjectNotSelected = 100,
        MultipleSelection = 101,
        VCIObjectNotAttached = 102,
        OnlyVciObject = 103,

        // VCIObject
        FirstScriptNameNotValid = 200,
        NoScriptName = 201,
        ScriptNameConfliction = 202,
        InvalidCharacter = 203,
        InvalidMetaData = 204,
        MultipleVCIObject = 205,
        InvalidComponent = 206,
        NestedSubItem = 207,
        RootSubItem = 208,
        RootAudioSource = 209,

        // SpringBone
        TooManySpringBone = 400,
        RootBoneNotFound = 401,
        TooManyRootBone = 402,
        TooManyRootBoneChild = 403,
        RootBoneContainsSubItem = 404,
        RootBoneNested = 405,

        // SpringBoneCollider
        TooManySpringBoneCollider = 410,
        TooManySphereCollider = 411,

        // PlayerSpawnPoint
        SpawnPointNotHorizontal = 501,
        SpawnPointOriginNotInRange = 502,

        // LocationBounds
        LocationBoundsCountLimitOver = 601,
        LocationBoundsValueExceeded = 602,

        // Animation
        RootIsAnimated = 701,
        RotationInterpolationIsEulerAngles = 702,
        ContainsMissingBlendShape = 703,

        // Effekseer
        EffekseerMaterialIncluded = 801,

        // SubItem
        SubItemKeyUndefined = 901,
        SubItemKeyDuplicated = 902,

        // Collider
        NonConvexMeshColliderIsUnderRigidbody = 1001,

        // Audio
        RollOffModeNotSupported = 1101,
    }
}
