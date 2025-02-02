using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class UIFunctions : MonoBehaviour
{
    private GameObject Spawnpoint;
    public GameObject LoseUI;

    public void Reload()
    {
        SceneManager.LoadScene(0);
        InventorySystem.Instance.Empty();
    }

    public void Retry()
    {
        var player = OmnicatLabs.CharacterControllers.CharacterController.Instance;
        Destroy(LoseUI);
        player.SetControllerLocked(false, false, false);
        player.rb.velocity = Vector3.zero;
        player.modelCollider.height = 2f;
        player.camHolder.localPosition = new Vector3(player.camHolder.localPosition.x, player.originalHeight, player.camHolder.localPosition.z);
        player.ChangeState(OmnicatLabs.CharacterControllers.CharacterStates.Idle);
        SaveManager.Instance.ResetTracked();
        player.transform.position = Checkpoint.spawnpoint.position;
        player.transform.rotation = Checkpoint.spawnpoint.rotation;
        MotionScanner.lastDoorOpened.Open();
        GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<NavMeshAgent>().speed = DifficultySelector.Instance.currentSpeed;
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private void OnDestroy()
    {
        GameStateController.isActive = false;
    }
}
