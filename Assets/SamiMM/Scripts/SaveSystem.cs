using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
	public static SaveSystem instance;
	private Inventory inventory;
	private SaveData saveData;

	private void Awake()
	{
		if (instance != null) Destroy(gameObject);
		else
			instance = this;
	}

	private void Start()
	{
		inventory = FindFirstObjectByType<Inventory>();
		// Load or initialize
		saveData = SaveManager.LoadGame() ?? new SaveData();
		// Restore inventory (global list)
		if (inventory != null)
			inventory.LoadFromIDs(saveData.collectedItemIDs);
		// Hook scene events
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// Remove pickups already collected in this scene
		RemoveCollectedPickupsInScene();
	}

	/// <summary>
	/// Call this whenever the player picks up a GameItem.
	/// It adds it to the per-scene list + the global inventory list,
	/// then writes the save file.
	/// </summary>
	public void MarkItemCollected(GameItem item)
	{
		string scene = SceneManager.GetActiveScene().name;

		// Find (or create) the ScenePickupData for this scene
		var entry = saveData.pickupsByScene
			.FirstOrDefault(e => e.sceneName == scene);
		if (entry == null)
		{
			entry = new ScenePickupData { sceneName = scene };
			saveData.pickupsByScene.Add(entry);
		}

		// Record this pickup (avoid duplicates)
		if (!entry.itemIDs.Contains(item.itemName))
			entry.itemIDs.Add(item.itemName);

		// Update global inventory list & currentScene
		saveData.collectedItemIDs = inventory.GetCollectedItemIDs();
		saveData.currentScene = scene;

		// Persist
		SaveManager.SaveGame(saveData);
	}

	/// <summary>
	/// Destroys any Pickup GameObjects in the current scene whose IDs
	/// are already recorded as collected for this scene.
	/// </summary>
	public void RemoveCollectedPickupsInScene()
	{
		string scene = SceneManager.GetActiveScene().name;
		var entry = saveData.pickupsByScene
			.FirstOrDefault(e => e.sceneName == scene);
		if (entry == null) return;

		foreach (var pickup in FindObjectsByType<Pickup>(FindObjectsSortMode.None))
		{
			if (entry.itemIDs.Contains(pickup.gameItem.itemName))
				Destroy(pickup.gameObject);
		}
	}

	/// <summary>
	/// Convenience for editor or debug UI.
	/// </summary>
	public void DeleteSave()
	{
		SaveManager.DeleteData();
		saveData = new SaveData();
		// Optionally, refresh the scene to respawn all pickups:
		// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
