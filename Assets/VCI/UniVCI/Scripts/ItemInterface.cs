#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VCI
{
    public interface IItemEventObserver
    {
        void Update();
        void OnCollisionEntered(Collider obj);
        void OnCollisionExited(Collider obj);
        void OnAction();
    }

    public interface IUnityCall
    {
        string Name { get; }

        void destroy();
        void setPosition(Vector3 position);
        Vector3 getPosition();
        Vector3 getForward();
        void scale(float n);
        void lookAt(GameObject target);
        void lookAtHorizontal(GameObject target);

        void PlaySound(int index);
        void PlayAnimation(int index);
        void StopAnimation(int index);
        void SetMaterialColor(int index, Vector4 color);
    }
}
