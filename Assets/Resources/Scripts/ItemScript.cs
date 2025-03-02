using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemScript : MonoBehaviour
{
    public GameObject fieldPrefab;

    private GameObject fieldObject;
    private LoadingSceneScript loadingScript;
    // Start is called before the first frame update
    void Start()
    {
        fieldObject = Instantiate(fieldPrefab, this.transform.position, this.transform.rotation, this.transform);

        loadingScript = FindObjectOfType<LoadingSceneScript>();
        loadingScript.AddObjectScene(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenClose()
    {
    }
}
