using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartService : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private GameObject _nemu;

    private void Update()
    {
        if (_character.Health <= 0)
        {
            _nemu.SetActive(true);
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}