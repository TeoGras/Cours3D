using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SpawnCubeInCircle : MonoBehaviour
{
    [SerializeField] private GameObject CubeHolder;
    [SerializeField] private GameObject CubePrefab;

    [SerializeField] private int NbCube = 4;
    private int PreviousNbCube;
    private GameObject [] CubRef = new GameObject[4];

    [SerializeField] private int Radius = 1;
    private int PreviousRadius;
    private Point3D Centre = new Point3D(0,0,0);
    private Vector3[] CirclePoints;
    private Quaternion[] RotationCube;
    
    // Start is called before the first frame update
        void Start()
        {
            PreviousNbCube = NbCube;
            PreviousRadius = Radius;
            CirclePoints = Coordonate(NbCube);
            RotationCube = Rotation(NbCube);
            for (int i = 0; i < NbCube; i++)
            {
                CubRef[i] = Instantiate(CubePrefab,position : CirclePoints[i],RotationCube[i],CubeHolder.transform);
            }
    }

    // Update is called once per frame
    void Update()
    {
        //A chaque fois qu'une variable est touché, on recréer les cubes
        if (NbCube!=PreviousNbCube || Radius!=PreviousRadius)
        {
            for (int j = 0; j < PreviousNbCube; j++)
            {
                Destroy(CubRef[j]);
            }
            //On redefinie les variables pour créer les nouveaux cubes
            CirclePoints = Coordonate(NbCube);
            RotationCube = Rotation(NbCube);
            CubRef = new GameObject[NbCube];
            
            //On les instancie
            for (int i = 0; i < NbCube; i++)
            {
                CubRef[i] = Instantiate(CubePrefab,position : CirclePoints[i],RotationCube[i],CubeHolder.transform);
            }

            PreviousNbCube = NbCube;
            PreviousRadius = Radius;
        }
        
    }
    
    private Vector3 [] Coordonate(int NbCube)
    { 
        /*Fonction qui permet de calculer les coordonnées de chaque cube
         *return Vecteur3 [] qui correspond a un tableau de Vecteur3[NbCube] element contenant le vecteur de translation
         *par rapport au CubeHolder 
         */
        Vector3 [] CirclePoints = new Vector3[NbCube];

        for (int i = 0; i < NbCube; i++) {
            float rad = 2 * Mathf.PI * i / NbCube;
            //Calcule a l'aide d'une simple équation de cercle
            CirclePoints[i]=new Vector3(Centre.x + Radius * Mathf.Cos(rad), Centre.y + Radius * Mathf.Sin(rad), Centre.z);
        }

        return CirclePoints;
    }

    private Quaternion[] Rotation(int NbCube)
    {
        /*Fonction qui permet de calculer la rotation de chaque cube sur eux même afin de pointer le CubeHolder
         *return Quaternion [] qui correspond a un tableau de Quaternion[NbCube] element contenant le vecteur de rotation
         pour pointer le CubeHolder
         */
        Quaternion[] RotationCube = new Quaternion[NbCube];
        for (int i = 0; i < NbCube; i++)
        {
            float deg = 360 * i / NbCube;
            RotationCube[i] = Quaternion.Euler(0,0,deg);
        }

        return RotationCube;
    }
}

