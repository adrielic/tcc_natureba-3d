using UnityEngine;
using Borodar.FarlandSkies.LowPoly;

// Classe responsável por calcular o ciclo de dia e noite com base na duração total de uma fase
public class CycleCalculator : MonoBehaviour
{
    [SerializeField] private float endThreshold; // Momento do dia (em porcentagem) em que eu quero que a fase termine

    void Start()
    {
        float cycleStart = SkyboxCycleManager.Instance.CycleProgress / 100; // Momento do dia (em porcentagem) em que a fase começa (se 30 / 100 = 0.30)
        float cycleEnd = endThreshold / 100; // Convertendo para decimal (se 92 / 100 = 0.92)
        float totalDuration = GameManager.Instance.timeLimit; // Duração total de uma fase

        SkyboxCycleManager.Instance.CycleDuration = totalDuration / (cycleEnd - cycleStart); // Calculando a duração de duração de um ciclo com base na duração de uma fase, dividido pelo momento do dia em que ela termina, subtraído pelo momento do dia em que ela começa 
        SkyboxCycleManager.Instance.Paused = !GameManager.Instance.startCountdown; // Se o contador da fase está pausado, o ciclo de dia e noite também pausa
    }
}
