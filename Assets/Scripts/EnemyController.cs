using CoreClasses.Models;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyDataContainer dataContainer;
    private Enemy _enemyLogic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (dataContainer != null)
        {
            // כאן קורה הקסם: אנחנו יוצרים את האויב של ה-DLL מהנתונים של יוניטי
            _enemyLogic = dataContainer.CreateInstance();

            Debug.Log($"Enemy {_enemyLogic.Name} is ready! Type: {_enemyLogic.Type}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
