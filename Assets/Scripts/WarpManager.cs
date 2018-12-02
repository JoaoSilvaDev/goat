using UnityEngine;

public class WarpManager : MonoBehaviour
{
    public GameObject warp1;
    public GameObject warp2;
    public GameObject activatedWarp;
    public GameObject target;

    void Update()
    {
        if (activatedWarp == warp1)
            target = warp2;
        else if (activatedWarp == warp2)
            target = warp1;
    }
}
