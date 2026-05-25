using System.Collections.Generic;
using UnityEngine;

public class NPCDatabase : MonoBehaviour
{
    public static NPCDatabase Instance;

    public List<NPCEntry> npcEntries;

    private Dictionary<string, NPCData> npcDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            npcDict = new Dictionary<string, NPCData>();
            foreach (var entry in npcEntries)
            {
                npcDict[entry.npcID] = entry.npcData;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static NPCData GetNPCByID(string id)
    {
        if (Instance.npcDict.TryGetValue(id, out NPCData data))
            return data;

        Debug.LogWarning("NPC ID not found: " + id);
        return null;
    }
}

[System.Serializable]
public class NPCEntry
{
    public string npcID;
    public NPCData npcData;
}
