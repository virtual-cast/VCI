
using System;
using VCIJSON;
using VCIGLTF;
using System.Collections.Generic;


namespace VCI {
    public static partial class VCIAOTCall {
        static void glTF()
        {       
            {       
                var f = new JsonFormatter();


// String
f.Serialize(default(System.String));
{
var value = default(System.String);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}

// Boolean
f.Serialize(default(System.Boolean));
{
var value = default(System.Boolean);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}

// Byte
f.Serialize(default(System.Byte));
{
var value = default(System.Byte);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}

// UInt16
f.Serialize(default(System.UInt16));
{
var value = default(System.UInt16);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}

// UInt32
f.Serialize(default(System.UInt32));
{
var value = default(System.UInt32);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}

// UInt64
f.Serialize(default(System.UInt64));
{
var value = default(System.UInt64);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}

// SByte
f.Serialize(default(System.SByte));
{
var value = default(System.SByte);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}

// Int16
f.Serialize(default(System.Int16));
{
var value = default(System.Int16);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}

// Int32
f.Serialize(default(System.Int32));
{
var value = default(System.Int32);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}

// Int64
f.Serialize(default(System.Int64));
{
var value = default(System.Int64);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}

// Single
f.Serialize(default(System.Single));
{
var value = default(System.Single);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}

// Double
f.Serialize(default(System.Double));
{
var value = default(System.Double);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}

// Vector2
f.Serialize(default(UnityEngine.Vector2));
{
var value = default(UnityEngine.Vector2);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Vector2>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Vector2>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// Vector3
f.Serialize(default(UnityEngine.Vector3));
{
var value = default(UnityEngine.Vector3);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Vector3>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Vector3>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Vector3>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// Vector4
f.Serialize(default(UnityEngine.Vector4));
{
var value = default(UnityEngine.Vector4);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Vector4>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Vector4>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Vector4>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Vector4>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// Color
f.Serialize(default(UnityEngine.Color));
{
var value = default(UnityEngine.Color);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Color>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Color>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Color>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Color>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// Quaternion
f.Serialize(default(UnityEngine.Quaternion));
{
var value = default(UnityEngine.Quaternion);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Quaternion>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Quaternion>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Quaternion>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,UnityEngine.Quaternion>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTF
f.Serialize(default(VCIGLTF.glTF));
{
var value = default(VCIGLTF.glTF);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<VCIGLTF.glTFAssets>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFAssets
f.Serialize(default(VCIGLTF.glTFAssets));
{
var value = default(VCIGLTF.glTFAssets);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAssets>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAssets>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAssets>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAssets>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAssets>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAssets>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<glTFBuffer>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFBuffer>));
{
var value = default(List<VCIGLTF.glTFBuffer>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFBuffer>>.GenericListDeserializer<VCIGLTF.glTFBuffer>(default(ListTreeNode<JsonValue>));
}

// glTFBuffer
f.Serialize(default(VCIGLTF.glTFBuffer));
{
var value = default(VCIGLTF.glTFBuffer);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFBuffer>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFBuffer>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFBuffer>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFBuffer>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFBuffer>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<glTFBufferView>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFBufferView>));
{
var value = default(List<VCIGLTF.glTFBufferView>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFBufferView>>.GenericListDeserializer<VCIGLTF.glTFBufferView>(default(ListTreeNode<JsonValue>));
}

// glTFBufferView
f.Serialize(default(VCIGLTF.glTFBufferView));
{
var value = default(VCIGLTF.glTFBufferView);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFBufferView>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFBufferView>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFBufferView>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFBufferView>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFBufferView>.DeserializeField<VCIGLTF.glBufferTarget>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glBufferTarget
f.Serialize(default(VCIGLTF.glBufferTarget));
{
var value = default(VCIGLTF.glBufferTarget);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glBufferTarget>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFBufferView>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFBufferView>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFBufferView>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<glTFAccessor>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFAccessor>));
{
var value = default(List<VCIGLTF.glTFAccessor>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFAccessor>>.GenericListDeserializer<VCIGLTF.glTFAccessor>(default(ListTreeNode<JsonValue>));
}

// glTFAccessor
f.Serialize(default(VCIGLTF.glTFAccessor));
{
var value = default(VCIGLTF.glTFAccessor);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAccessor>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAccessor>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAccessor>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAccessor>.DeserializeField<VCIGLTF.glComponentType>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glComponentType
f.Serialize(default(VCIGLTF.glComponentType));
{
var value = default(VCIGLTF.glComponentType);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glComponentType>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAccessor>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAccessor>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// Single[]
f.Serialize(default(Single[]));
{
var value = default(Single[]);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, Single[]>.GenericArrayDeserializer<Single>(default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAccessor>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAccessor>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAccessor>.DeserializeField<VCIGLTF.glTFSparse>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFSparse
f.Serialize(default(VCIGLTF.glTFSparse));
{
var value = default(VCIGLTF.glTFSparse);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparse>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparse>.DeserializeField<VCIGLTF.glTFSparseIndices>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFSparseIndices
f.Serialize(default(VCIGLTF.glTFSparseIndices));
{
var value = default(VCIGLTF.glTFSparseIndices);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparseIndices>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparseIndices>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparseIndices>.DeserializeField<VCIGLTF.glComponentType>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparseIndices>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparseIndices>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparse>.DeserializeField<VCIGLTF.glTFSparseValues>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFSparseValues
f.Serialize(default(VCIGLTF.glTFSparseValues));
{
var value = default(VCIGLTF.glTFSparseValues);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparseValues>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparseValues>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparseValues>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparseValues>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparse>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSparse>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAccessor>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAccessor>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAccessor>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<glTFTexture>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFTexture>));
{
var value = default(List<VCIGLTF.glTFTexture>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFTexture>>.GenericListDeserializer<VCIGLTF.glTFTexture>(default(ListTreeNode<JsonValue>));
}

// glTFTexture
f.Serialize(default(VCIGLTF.glTFTexture));
{
var value = default(VCIGLTF.glTFTexture);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFTexture>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFTexture>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFTexture>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFTexture>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFTexture>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<glTFTextureSampler>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFTextureSampler>));
{
var value = default(List<VCIGLTF.glTFTextureSampler>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFTextureSampler>>.GenericListDeserializer<VCIGLTF.glTFTextureSampler>(default(ListTreeNode<JsonValue>));
}

// glTFTextureSampler
f.Serialize(default(VCIGLTF.glTFTextureSampler));
{
var value = default(VCIGLTF.glTFTextureSampler);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFTextureSampler>.DeserializeField<VCIGLTF.glFilter>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glFilter
f.Serialize(default(VCIGLTF.glFilter));
{
var value = default(VCIGLTF.glFilter);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glFilter>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFTextureSampler>.DeserializeField<VCIGLTF.glFilter>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFTextureSampler>.DeserializeField<VCIGLTF.glWrap>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glWrap
f.Serialize(default(VCIGLTF.glWrap));
{
var value = default(VCIGLTF.glWrap);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glWrap>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFTextureSampler>.DeserializeField<VCIGLTF.glWrap>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFTextureSampler>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFTextureSampler>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFTextureSampler>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<glTFImage>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFImage>));
{
var value = default(List<VCIGLTF.glTFImage>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFImage>>.GenericListDeserializer<VCIGLTF.glTFImage>(default(ListTreeNode<JsonValue>));
}

// glTFImage
f.Serialize(default(VCIGLTF.glTFImage));
{
var value = default(VCIGLTF.glTFImage);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFImage>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFImage>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFImage>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFImage>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFImage>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFImage>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<glTFMaterial>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFMaterial>));
{
var value = default(List<VCIGLTF.glTFMaterial>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFMaterial>>.GenericListDeserializer<VCIGLTF.glTFMaterial>(default(ListTreeNode<JsonValue>));
}

// glTFMaterial
f.Serialize(default(VCIGLTF.glTFMaterial));
{
var value = default(VCIGLTF.glTFMaterial);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterial>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterial>.DeserializeField<VCIGLTF.glTFPbrMetallicRoughness>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFPbrMetallicRoughness
f.Serialize(default(VCIGLTF.glTFPbrMetallicRoughness));
{
var value = default(VCIGLTF.glTFPbrMetallicRoughness);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPbrMetallicRoughness>.DeserializeField<VCIGLTF.glTFMaterialBaseColorTextureInfo>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFMaterialBaseColorTextureInfo
f.Serialize(default(VCIGLTF.glTFMaterialBaseColorTextureInfo));
{
var value = default(VCIGLTF.glTFMaterialBaseColorTextureInfo);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialBaseColorTextureInfo>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialBaseColorTextureInfo>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialBaseColorTextureInfo>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialBaseColorTextureInfo>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPbrMetallicRoughness>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPbrMetallicRoughness>.DeserializeField<VCIGLTF.glTFMaterialMetallicRoughnessTextureInfo>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFMaterialMetallicRoughnessTextureInfo
f.Serialize(default(VCIGLTF.glTFMaterialMetallicRoughnessTextureInfo));
{
var value = default(VCIGLTF.glTFMaterialMetallicRoughnessTextureInfo);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialMetallicRoughnessTextureInfo>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialMetallicRoughnessTextureInfo>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialMetallicRoughnessTextureInfo>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialMetallicRoughnessTextureInfo>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPbrMetallicRoughness>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPbrMetallicRoughness>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPbrMetallicRoughness>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPbrMetallicRoughness>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterial>.DeserializeField<VCIGLTF.glTFMaterialNormalTextureInfo>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFMaterialNormalTextureInfo
f.Serialize(default(VCIGLTF.glTFMaterialNormalTextureInfo));
{
var value = default(VCIGLTF.glTFMaterialNormalTextureInfo);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialNormalTextureInfo>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialNormalTextureInfo>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialNormalTextureInfo>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialNormalTextureInfo>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialNormalTextureInfo>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterial>.DeserializeField<VCIGLTF.glTFMaterialOcclusionTextureInfo>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFMaterialOcclusionTextureInfo
f.Serialize(default(VCIGLTF.glTFMaterialOcclusionTextureInfo));
{
var value = default(VCIGLTF.glTFMaterialOcclusionTextureInfo);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialOcclusionTextureInfo>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialOcclusionTextureInfo>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialOcclusionTextureInfo>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialOcclusionTextureInfo>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialOcclusionTextureInfo>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterial>.DeserializeField<VCIGLTF.glTFMaterialEmissiveTextureInfo>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFMaterialEmissiveTextureInfo
f.Serialize(default(VCIGLTF.glTFMaterialEmissiveTextureInfo));
{
var value = default(VCIGLTF.glTFMaterialEmissiveTextureInfo);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialEmissiveTextureInfo>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialEmissiveTextureInfo>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialEmissiveTextureInfo>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterialEmissiveTextureInfo>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterial>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterial>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterial>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterial>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterial>.DeserializeField<VCIGLTF.glTFMaterial_extensions>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFMaterial_extensions
f.Serialize(default(VCIGLTF.glTFMaterial_extensions));
{
var value = default(VCIGLTF.glTFMaterial_extensions);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterial_extensions>.DeserializeField<VCIGLTF.glTF_KHR_materials_unlit>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTF_KHR_materials_unlit
f.Serialize(default(VCIGLTF.glTF_KHR_materials_unlit));
{
var value = default(VCIGLTF.glTF_KHR_materials_unlit);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMaterial>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<glTFMesh>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFMesh>));
{
var value = default(List<VCIGLTF.glTFMesh>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFMesh>>.GenericListDeserializer<VCIGLTF.glTFMesh>(default(ListTreeNode<JsonValue>));
}

// glTFMesh
f.Serialize(default(VCIGLTF.glTFMesh));
{
var value = default(VCIGLTF.glTFMesh);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMesh>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMesh>.DeserializeField<System.Collections.Generic.List<glTFPrimitives>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFPrimitives>));
{
var value = default(List<VCIGLTF.glTFPrimitives>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFPrimitives>>.GenericListDeserializer<VCIGLTF.glTFPrimitives>(default(ListTreeNode<JsonValue>));
}

// glTFPrimitives
f.Serialize(default(VCIGLTF.glTFPrimitives));
{
var value = default(VCIGLTF.glTFPrimitives);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPrimitives>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPrimitives>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPrimitives>.DeserializeField<VCIGLTF.glTFAttributes>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFAttributes
f.Serialize(default(VCIGLTF.glTFAttributes));
{
var value = default(VCIGLTF.glTFAttributes);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAttributes>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAttributes>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAttributes>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAttributes>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAttributes>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAttributes>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAttributes>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPrimitives>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPrimitives>.DeserializeField<System.Collections.Generic.List<gltfMorphTarget>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.gltfMorphTarget>));
{
var value = default(List<VCIGLTF.gltfMorphTarget>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.gltfMorphTarget>>.GenericListDeserializer<VCIGLTF.gltfMorphTarget>(default(ListTreeNode<JsonValue>));
}

// gltfMorphTarget
f.Serialize(default(VCIGLTF.gltfMorphTarget));
{
var value = default(VCIGLTF.gltfMorphTarget);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.gltfMorphTarget>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.gltfMorphTarget>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.gltfMorphTarget>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPrimitives>.DeserializeField<VCIGLTF.glTFPrimitives_extras>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFPrimitives_extras
f.Serialize(default(VCIGLTF.glTFPrimitives_extras));
{
var value = default(VCIGLTF.glTFPrimitives_extras);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPrimitives_extras>.DeserializeField<System.Collections.Generic.List<String>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<System.String>));
{
var value = default(List<System.String>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<System.String>>.GenericListDeserializer<System.String>(default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPrimitives>.DeserializeField<VCIGLTF.glTFPrimitives_extensions>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFPrimitives_extensions
f.Serialize(default(VCIGLTF.glTFPrimitives_extensions));
{
var value = default(VCIGLTF.glTFPrimitives_extensions);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMesh>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMesh>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFMesh>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<glTFNode>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFNode>));
{
var value = default(List<VCIGLTF.glTFNode>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFNode>>.GenericListDeserializer<VCIGLTF.glTFNode>(default(ListTreeNode<JsonValue>));
}

// glTFNode
f.Serialize(default(VCIGLTF.glTFNode));
{
var value = default(VCIGLTF.glTFNode);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode>.DeserializeField<System.Int32[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// Int32[]
f.Serialize(default(Int32[]));
{
var value = default(Int32[]);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, Int32[]>.GenericArrayDeserializer<Int32>(default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode>.DeserializeField<VCIGLTF.glTFNode_extensions>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFNode_extensions
f.Serialize(default(VCIGLTF.glTFNode_extensions));
{
var value = default(VCIGLTF.glTFNode_extensions);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode_extensions>.DeserializeField<VCIGLTF.glTF_VCAST_vci_colliders>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_colliders
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_colliders));
{
var value = default(VCIGLTF.glTF_VCAST_vci_colliders);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_colliders>.DeserializeField<System.Collections.Generic.List<glTF_VCAST_vci_Collider>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTF_VCAST_vci_Collider>));
{
var value = default(List<VCIGLTF.glTF_VCAST_vci_Collider>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTF_VCAST_vci_Collider>>.GenericListDeserializer<VCIGLTF.glTF_VCAST_vci_Collider>(default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_Collider
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_Collider));
{
var value = default(VCIGLTF.glTF_VCAST_vci_Collider);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Collider>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Collider>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Collider>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Collider>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Collider>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Collider>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Collider>.DeserializeField<VCIGLTF.glTF_VCAST_vci_PhysicMaterial>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_PhysicMaterial
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_PhysicMaterial));
{
var value = default(VCIGLTF.glTF_VCAST_vci_PhysicMaterial);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_PhysicMaterial>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_PhysicMaterial>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_PhysicMaterial>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_PhysicMaterial>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_PhysicMaterial>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode_extensions>.DeserializeField<VCIGLTF.glTF_VCAST_vci_rigidbody>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_rigidbody
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_rigidbody));
{
var value = default(VCIGLTF.glTF_VCAST_vci_rigidbody);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_rigidbody>.DeserializeField<System.Collections.Generic.List<glTF_VCAST_vci_Rigidbody>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTF_VCAST_vci_Rigidbody>));
{
var value = default(List<VCIGLTF.glTF_VCAST_vci_Rigidbody>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTF_VCAST_vci_Rigidbody>>.GenericListDeserializer<VCIGLTF.glTF_VCAST_vci_Rigidbody>(default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_Rigidbody
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_Rigidbody));
{
var value = default(VCIGLTF.glTF_VCAST_vci_Rigidbody);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Rigidbody>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Rigidbody>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Rigidbody>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Rigidbody>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Rigidbody>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Rigidbody>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Rigidbody>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Rigidbody>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Rigidbody>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Rigidbody>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Rigidbody>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Rigidbody>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_Rigidbody>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode_extensions>.DeserializeField<VCIGLTF.glTF_VCAST_vci_joints>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_joints
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_joints));
{
var value = default(VCIGLTF.glTF_VCAST_vci_joints);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joints>.DeserializeField<System.Collections.Generic.List<glTF_VCAST_vci_joint>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTF_VCAST_vci_joint>));
{
var value = default(List<VCIGLTF.glTF_VCAST_vci_joint>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTF_VCAST_vci_joint>>.GenericListDeserializer<VCIGLTF.glTF_VCAST_vci_joint>(default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_joint
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_joint));
{
var value = default(VCIGLTF.glTF_VCAST_vci_joint);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<System.Single[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<VCIGLTF.glTF_VCAST_vci_joint.Spring>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// Spring
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_joint.Spring));
{
var value = default(VCIGLTF.glTF_VCAST_vci_joint.Spring);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint.Spring>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint.Spring>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint.Spring>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint.Spring>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint.Spring>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint.Spring>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint>.DeserializeField<VCIGLTF.glTF_VCAST_vci_joint.Limits>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// Limits
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_joint.Limits));
{
var value = default(VCIGLTF.glTF_VCAST_vci_joint.Limits);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint.Limits>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint.Limits>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint.Limits>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint.Limits>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_joint.Limits>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode_extensions>.DeserializeField<VCIGLTF.glTF_VCAST_vci_item>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_item
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_item));
{
var value = default(VCIGLTF.glTF_VCAST_vci_item);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_item>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_item>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_item>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_item>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFNode>.DeserializeField<VCIGLTF.glTFNode_extra>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFNode_extra
f.Serialize(default(VCIGLTF.glTFNode_extra));
{
var value = default(VCIGLTF.glTFNode_extra);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<glTFSkin>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFSkin>));
{
var value = default(List<VCIGLTF.glTFSkin>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFSkin>>.GenericListDeserializer<VCIGLTF.glTFSkin>(default(ListTreeNode<JsonValue>));
}

// glTFSkin
f.Serialize(default(VCIGLTF.glTFSkin));
{
var value = default(VCIGLTF.glTFSkin);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSkin>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSkin>.DeserializeField<System.Int32[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSkin>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSkin>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSkin>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFSkin>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<gltfScene>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.gltfScene>));
{
var value = default(List<VCIGLTF.gltfScene>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.gltfScene>>.GenericListDeserializer<VCIGLTF.gltfScene>(default(ListTreeNode<JsonValue>));
}

// gltfScene
f.Serialize(default(VCIGLTF.gltfScene));
{
var value = default(VCIGLTF.gltfScene);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.gltfScene>.DeserializeField<System.Int32[]>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.gltfScene>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.gltfScene>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.gltfScene>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<glTFAnimation>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFAnimation>));
{
var value = default(List<VCIGLTF.glTFAnimation>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFAnimation>>.GenericListDeserializer<VCIGLTF.glTFAnimation>(default(ListTreeNode<JsonValue>));
}

// glTFAnimation
f.Serialize(default(VCIGLTF.glTFAnimation));
{
var value = default(VCIGLTF.glTFAnimation);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimation>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimation>.DeserializeField<System.Collections.Generic.List<glTFAnimationChannel>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFAnimationChannel>));
{
var value = default(List<VCIGLTF.glTFAnimationChannel>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFAnimationChannel>>.GenericListDeserializer<VCIGLTF.glTFAnimationChannel>(default(ListTreeNode<JsonValue>));
}

// glTFAnimationChannel
f.Serialize(default(VCIGLTF.glTFAnimationChannel));
{
var value = default(VCIGLTF.glTFAnimationChannel);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimationChannel>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimationChannel>.DeserializeField<VCIGLTF.glTFAnimationTarget>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFAnimationTarget
f.Serialize(default(VCIGLTF.glTFAnimationTarget));
{
var value = default(VCIGLTF.glTFAnimationTarget);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimationTarget>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimationTarget>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimationTarget>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimationTarget>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimationChannel>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimationChannel>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimation>.DeserializeField<System.Collections.Generic.List<glTFAnimationSampler>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFAnimationSampler>));
{
var value = default(List<VCIGLTF.glTFAnimationSampler>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFAnimationSampler>>.GenericListDeserializer<VCIGLTF.glTFAnimationSampler>(default(ListTreeNode<JsonValue>));
}

// glTFAnimationSampler
f.Serialize(default(VCIGLTF.glTFAnimationSampler));
{
var value = default(VCIGLTF.glTFAnimationSampler);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimationSampler>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimationSampler>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimationSampler>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimationSampler>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimationSampler>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimation>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFAnimation>.DeserializeField<System.Object>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<glTFCamera>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTFCamera>));
{
var value = default(List<VCIGLTF.glTFCamera>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTFCamera>>.GenericListDeserializer<VCIGLTF.glTFCamera>(default(ListTreeNode<JsonValue>));
}

// glTFCamera
f.Serialize(default(VCIGLTF.glTFCamera));
{
var value = default(VCIGLTF.glTFCamera);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFCamera>.DeserializeField<VCIGLTF.glTFOrthographic>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFOrthographic
f.Serialize(default(VCIGLTF.glTFOrthographic));
{
var value = default(VCIGLTF.glTFOrthographic);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFOrthographic>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFOrthographic>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFOrthographic>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFOrthographic>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFOrthographic>.DeserializeField<VCIGLTF.glTFOrthographic_extensions>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFOrthographic_extensions
f.Serialize(default(VCIGLTF.glTFOrthographic_extensions));
{
var value = default(VCIGLTF.glTFOrthographic_extensions);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFOrthographic>.DeserializeField<VCIGLTF.glTFOrthographic_extras>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFOrthographic_extras
f.Serialize(default(VCIGLTF.glTFOrthographic_extras));
{
var value = default(VCIGLTF.glTFOrthographic_extras);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFCamera>.DeserializeField<VCIGLTF.glTFPerspective>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFPerspective
f.Serialize(default(VCIGLTF.glTFPerspective));
{
var value = default(VCIGLTF.glTFPerspective);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPerspective>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPerspective>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPerspective>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPerspective>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPerspective>.DeserializeField<VCIGLTF.glTFPerspective_extensions>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFPerspective_extensions
f.Serialize(default(VCIGLTF.glTFPerspective_extensions));
{
var value = default(VCIGLTF.glTFPerspective_extensions);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFPerspective>.DeserializeField<VCIGLTF.glTFPerspective_extras>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFPerspective_extras
f.Serialize(default(VCIGLTF.glTFPerspective_extras));
{
var value = default(VCIGLTF.glTFPerspective_extras);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFCamera>.DeserializeField<VCIGLTF.ProjectionType>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// ProjectionType
f.Serialize(default(VCIGLTF.ProjectionType));
{
var value = default(VCIGLTF.ProjectionType);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.ProjectionType>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFCamera>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFCamera>.DeserializeField<VCIGLTF.glTFCamera_extensions>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFCamera_extensions
f.Serialize(default(VCIGLTF.glTFCamera_extensions));
{
var value = default(VCIGLTF.glTFCamera_extensions);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTFCamera>.DeserializeField<VCIGLTF.glTFCamera_extras>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTFCamera_extras
f.Serialize(default(VCIGLTF.glTFCamera_extras));
{
var value = default(VCIGLTF.glTFCamera_extras);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<String>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<System.Collections.Generic.List<String>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<VCIGLTF.glTF_extensions>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTF_extensions
f.Serialize(default(VCIGLTF.glTF_extensions));
{
var value = default(VCIGLTF.glTF_extensions);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_extensions>.DeserializeField<VCIGLTF.glTF_VCAST_vci_meta>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_meta
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_meta));
{
var value = default(VCIGLTF.glTF_VCAST_vci_meta);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<VCIGLTF.glTF_VCAST_vci_meta.LicenseType>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// LicenseType
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_meta.LicenseType));
{
var value = default(VCIGLTF.glTF_VCAST_vci_meta.LicenseType);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta.LicenseType>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<VCIGLTF.glTF_VCAST_vci_meta.LicenseType>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<System.Boolean>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta>.DeserializeField<VCIGLTF.glTF_VCAST_vci_meta.ScriptFormat>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// ScriptFormat
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_meta.ScriptFormat));
{
var value = default(VCIGLTF.glTF_VCAST_vci_meta.ScriptFormat);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_meta.ScriptFormat>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_extensions>.DeserializeField<VCIGLTF.glTF_VCAST_vci_audios>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_audios
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_audios));
{
var value = default(VCIGLTF.glTF_VCAST_vci_audios);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_audios>.DeserializeField<System.Collections.Generic.List<glTF_VCAST_vci_audio>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTF_VCAST_vci_audio>));
{
var value = default(List<VCIGLTF.glTF_VCAST_vci_audio>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTF_VCAST_vci_audio>>.GenericListDeserializer<VCIGLTF.glTF_VCAST_vci_audio>(default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_audio
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_audio));
{
var value = default(VCIGLTF.glTF_VCAST_vci_audio);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_audio>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_audio>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_audio>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_extensions>.DeserializeField<VCIGLTF.glTF_VCAST_vci_embedded_script>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_embedded_script
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_embedded_script));
{
var value = default(VCIGLTF.glTF_VCAST_vci_embedded_script);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_embedded_script>.DeserializeField<System.Collections.Generic.List<glTF_VCAST_vci_embedded_script_source>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCIGLTF.glTF_VCAST_vci_embedded_script_source>));
{
var value = default(List<VCIGLTF.glTF_VCAST_vci_embedded_script_source>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCIGLTF.glTF_VCAST_vci_embedded_script_source>>.GenericListDeserializer<VCIGLTF.glTF_VCAST_vci_embedded_script_source>(default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_embedded_script_source
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_embedded_script_source));
{
var value = default(VCIGLTF.glTF_VCAST_vci_embedded_script_source);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_embedded_script_source>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_embedded_script_source>.DeserializeField<VCIGLTF.ScriptMimeType>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// ScriptMimeType
f.Serialize(default(VCIGLTF.ScriptMimeType));
{
var value = default(VCIGLTF.ScriptMimeType);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.ScriptMimeType>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_embedded_script_source>.DeserializeField<VCIGLTF.TargetEngine>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// TargetEngine
f.Serialize(default(VCIGLTF.TargetEngine));
{
var value = default(VCIGLTF.TargetEngine);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.TargetEngine>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_embedded_script_source>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_embedded_script>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_extensions>.DeserializeField<VCIGLTF.glTF_VCAST_vci_material_unity>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// glTF_VCAST_vci_material_unity
f.Serialize(default(VCIGLTF.glTF_VCAST_vci_material_unity));
{
var value = default(VCIGLTF.glTF_VCAST_vci_material_unity);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF_VCAST_vci_material_unity>.DeserializeField<System.Collections.Generic.List<glTF_VCI_Material>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// List`1
f.Serialize(default(List<VCI.glTF_VCI_Material>));
{
var value = default(List<VCI.glTF_VCI_Material>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, List<VCI.glTF_VCI_Material>>.GenericListDeserializer<VCI.glTF_VCI_Material>(default(ListTreeNode<JsonValue>));
}

// glTF_VCI_Material
f.Serialize(default(VCI.glTF_VCI_Material));
{
var value = default(VCI.glTF_VCI_Material);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCI.glTF_VCI_Material>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCI.glTF_VCI_Material>.DeserializeField<System.String>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCI.glTF_VCI_Material>.DeserializeField<System.Int32>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCI.glTF_VCI_Material>.DeserializeField<System.Collections.Generic.Dictionary<String,Single>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// Dictionary`2
f.Serialize(default(Dictionary<string, System.Single>));
{
var value = default(Dictionary<string, System.Single>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, Dictionary<string, System.Single>>.DictionaryDeserializer<System.Single>(default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCI.glTF_VCI_Material>.DeserializeField<System.Collections.Generic.Dictionary<String,Single[]>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// Dictionary`2
f.Serialize(default(Dictionary<string, System.Single[]>));
{
var value = default(Dictionary<string, System.Single[]>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, Dictionary<string, System.Single[]>>.DictionaryDeserializer<System.Single[]>(default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCI.glTF_VCI_Material>.DeserializeField<System.Collections.Generic.Dictionary<String,Int32>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// Dictionary`2
f.Serialize(default(Dictionary<string, System.Int32>));
{
var value = default(Dictionary<string, System.Int32>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, Dictionary<string, System.Int32>>.DictionaryDeserializer<System.Int32>(default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCI.glTF_VCI_Material>.DeserializeField<System.Collections.Generic.Dictionary<String,Boolean>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// Dictionary`2
f.Serialize(default(Dictionary<string, System.Boolean>));
{
var value = default(Dictionary<string, System.Boolean>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, Dictionary<string, System.Boolean>>.DictionaryDeserializer<System.Boolean>(default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCI.glTF_VCI_Material>.DeserializeField<System.Collections.Generic.Dictionary<String,String>>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// Dictionary`2
f.Serialize(default(Dictionary<string, System.String>));
{
var value = default(Dictionary<string, System.String>);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
GenericDeserializer<JsonValue, Dictionary<string, System.String>>.DictionaryDeserializer<System.String>(default(ListTreeNode<JsonValue>));
}
{
JsonObjectValidator.GenericDeserializer<JsonValue,VCIGLTF.glTF>.DeserializeField<VCIGLTF.gltf_extras>(default(JsonSchema), default(ListTreeNode<JsonValue>));
}

// gltf_extras
f.Serialize(default(VCIGLTF.gltf_extras));
{
var value = default(VCIGLTF.gltf_extras);
default(ListTreeNode<JsonValue>).Deserialize(ref value);
}
}

{
                var f = new MsgPackFormatter();


// String
f.Serialize(default(System.String));
{
var value = default(System.String);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}

// Boolean
f.Serialize(default(System.Boolean));
{
var value = default(System.Boolean);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}

// Byte
f.Serialize(default(System.Byte));
{
var value = default(System.Byte);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}

// UInt16
f.Serialize(default(System.UInt16));
{
var value = default(System.UInt16);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}

// UInt32
f.Serialize(default(System.UInt32));
{
var value = default(System.UInt32);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}

// UInt64
f.Serialize(default(System.UInt64));
{
var value = default(System.UInt64);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}

// SByte
f.Serialize(default(System.SByte));
{
var value = default(System.SByte);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}

// Int16
f.Serialize(default(System.Int16));
{
var value = default(System.Int16);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}

// Int32
f.Serialize(default(System.Int32));
{
var value = default(System.Int32);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}

// Int64
f.Serialize(default(System.Int64));
{
var value = default(System.Int64);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}

// Single
f.Serialize(default(System.Single));
{
var value = default(System.Single);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}

// Double
f.Serialize(default(System.Double));
{
var value = default(System.Double);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}

// Vector2
f.Serialize(default(UnityEngine.Vector2));
{
var value = default(UnityEngine.Vector2);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Vector2>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Vector2>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}

// Vector3
f.Serialize(default(UnityEngine.Vector3));
{
var value = default(UnityEngine.Vector3);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Vector3>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Vector3>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Vector3>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}

// Vector4
f.Serialize(default(UnityEngine.Vector4));
{
var value = default(UnityEngine.Vector4);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Vector4>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Vector4>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Vector4>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Vector4>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}

// Color
f.Serialize(default(UnityEngine.Color));
{
var value = default(UnityEngine.Color);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Color>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Color>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Color>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Color>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}

// Quaternion
f.Serialize(default(UnityEngine.Quaternion));
{
var value = default(UnityEngine.Quaternion);
default(ListTreeNode<MsgPackValue>).Deserialize(ref value);
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Quaternion>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Quaternion>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Quaternion>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}
{
JsonObjectValidator.GenericDeserializer<MsgPackValue,UnityEngine.Quaternion>.DeserializeField<System.Single>(default(JsonSchema), default(ListTreeNode<MsgPackValue>));
}

            }
        }
    }
}

