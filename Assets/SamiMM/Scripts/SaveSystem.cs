// using System.Linq;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class SaveSystem : MonoBehaviour
// {
// 	public static SaveSystem instance;
// 	private Inventory inventory;
// 	private SaveData saveData;

// 	public bool dontOverrideScene;

// 	private void Awake()
// 	{
// 		if (instance != null)
// 		{
// 			Destroy(gameObject);
// 			return;
// 		}

// 		instance = this;

// 		// Subscribe immediately so saveData is never null on scene load
// 		SceneManager.sceneLoaded += OnSceneLoaded;

// 		// Load or initialize saveData
// 		saveData = SaveManager.LoadGame() ?? new SaveData();
// 	}

// 	private void Start()
// 	{
// 		// Inventory depends on scene objects�delay one frame

// 		var all = FindObjectsByType<Pickup>(FindObjectsInactive.Include, FindObjectsSortMode.None);
// 		foreach (var t in all)
// 		{
// 			t.Clone();
// 		}

// 		Invoke(nameof(LoadInventory), 0.1f);
// 	}
// 	private void LoadTutorial()
// 	{
// 		// Robust cross-version way to find inactive TutorialManager instances
// 		TutorialManager t = FindFirstObjectByType<TutorialManager>(FindObjectsInactive.Include);

// 		if (t != null)
// 		{
// 			if (saveData == null) saveData = new SaveData();
// 			if (saveData.tutorialHasPlayed)
// 			{
// 				t.gameObject.SetActive(false);
// 				return;
// 			}

// 			// Your original behaviour: mark as played immediately and deactivate
// 			saveData.tutorialHasPlayed = true;
// 			t.gameObject.SetActive(true);

// 			SaveManager.SaveGame(saveData);
// 		}
// 	}
// 	private void LoadInventory()
// 	{
// 		inventory = FindFirstObjectByType<Inventory>();

// 		// Restore inventory (global list)
// 		if (inventory != null)
// 			inventory.LoadFromIDs(saveData.collectedItemIDs);

// 		var inventoryUI = FindFirstObjectByType<InventoryUI>();
// 		if (inventoryUI != null)
// 			inventoryUI.UpdateNotebookClues();
// 	}

// 	private void OnDestroy()
// 	{
// 		SceneManager.sceneLoaded -= OnSceneLoaded;
// 	}

// 	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
// 	{
// 		// Run our delayed init so scene objects (including inactive) are present.
// 		StartCoroutine(DelayedSceneInit());
// 	}

// 	private IEnumerator DelayedSceneInit()
// 	{
// 		// wait one frame so scene hierarchy (including inactive objects) is fully available
// 		yield return null;

// 		// load/handle tutorial according to existing logic
// 		LoadTutorial();

// 		// then remove collected pickups in scene (keeps original flow)
// 		StartCoroutine(DelayedRemovePickups());
// 	}

// 	private IEnumerator DelayedRemovePickups()
// 	{
// 		yield return null;
// 		RemoveCollectedPickupsInScene();
// 	}

// 	/// <summary>
// 	/// Call this whenever the player picks up a GameItem.
// 	/// It adds it to the per-scene list + the global inventory list,
// 	/// then writes the save file.
// 	/// </summary>
// 	public void MarkItemCollected(GameItem item)
// 	{
// 		if (saveData == null) saveData = new SaveData();

// 		// Find inventory if not already found
// 		if (inventory == null)
// 			inventory = FindFirstObjectByType<Inventory>();

// 		if (inventory == null)
// 		{
// 			Debug.LogError("[SaveSystem] No Inventory found in scene!");
// 			return;
// 		}

// 		string scene = SceneManager.GetActiveScene().name;

// 		// Find (or create) the ScenePickupData for this scene
// 		var entry = saveData.pickupsByScene
// 			.FirstOrDefault(e => e.sceneName == scene);
// 		if (entry == null)
// 		{
// 			entry = new ScenePickupData { sceneName = scene };
// 			saveData.pickupsByScene.Add(entry);
// 		}

// 		// Record this pickup (avoid duplicates)
// 		if (!entry.itemIDs.Contains(item.itemName))
// 			entry.itemIDs.Add(item.itemName);

// 		// Update global inventory list & currentScene
// 		saveData.collectedItemIDs = inventory.GetCollectedItemIDs();
// 		saveData.currentScene = scene;

// 		// Persist
// 		SaveManager.SaveGame(saveData);
// 	}

// 	/// <summary>
// 	/// Destroys any Pickup GameObjects in the current scene whose IDs
// 	/// are already recorded as collected for this scene.
// 	/// </summary>
// 	public void RemoveCollectedPickupsInScene()
// 	{
// 		if (saveData == null || saveData.pickupsByScene == null)
// 			return;

// 		string scene = SceneManager.GetActiveScene().name;
// 		var entry = saveData.pickupsByScene
// 			.FirstOrDefault(e => e.sceneName == scene);
// 		if (entry == null || entry.itemIDs == null)
// 			return;

