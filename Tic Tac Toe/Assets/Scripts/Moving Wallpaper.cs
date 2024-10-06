using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallpaper : MonoBehaviour
{
    public GameObject decoX;
    GameObject[] X = new GameObject[4];
    public GameObject decoO;
    GameObject[] O = new GameObject[4];
    public float upSpeed;
    bool[] done = new bool[4];

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<4;i++){
            done[i] = i<3;

            X[i] = Instantiate(i%2==0?decoX:decoO, new Vector2(Random.Range(-406,-207),801 - i*605),i%2==0?Quaternion.Euler(new Vector3(0,0,Random.Range(-33.672f,33.672f))):Quaternion.identity);
            X[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0,upSpeed);

            O[i] = Instantiate(i%2==0?decoO:decoX, new Vector2(Random.Range(195,413),801 - i*605), i%2==0?Quaternion.identity:Quaternion.Euler(new Vector3(0,0,Random.Range(-33.672f,33.672f))));
            O[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0,upSpeed);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if((X[3].transform.position.y>-410) && !done[3]){
            X[0].transform.position = new Vector2(Random.Range(-436,-277),-1015);
            O[0].transform.position = new Vector2(Random.Range(245,453),-1185);
            done[0] = false;
            done[3]  = true;
        }

        for(int i=0;i<3;i++){
            if((X[i].transform.position.y>-410) && !done[i]){
                X[i+1].transform.position = new Vector2(Random.Range(-436,-277),-1015);
                O[i+1].transform.position = new Vector2(Random.Range(245,453),-1185);
                done[i+1] = false;
                done[i] = true;
            }
        }        
    }
}
