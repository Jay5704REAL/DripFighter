using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public Animator animator;
    public Rigidbody2D rb;
    public bool invulnerable = false;
    public float invulnTime = 0.3f;

    void Awake()
    {
        currentHP = maxHP;
        if (animator == null) animator = GetComponentInChildren<Animator>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int amount, Transform hitter)
    {
        if (invulnerable) return;
        currentHP -= amount;
        if (animator) animator.SetTrigger("Hit");

        // Knockback (simple)
        if (rb != null && hitter != null)
        {
            Vector2 knock = (transform.position - hitter.position).normalized * 5f;
            rb.AddForce(knock, ForceMode2D.Impulse);
        }

        StartCoroutine(InvulRoutine());

        if (currentHP <= 0) Die();
    }

    System.Collections.IEnumerator InvulRoutine()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnTime);
        invulnerable = false;
    }

    void Die()
    {
        if (animator) animator.SetTrigger("Dead");
        var controller = GetComponent<PlayerController>();
        if (controller) controller.enabled = false;
    }
}