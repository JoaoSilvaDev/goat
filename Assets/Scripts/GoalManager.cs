using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public GameObject deactivatedGoal;
    public GameObject activatedGoal;

    public void ActivateGoal()
    {
        deactivatedGoal.SetActive(false);
        activatedGoal.SetActive(true);
    }
}
