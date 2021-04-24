using UnityEngine;
using UnityEngine.Events;

public class Lerp : MonoBehaviour
{
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

    private void FinishLerp()
    {
        lerping = false;
        onCompleteCallback?.Invoke();
    }

    private void Update()
    {
        if(lerping)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector2.Lerp(fromVec, toVec, currentTime / totalTime);
            if(currentTime >= totalTime)
            {
                FinishLerp();
            }
        }
    }
}
