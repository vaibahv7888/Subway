
    using UnityEngine;
    using System.Collections;
     
    public class PlayerScript : MonoBehaviour
    {
    private Vector2 pos1;
    private Vector2 pos2;
    public float objectHeight = 1.0f;
    public GameObject prefab;
     
    private GameObject go;
     
    void Update()
    {
    if (Input.GetMouseButtonDown(0))
    {
     
    pos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    pos2 = pos1;
     
    go = Instantiate(prefab) as GameObject;
    go.transform.position = pos1;
    Vector3 temp = go.transform.localScale;
    temp.y = 0.0f;
    go.transform.localScale = temp;;
    }
     
    if (Input.GetMouseButton(0))
    {
    pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
     
    if (pos2 != pos1)
    {
    Vector2 v3 = pos2 - pos1;
    go.transform.position = pos1 + (v3) / 2.0f;
    go.transform.localScale = new Vector2(go.transform.localScale.x, v3.magnitude / objectHeight);
    go.transform.rotation = Quaternion.FromToRotation(Vector2.up, v3);
    }
    }
    }