using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public string transitionedFromScene;
	public Vector2 platformingRespawnPoint;
	public Vector2 respawnPoint;
	[SerializeField] Bench bench;
	public static GameManager Instance { get; private set; }

	private void Awake()
	{
		SaveData.Instance.Initialize();
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
		SaveScene();
		DontDestroyOnLoad(gameObject);
		bench = FindObjectOfType<Bench>();
	}

	public void SaveScene()
	{
		string currentSceneName = SceneManager.GetActiveScene().name;
		SaveData.Instance.sceneNames.Add(currentSceneName);
	}

	public void RespawnPlayer()
	{
		SaveData.Instance.LoadBench();
		if(SaveData.Instance.benchPos != null)
		{
			SceneManager.LoadScene(SaveData.Instance.benchSceneName);
		}
		if(bench !=null)
		{
			if(bench.interacted)
			{
				respawnPoint = bench.transform.position;
			}
			else
			{
				respawnPoint = platformingRespawnPoint;
			}
		}
		else
			{
				respawnPoint = platformingRespawnPoint;
			}
		
		PlayerController.Instance.transform.position = respawnPoint;
		StartCoroutine(UIManager.Instance.DeactivateDeathScreen());
		PlayerController.Instance.Respawned();
	}
}
