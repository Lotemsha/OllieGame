using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UIManager : MonoBehaviour
{
    public Volume globalVolume;
    private DepthOfField dof;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (globalVolume == null)
            globalVolume = FindObjectOfType<Volume>();

        globalVolume.profile.TryGet(out dof);
    }

    public void SetBlur(bool active)
    {
        if (dof != null)
            dof.active = active;
    }
}
