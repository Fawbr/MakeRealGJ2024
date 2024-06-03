using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] float speed, destroytime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InactiveObject());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vectorMesh = transform.localScale;
        float growing = speed * Time.deltaTime;
        transform.localScale = new Vector3(vectorMesh.x + growing, vectorMesh.y + growing, vectorMesh.z + growing);
    }

    IEnumerator InactiveObject()
    {
        yield return new WaitForSeconds(destroytime);
        gameObject.SetActive(false);
    }
}
