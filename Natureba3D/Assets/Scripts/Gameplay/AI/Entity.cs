using UnityEngine;
using UnityEngine.AI;

// Classe mãe de todos os animais
public abstract class Entity : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] protected bool enable; // Decide se todo o comportamento da IA está ligado ou desligado

    [Header("Roaming")]
    [SerializeField] protected Transform path; // Caminho da ronda deve ser o objeto pai de cada waypoint
    protected Transform[] pathWaypoints;
    protected int currentWaypointIndex;
    protected float waypointTolerance = 1f; // Distância mínima para o próximo waypoint
    protected bool useWaypoints = false; // Apenas em animais que rodam uma área

    [Header("Detection")]
    [SerializeField] protected bool detectPlayer = true;
    [SerializeField] protected bool detectBait = false;
    [SerializeField] protected float detectionRadius = 10f;
    [SerializeField] protected LayerMask detectionLayer; // Selecionar a layer Target no inspector
    [SerializeField] protected Bait.BaitType preferredBaitType;
    protected Transform target;

    [Header("Contact")]
    [SerializeField] protected float contactRadius = 1f;
    [SerializeField] protected Transform contactArea;
    protected bool killedPlayer;

    protected float regularSpeed;

    protected NavMeshAgent agent;
    protected Animator animator;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        // Importante para resetar a velocidade de um animal quando ele parar de perseguir o jogador
        regularSpeed = agent.speed;

        // Se o animal possuir um caminho atribuído no inspetor, cada waypoint parenteado ao caminho será adicionado ao array que representa os pontos de passagem da ronda
        if (path == null) return;

        pathWaypoints = new Transform[path.childCount];

        for (int i = 0; i < path.childCount; i++)
        {
            pathWaypoints[i] = path.GetChild(i);
        }

        useWaypoints = true;
    }

    protected virtual void Update()
    {
        // A IA do animal é desativada quando ele pega uma isca. Também possível desativar no inspetor (para testes)
        if (!enable) return;

        DetectTarget();
        CheckContact();

        // Só fazer ronda se possui não possui um alvo, se usa waypoints e se o caminho tiver waypoints
        if (useWaypoints && pathWaypoints.Length > 0 && target == null)
        {
            Roam();
        }
    }

    // Comportamento de ronda garante que um waypoint seja sempre atribuído como destino, e procura o próximo waypoint
    protected virtual void Roam()
    {
        agent.SetDestination(pathWaypoints[currentWaypointIndex].position); // Determinando o destino

        if (!agent.pathPending && agent.remainingDistance < waypointTolerance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % pathWaypoints.Length; // Escolhendo o próximo waypoint
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

            if (detectBait && hit.TryGetComponent(out Bait bait) && bait.type == preferredBaitType && bait.isEnabled) // Encontrando uma isca ativa
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
        if (detectBait && hit.TryGetComponent(out Bait bait) && bait.type == preferredBaitType) // Verificando contato com iscas
        {
            TakeBait(hit);
        }
    }

    // Ao pegar uma isca, apaga o alvo, reseta o caminho do Agent e para a animação de perseguição
    protected void TakeBait(Collider baitCollider)
    {
        enable = false;
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
