using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage = 10;
    public float stunTime = 0.2f;
    Collider2D col;
    void Awake()
    {
        col = GetComponent<Collider2D>();
        if (col) col.enabled = false;
    }

    public void Activate()
    {
        if (col) col.enabled = true;
    }

    public void Deactivate()
    {
        if (col) col.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
        var health = other.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage, transform);
        }
    }
}