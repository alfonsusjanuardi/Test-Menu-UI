using UnityEngine;

public abstract class BaseSceneController : MonoBehaviour
{
    protected GlobalManager gm;

    /**
    * On awake, set a reference to Global Manager.
    */
    protected virtual void Awake()
    {
        // Get Global Manager instance
        gm = GlobalManager.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
    * When a scene is destroyed, make sure all event listeners it added to the SmartFox class instance are removed.
    */
    protected virtual void OnDestroy()
    {
        if (gm.gamePlay == GlobalManager.GamePlay.Multiplayer)
        {
            // Remove SFS2X listeners
            RemoveSmartFoxListeners();
        }
    }

    /**
    * Abstract method to be implementated by controller classes, to remove all SmartFox-related event listeners.
    */
    protected abstract void RemoveSmartFoxListeners();
}
