using UnityEngine;
using System.Collections;

public class MengerSpongeFractal : MonoBehaviour
{
    [SerializeField] private GameObject mengerSpongePrefab;

    //[SerializeField] private MengerCubePoolManager mengerCubePoolManager;

    [SerializeField] private float initialScale=1f;

    private float newScale, offset;

    [SerializeField] private Vector3 initialPosition;

    [SerializeField] private int iterations = 1;

    [SerializeField] private GameObject gameObjectParent;

    public void Start(){
        Debug.Log("Start");
        GenerateMengerSponge();
    }
    public void SetDepth(int depth)
    {
        Debug.Log("SetDepth: " + depth);
        iterations = depth;
    }

    public void SetInitialScale(float scale){
        initialScale = scale;
        transform.localScale = Vector3.one * initialScale;
    }

    public void InitializeScaleEditor(){
        Debug.Log("InitializeScaleEditor");
        transform.localScale = Vector3.one * initialScale;
    }

    public void DeactivateComponents(){
        // Deactivate the renderer, collider, and this script component.
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }
//
    //public void ActivateComponents(){
    //    // Deactivate the renderer, collider, and this script component.
    //    MeshRenderer renderer = GetComponent<MeshRenderer>();
    //    if (renderer != null)
    //    {
    //        renderer.enabled = true;
    //    }
//
    //    //BoxCollider collider = GetComponent<BoxCollider>();
    //    //if (collider != null)
    //    //{
    //    //    collider.enabled = false;
    //    //}
    //}



    public void GenerateMengerSponge()
    {
        Debug.Log("GenerateMengerSponge");
        if (iterations == 0){
            Debug.Log("iterations == 0, breaking out of GenerateMengerSponge()");
            return;
        }

        DeactivateComponents();

        initialPosition = transform.position;

        newScale = initialScale / 3f;

        offset = newScale;
        int xIndex = 0;
        int yIndex = 0;
        int zIndex = 0;
        for (float x = -1; x <= 1; x++){
            if (x==0){
                xIndex=1;
            } else {
                xIndex=0;
            }
            for (float y = -1; y <= 1; y++){
                if (y==0){
                    yIndex=1;
                } else {
                    yIndex=0;
                }
                for (float z = -1; z <= 1; z++){
                    if (z==0){
                        zIndex=1;
                    } else {
                        zIndex=0;
                    }
                    //Debug.Log("numberOfZeroIndexes: " + numberOfZeroIndexes);
                    if (xIndex + yIndex + zIndex < 2){
                        //Vector3 newPosition = initialPosition + new Vector3(x * offset, y * offset, z * offset);
                        // Get a cube from the object pool.
                        GameObject newMengerSponge = MengerCubePoolManager.Instance.GetCubeFromPool();
                        if (newMengerSponge != null){
                            //newMengerSponge.transform.SetParent(transform);
                            
                            newMengerSponge.transform.position = initialPosition + new Vector3(x, y, z) * offset;
                            MengerSpongeFractal newMengerSpongeScript = newMengerSponge.GetComponent<MengerSpongeFractal>();
                            newMengerSpongeScript.SetParent(gameObjectParent);
                            newMengerSpongeScript.SetInitialScale(newScale);
                            newMengerSpongeScript.SetDepth(iterations - 1);
                            newMengerSpongeScript.GenerateMengerSponge();
                        }   
                    }
                }
            }
        }
        // Deactivate self
        gameObject.SetActive(false);
    }

    public void SetParent(GameObject parent){
        gameObjectParent = parent;
        transform.parent = gameObjectParent.transform;
    }

    public void OnDisable(){
        Debug.Log("OnDisable");
        DeactivateComponents();
        // Return the cube to the pool.
        MengerCubePoolManager.Instance.ReturnCubeToPool(gameObject);
    }

    

}
