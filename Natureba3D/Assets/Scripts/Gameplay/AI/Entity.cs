using UnityEngine;
using UnityEngine.AI;

// Classe mãe de todos os animais
public abstract class Entity : MonoBehaviour
{
    [Header("Roaming")]
    [SerializeField] protected bool useWaypoints; // Apenas em animais que rodam uma área
    [SerializeField] protected Transform path; // Caminho da ronda deve ser o objeto pai de cada waypoint
    [SerializeField] protected float waypointTolerance = 1f; // Distância mínima para o próximo waypoint
    protected Transform[] pathWaypoints;
    protected int currentWaypoint;

    [Header("Detection")]
    [SerializeField] protected float detectionRadius = 10f;
    [SerializeField] protected LayerMask detectionLayer; // Selecionar a layer Target no inspector
    [SerializeField] protected bool detectPlayer = true;
    [SerializeField] protected bool detectBait = false;
    [SerializeField] protected Bait.BaitType preferredBait;
    
    protected Transform target;

    [Header("Contact")]
    [SerializeField] protected float contactRadius = 1f;
    [SerializeField] protected Transform contactArea;
    protected bool baitTaken;

    protected float initialSpeed;
    protected float initialAngularSpeed;
    protected float initialAcceleration;

    protected NavMeshAgent agent;
    protected Animator animator;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Atribuindo o caminho do animal em tempo de execução
        if (path != null)
        {
            pathWaypoints = new Transform[path.childCount];

            for (int i = 0; i < path.childCount; i++)
            {
                pathWaypoints[i] = path.GetChild(i);
            }
        }

        // Importante para resetar as velocidades de um animal quando ele parar de perseguir o jogador
        initialSpeed = agent.speed;
        initialAngularSpeed = agent.angularSpeed;
        initialAcceleration = agent.acceleration;
    }

    protected virtual void Update()
    {
        // Daqui pra baixo, nada funciona se o animal já pegou uma isca
        if (baitTaken) return;

        DetectTarget();
        CheckContact();

        // Só fazer ronda se possui não possui um alvo, se usa waypoints e se o caminho tiver waypoints
        if (target == null && useWaypoints && pathWaypoints.Length > 0)
        {
            Roam();
        }
    }

    // Comportamento de ronda garante que um waypoint seja sempre atribuído como destino, e procura o próximo waypoint
    protected virtual void Roam()
    {
        agent.SetDestination(pathWaypoints[currentWaypoint].position); // Determinando o destino

        if (!agent.pathPending && agent.remainingDistance < waypointTolerance)
        {
            currentWaypoint = (currentWaypoint + 1) % pathWaypoints.Length; // Escolhendo o próximo waypoint
        }
    }

    // Detecta objetos com a layer Target que entrarem na área de detecção, caso o animal possa detectar jogador ou isca
    protected virtual void DetectTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        foreach (Collider hit in hits)
        {
            if (detectPlayer && hit.CompareTag("Player")) // Encontrando jogador
            {
                target = hit.transform;
                return;
            }

            if (detectBait && hit.TryGetComponent(out Bait bait) && bait.type == preferredBait && bait.isEnabled) // Encontrando uma isca ativa
            {
                target = hit.transform;
                return;
            }

            Debug.Log($"{gameObject.name} detected {hit.gameObject.name}.");
        }
    }

    // Verifica contato para causar dano ao jogador e para pegar isca
    protected void CheckContact()
    {
        Collider[] hits = Physics.OverlapSphere(contactArea.position, contactRadius, detectionLayer);

        foreach (Collider hit in hits)
        {
            OnContact(hit);
        }
    }

    // Quando o contato é efetuado. Já inclui a lógica de contato com a isca
    protected virtual void OnContact(Collider hit)
    {
        if (detectBait && hit.TryGetComponent(out Bait bait) && bait.type == preferredBait) // Verificando contato com iscas
        {
            TakeBait(hit);
        }
    }

    // Ao pegar uma isca, apaga o alvo, reseta o caminho do Agent e para a animação de perseguição
    protected void TakeBait(Collider baitCollider)
    {
        baitTaken = true;
        target = null;
        agent.ResetPath();

        if (animator != null)
        {
            animator.SetTrigger("Stop");
        }

        if (baitCollider.TryGetComponent(out Bait bait))
        {
            bait.OnBaitTaken();
        }

        Debug.Log($"{gameObject.name} took the bait ({baitCollider.gameObject.name}).");
    }

    // Desliga os componentes NavMesh Agent, Animator e o próprio script da IA
    public void ShutDown()
    {
        Destroy(agent);
        Destroy(animator);
        Destroy(this);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        // Desenhando a área de detecção
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Desenhando a área de contato
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(contactArea.position, contactRadius);
    }
}