// 		// Include inactive in case pickups start disabled
// 		var allPickups = FindObjectsByType<Pickup>(
// 			FindObjectsInactive.Include,
// 			FindObjectsSortMode.None);

// 		foreach (var pickup in allPickups)
// 		{
// 			if (pickup == null || pickup.gameItem == null)
// 				continue;

// 			string id = pickup.gameItem.itemName;
// 			if (string.IsNullOrEmpty(id))
// 				continue;

// 			if (entry.itemIDs.Contains(id))
// 			{
// 				Debug.Log($"[SaveSystem] Destroying collected pickup: {id}");
// 				Destroy(pickup.gameObject);
// 			}
// 		}
// 	}

// 	/// <summary>
// 	/// Convenience for editor or debug UI.
// 	/// </summary>
// 	public void DeleteSave()
// 	{
// 		SaveManager.DeleteData();
// 		saveData = new SaveData();
// 		// Optionally respawn pickups:
// 		// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
// 	}
// }


using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
	public static SaveSystem instance;
	private Inventory inventory;
	private SaveData saveData;

	public bool dontOverrideScene;

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;

		// Subscribe immediately so saveData is never null on scene load
		SceneManager.sceneLoaded += OnSceneLoaded;

		// Load or initialize saveData
		saveData = SaveManager.LoadGame() ?? new SaveData();
	}

	private void Start()
	{
		// Inventory depends on scene objectsï¿½delay one frame

		var all = FindObjectsByType<Pickup>(FindObjectsInactive.Include, FindObjectsSortMode.None);
		foreach (var t in all)
		{
			t.Clone();
		}

		Invoke(nameof(LoadInventory), 0.1f);
	}
	private void LoadTutorial()
	{
		// Robust cross-version way to find inactive TutorialManager instances
		TutorialManager t = FindFirstObjectByType<TutorialManager>(FindObjectsInactive.Include);

		if (t != null)
		{
			if (saveData == null) saveData = new SaveData();
			if (saveData.tutorialHasPlayed)
			{
				t.gameObject.SetActive(false);
				return;
			}

			// Your original behaviour: mark as played immediately and deactivate
			saveData.tutorialHasPlayed = true;
			t.gameObject.SetActive(true);

			SaveManager.SaveGame(saveData);
		}
	}
	private void LoadInventory()
	{
		inventory = FindFirstObjectByType<Inventory>();

		// Restore inventory (global list)
		if (inventory != null)
			inventory.LoadFromIDs(saveData.collectedItemIDs);

		var inventoryUI = FindFirstObjectByType<InventoryUI>();
		if (inventoryUI != null)
			inventoryUI.UpdateNotebookClues();
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// Run our delayed init so scene objects (including inactive) are present.
		StartCoroutine(DelayedSceneInit());
	}

	private IEnumerator DelayedSceneInit()
	{
		// wait one frame so scene hierarchy (including inactive objects) is fully available
		yield return null;

		// load/handle tutorial according to existing logic
		LoadTutorial();

		// then remove collected pickups in scene (keeps original flow)
		StartCoroutine(DelayedRemovePickups());
	}

	private IEnumerator DelayedRemovePickups()
	{
		yield return null;
		RemoveCollectedPickupsInScene();
	}

	/// <summary>
	/// Call this whenever the player picks up a GameItem.
	/// It adds it to the per-scene list + the global inventory list,
	/// then writes the save file.
	/// </summary>
	public void MarkItemCollected(GameItem item)
	{
		if (saveData == null) saveData = new SaveData();

		// Find inventory if not already found
		if (inventory == null)
			inventory = FindFirstObjectByType<Inventory>();

		if (inventory == null)
		{
			Debug.LogError("[SaveSystem] No Inventory found in scene!");
			return;
		}

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
		if (saveData == null || saveData.pickupsByScene == null)
			return;

		string scene = SceneManager.GetActiveScene().name;
		var entry = saveData.pickupsByScene
			.FirstOrDefault(e => e.sceneName == scene);
		if (entry == null || entry.itemIDs == null)
			return;

		// Include inactive in case pickups start disabled
		var allPickups = FindObjectsByType<Pickup>(
			FindObjectsInactive.Include,
			FindObjectsSortMode.None);

		foreach (var pickup in allPickups)
		{
			if (pickup == null || pickup.gameItem == null)
				continue;

			string id = pickup.gameItem.itemName;
			if (string.IsNullOrEmpty(id))
				continue;

			if (entry.itemIDs.Contains(id))
			{
				Debug.Log($"[SaveSystem] Destroying collected pickup: {id}");
				Destroy(pickup.gameObject);
			}
		}
	}

	/// <summary>
	/// Convenience for editor or debug UI.
	/// </summary>
	public void DeleteSave()
	{
		SaveManager.DeleteData();
		saveData = new SaveData();
		// Optionally respawn pickups:
		// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}