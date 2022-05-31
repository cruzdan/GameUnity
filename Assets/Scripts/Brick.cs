using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    BrickManager bm;
    // Start is called before the first frame update
    void Start()
    {
        bm = GameObject.Find("BrickManager").GetComponent<BrickManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bm.DecrementActualBricks();
        Destroy(this.gameObject);
    }
}
