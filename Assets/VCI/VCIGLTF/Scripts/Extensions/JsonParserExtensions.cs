using System.Linq;
using UnityEngine;
using System.Collections.Generic;


namespace VCIJSON
{
    public static class JsonParserExtensions
    {
        public static List<T> DeserializeList<T>(this ListTreeNode<JsonValue> jsonList)
        {
            return jsonList.ArrayItems().Select(x => {

                return JsonUtility.FromJson<T>(new Utf8String(x.Value.Bytes).ToString());
                
            }).ToList();
        }
    }
}
