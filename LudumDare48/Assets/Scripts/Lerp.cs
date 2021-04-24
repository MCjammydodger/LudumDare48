using UnityEngine;
using UnityEngine.Events;

public class Lerp : MonoBehaviour
{
    public struct LerpTo
    {
        public Vector2 toVec;
        public float time;
    }

    private LerpTo[] lerpTos = null;
    private int currentTo = 0;
    private Vector2 fromVec = Vector2.zero;
    private Vector2 toVec = Vector2.zero;
    private float totalTime = 0;
    private float currentTime = 0;
    private UnityAction onCompleteCallback = null;
    private bool lerping = false;

    public void DoLerp(Vector2 from, Vector2 to, float time, UnityAction onComplete)
    {
        fromVec = from;
        toVec = to;
        totalTime = time;
        currentTime = 0;
        onCompleteCallback = onComplete;
        lerping = true;
    }

    public void DoLerp(Vector2 from, LerpTo[] tos, UnityAction onComplete)
    {
        currentTo = 0;
        lerpTos = tos;
        DoLerp(from, tos[0].toVec, tos[0].time, onComplete);
    }

    private void FinishLerp()
    {
        lerping = false;
        lerpTos = null;
        currentTo = 0;
        onCompleteCallback?.Invoke();
    }

    private void Update()
    {
        if (lerping)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector2.Lerp(fromVec, toVec, currentTime / totalTime);
            if (currentTime >= totalTime)
            {
                currentTo += 1;
                if (lerpTos == null || currentTo >= lerpTos.Length)
                {
                    FinishLerp();
                }
                else
                {
                    DoLerp(transform.position, lerpTos[currentTo].toVec, lerpTos[currentTo].time, onCompleteCallback);
                }
            }
        }
    }
}
