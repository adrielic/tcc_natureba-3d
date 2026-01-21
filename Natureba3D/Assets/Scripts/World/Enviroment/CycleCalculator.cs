using UnityEngine;
using Borodar.FarlandSkies.LowPoly;

public class CycleCalculator : MonoBehaviour
{
    public float endThreshold;

    void Start()
    {
        float cycleStart = SkyboxCycleManager.Instance.CycleProgress / 100;
        float cycleEnd = endThreshold / 100;
        float totalDuration = GameManager.Instance.levelTimeLimit;

        SkyboxCycleManager.Instance.CycleDuration = totalDuration / (cycleEnd - cycleStart);

        SkyboxCycleManager.Instance.Paused = !GameManager.Instance.countdownWasStarted;
    }
}
