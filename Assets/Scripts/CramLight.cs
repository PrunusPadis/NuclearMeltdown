using System.Numerics;
using UnityEngine;

public class CramLight : MonoBehaviour
{
    ReactorInternals reactor;
    [SerializeField] GameObject target;
    void Start()
    {
        reactor = ReactorInternals.Instance;
        target.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (reactor.cramActivated)
        {
            target.SetActive(true);
        }
    }
}
