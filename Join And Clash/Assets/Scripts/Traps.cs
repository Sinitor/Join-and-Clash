using UnityEngine;
using DG.Tweening;

public class Traps : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveY(-2f, Random.Range(1f, 1.4f)).SetLoops(100000, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
