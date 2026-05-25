using UnityEngine;

public class GlobalVolume : MonoBehaviour
{
    public class GlobalVolumeKeeper : MonoBehaviour
    {
        void Awake()
        {
            var existing = FindObjectsOfType<GlobalVolumeKeeper>();
            if (existing.Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}
