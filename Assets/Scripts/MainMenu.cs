using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private string gameSceneName;

	public void PlayGame()
	{
		SceneManager.LoadScene(gameSceneName);
	}

	public void Quit()
	{
		Application.Quit();
	}

}
