using System;
using UnityEngine;

namespace VCI
{
    // パッケージの作者
    // TODO: Nameがない場合Exceptionを投げる
    [Serializable]
    internal sealed class Author
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private string _email;

        [SerializeField]
        private string _url;

        public string Name => _name;
        public string Email => _email;
        public string Url => _url;

        public Author(string name, string email, string url)
        {
            _name = name;
            _email = email;
            _url = url;
        }
    }
}