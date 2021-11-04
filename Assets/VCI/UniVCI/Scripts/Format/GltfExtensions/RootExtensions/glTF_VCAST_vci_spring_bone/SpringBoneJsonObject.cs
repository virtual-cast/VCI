using System;
using System.Collections.Generic;
using UnityEngine;
using UniGLTF;

namespace VCI
{
    [Serializable]
    [UniGLTF.JsonSchema(Title = "vci.spring_bone.spring_bone")]
    public class SpringBoneJsonObject
    {
        [UniGLTF.JsonSchema(Description = "The resilience of the swaying object (the power of returning to the initial pose).")]
        public float stiffiness;

        [UniGLTF.JsonSchema(Description = "The strength of gravity.")]
        public float gravityPower;

        [UniGLTF.JsonSchema(Description = "The direction of gravity. Set (0, -1, 0) for simulating the gravity. Set (1, 0, 0) for simulating the wind.")]
        public Vector3 gravityDir;

        [UniGLTF.JsonSchema(Description = "The resistance (deceleration) of automatic animation.")]
        public float dragForce;

        // NOTE: This value denotes index but may contain -1 as a value.
        // When the value is -1, it means that center node is not specified.
        // This is a historical issue and a compromise for forward compatibility.
        [UniGLTF.JsonSchema(Description = @"The reference point of a swaying object can be set at any location except the origin. When implementing UI moving with warp, the parent node to move with warp can be specified if you don't want to make the object swaying with warp movement.")]
        public int center;

        [UniGLTF.JsonSchema(Description = "The radius of the sphere used for the collision detection with colliders.")]
        public float hitRadius;

        [UniGLTF.JsonSchema(Description = "Specify the node index of the root bone of the swaying object.")]
        [ItemJsonSchema(Minimum = 0)]
        public int[] bones = {};

        [UniGLTF.JsonSchema(Description = "Specify the index of the collider group for collisions with swaying objects.")]
        [ItemJsonSchema(Minimum = 0)]
        public int[] colliderIds = {};
    }
}
