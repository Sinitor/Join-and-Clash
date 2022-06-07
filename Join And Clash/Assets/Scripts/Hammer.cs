using UnityEngine;
using DG.Tweening;

public class Hammer : MonoBehaviour
{
    [SerializeField] private bool leftRight;
    private void Start()
    {
        if (leftRight)
        {
            transform.DORotate(new Vector3(0, 90, 0), Random.Range(1f, 1.4f)).SetLoops(100000, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
        else
        {
            transform.DORotate(new Vector3(-180, 90, 0), Random.Range(1f, 1.4f)).SetLoops(100000, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }
}
