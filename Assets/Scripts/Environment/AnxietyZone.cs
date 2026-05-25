using CoreClasses.Models;
using UnityEngine;

public class EnvironmentZone : MonoBehaviour
{
    public EnvironmentType zoneType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = GameController.Instance.player;
            player.CurrentEnvironment = zoneType;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = GameController.Instance.player;
            player.CurrentEnvironment = EnvironmentType.Neutral;
        }
    }
}
