using System.Net.Sockets;
using Mono.Cecil;
using UnityEngine;

// Classe dos animais hostis ao jogador
public class Hostile : Entity
{
    [Header("Chasing")]
    [SerializeField] protected float chaseSpeed = 10f;
    [SerializeField] protected float chaseDelay = 0f; // Para animais com animação de detecção, deve ter o tempo da animação para impedir que ele deslize no chão
    protected bool isChasing;

    [Header("Target Loss")]
    [SerializeField] protected bool canLosePlayer = false; // Verdadeiro apenas para animais que podem perder o jogador com a distância
    [SerializeField] protected bool destroyAfterLosing; // Usado apenas no enxame
    [SerializeField] protected float losingDistance = 20f;

    protected override void Update()
    {
        base.Update();

        if (target == null) return;

        ChaseTarget();

        if (canLosePlayer)
        {
            LoseTarget();
        }
    }

    // Persegue o jogador atribuindo ele como alvo e se já não tiver perseguindo outro alvo antes
    protected void ChaseTarget()
    {
        agent.SetDestination(target.position);

        if (isChasing) return;

        if (chaseDelay > 0)
        {
            // Animais que fazem animação de detecção devem aguardar o tempo da animação antes de começar a perseguir
            Invoke(nameof(OnChasingTarget), chaseDelay);
        }
        else
        {
            OnChasingTarget();
        }

        isChasing = true;

        // Daqui pra baixo apenas para animais com animação
        if (animator == null) return;

        animator.SetTrigger("Chase");
    }

    // Animais perdem o alvo (jogador) se a distância entre jogador e animal for maior que a distância máxima estabelecida no inspector
    protected void LoseTarget()
    {
        if (target.CompareTag("Player"))
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > losingDistance)
            {
                if (destroyAfterLosing)
                {
                    Destroy(gameObject); // O enxame é destruído quando perde o jogador para dar a impressão de que se dispersou
                }
                else
                {
                    OnTargetLost();
                }
            }
        }
    }

    // Quando a perseguição começa, troca o valor da velocidade do Agent para que os animais se movam mais rápido
    protected void OnChasingTarget()
    {
        agent.speed = chaseSpeed;
    }

    // Quando o alvo é perdido, retira o alvo do jogador, para a perseguição e retorna ao valor inicial de velocidade
    protected void OnTargetLost()
    {
        target = null;
        agent.ResetPath();
        isChasing = false;
        agent.speed = regularSpeed;
    }

    // Quando o contato com o jogador é efetuado, executa o game over
    protected override void OnContact(Collider hit)
    {
        base.OnContact(hit);

        if (hit.CompareTag("Player")) // Faz contato apenas com o jogador
        {
            if (killedPlayer) return;

            GameManager.Instance.GameOver("Animal");
            killedPlayer = true;

            Debug.Log($"{gameObject.name} has hit the player.");
        }
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        
        // Desenhando a área de perda de alvo
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, losingDistance);
    }
}
