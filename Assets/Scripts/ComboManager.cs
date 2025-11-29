using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public float inputBufferTime = 0.4f;
    float lastInputTime = -10f;
    int comboIndex = 0;
    public int maxCombo = 3;

    public void RegisterAttackInput()
    {
        float t = Time.time;
        if (t - lastInputTime <= inputBufferTime)
        {
            comboIndex = Mathf.Min(comboIndex + 1, maxCombo);
        }
        else
        {
            comboIndex = 1;
        }
        lastInputTime = t;
        GetComponent<Animator>().SetInteger("ComboIndex", comboIndex);
    }

    public void ResetCombo()
    {
        comboIndex = 0;
        GetComponent<Animator>().SetInteger("ComboIndex", 0);
    }
}