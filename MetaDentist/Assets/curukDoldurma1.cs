using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class curukDoldurma1 : MonoBehaviour

{


    public GameObject doldurulcakYer;
    public GameObject curuk;
    public GameObject curuks;
    public float minearalik;
    public float frequency;
    public int CubeCount;
    // Start is called before the first frame update
    void Start()
    {
        doldur();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void doldur()
    {
       

        CubeCount = 0;
        Renderer render = doldurulcakYer.GetComponent<Renderer>();
        Renderer rendercuruk = curuk.GetComponent<Renderer>();
        float xbound = render.transform.localScale.x;
        float ybound = render.transform.localScale.y;
        float zbound = render.transform.localScale.z;



        if (render != null)
        {
            for (float xR = rendercuruk.transform.localScale.x; xR <= xbound + minearalik; xR += rendercuruk.transform.localScale.x)
            {
                for (float yR = rendercuruk.transform.localScale.y; yR <= (ybound + minearalik); yR += rendercuruk.transform.localScale.y)
                {
                    for (float zR = rendercuruk.transform.localScale.z; zR <= (zbound + minearalik); zR += rendercuruk.transform.localScale.z)
                    {
                        CubeCount += 1;
                        Instantiate(curuk, new Vector3(xR, yR, zR), Quaternion.identity , curuks.transform);
                        
                    }
                }


            }
            
        }



        //curuks.transform.position = new Vector3(0, 0, 0);
        curuks.transform.SetParent(doldurulcakYer.transform , true);
        //curuks.transform.localPosition = doldurulcakYer.transform.localPosition;
        curuks.transform.localPosition = new Vector3(0, 0, 0);
    }

}
