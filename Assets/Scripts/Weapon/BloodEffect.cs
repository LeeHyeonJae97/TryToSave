using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public float duration;

    private void OnEnable() => Invoke(nameof(Return), duration);

    private void Return() => PoolingManager.instance.Return(gameObject);
}
