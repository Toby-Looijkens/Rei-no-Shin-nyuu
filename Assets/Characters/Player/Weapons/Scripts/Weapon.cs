using UnityEngine;

public class Weapon : MonoBehaviour
{
    public void AnimationEvent()
    {
        SendMessageUpwards("UnlockLockRotation");
    }
}
