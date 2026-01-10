using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour
{
    [Header("General Settings")]
    public float detectionRadius = 10f;
    public Transform player;
    public LayerMask detectionLayer;
    public bool useWaypoints;
    public Transform[] waypoints;
    public float waypointTolerance = 1f;

    [Header("Detection Settings")]
    public bool detectPlayer = true;
    public bool detectBait = false;
    public Bait.BaitType baitTypeToDetect;

    [Header("Interaction Settings")]
    public float interactionRadius = 2f;
    public LayerMask interactionLayer;

    protected NavMeshAgent agent;
    protected int currentWaypoint = 0;
    protected Transform target;
    protected bool baitTaken = false;

    protected Animator anim;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (baitTaken)
            return;

        DetectTarget();
        CheckInteractions();

        if (target != null)
        {
            agent.SetDestination(target.position);
            OnChasingTarget();
        }
        else if (useWaypoints && waypoints.Length > 0)
        {
            Roam();
        }
        else
        {
            agent.ResetPath();
        }
    }

    protected void Roam()
    {
        if (waypoints.Length == 0)
            return;

        agent.SetDestination(waypoints[currentWaypoint].position);

        if (!agent.pathPending && agent.remainingDistance < waypointTolerance)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    protected void DetectTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        foreach (Collider hit in hits)
        {
            // Jogador
            if (detectPlayer && hit.CompareTag("Player"))
            {
                target = hit.transform;
                return;
            }

            // Isca
            if (detectBait && hit.TryGetComponent<Bait>(out var bait) && bait.type == baitTypeToDetect)
            {
                target = hit.transform;
                return;
            }
        }

        // Só limpa o alvo se for enxame (os outros mantêm até interagir)
        if (!(this is Swarm))
            return;

        target = null;
    }

    protected virtual void OnChasingTarget()
    {
        Debug.Log($"{gameObject.name} is chasing the player.");
        anim.SetTrigger("DetectedPlayer");
    }

    public void TakeBait()
    {
        baitTaken = true;
        agent.ResetPath();
        target = null;
        anim.SetTrigger("DetectedBait");
    }

    protected void CheckInteractions()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRadius, interactionLayer);

        foreach (Collider hit in hits)
        {
            // Atacar o jogador
            if (hit.CompareTag("Player"))
            {
                Debug.Log($"{gameObject.name} hit you.");
            }

            // Pegar a isca
            if (detectBait && hit.TryGetComponent<Bait>(out var bait) && bait.type == baitTypeToDetect)
            {
                TakeBait();
                Debug.Log($"{gameObject.name} has taken the {baitTypeToDetect}.");

                if (hit.TryGetComponent<Interactable>(out var interactable))
                    interactable.isPickable = false;
            }
        }
    }

    public void ShutDown()
    {
        Destroy(agent);
        Destroy(GetComponent<Animator>());
        Destroy(this);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        // Área de detecção
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Área de interação / Ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
