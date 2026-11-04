
using UnityEngine;

public class SurvivalTimer : MonoBehaviour
{
    public float goal = 60f;
    public Animator gateAnimator;

    float t;
    bool won;

    void Update()
    {
        if (won) return;

        t += Time.deltaTime;

        if (t >= goal)
        {
            won = true;
            Debug.Log("YOU WIN!");

            if (gateAnimator != null)
                gateAnimator.SetBool("Open", true);
        }
    }
}
