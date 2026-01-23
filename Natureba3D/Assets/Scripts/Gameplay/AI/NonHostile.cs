using UnityEngine;

// Classe dos animais não hostis ao jogador
public class NonHostile : Entity
{
    [Header("Fleeing")]
    [SerializeField] protected float fleeingSpeed = 12f;
    [SerializeField] protected float fleeingAngularSpeed = 360f;
    [SerializeField] protected float fleeingAcceleration = 10f;
    [SerializeField] protected float safeDistanceThreshold = 5f;
    protected bool isFleeing;

    protected override void Update()
    {
        base.Update();

        if (target == null) return;

        if (IsSafe())
        {
            OnSafePosition();
        }
        else
        {
            Flee();
        }
    }

    protected override void DetectTarget()
    {
        // Nenhum comportamento específico
        base.DetectTarget();
    }

    protected override void OnContact(Collider hit)
    {
        // Nenhum comportamento específico
        base.OnContact(hit);
    }

    protected void Flee()
    {
        if (isFleeing) return;

        OnFleeing();
    }

    protected bool IsSafe()
    {
        if (target.CompareTag("Player"))
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > safeDistanceThreshold) return true;
            else return false;
        }
        else return true;
    }

    protected void OnFleeing()
    {
        isFleeing = true;
        agent.speed = fleeingSpeed;
        agent.angularSpeed = fleeingAngularSpeed;
        agent.acceleration = fleeingAcceleration;
    }

    protected void OnSafePosition()
    {
        isFleeing = false;
        agent.speed = initialSpeed;
        agent.angularSpeed = initialAngularSpeed;
        agent.acceleration = initialAcceleration;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, safeDistanceThreshold);
    }
}
