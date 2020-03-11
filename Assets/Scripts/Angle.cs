using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// class for generating vectors with angular offsets
public class Angle : MonoBehaviour
{

    /**
     * <summary> Generate a list of normalized <c>numVectors</c> Vectors starting at
     * <c>initialVector</c> with equiangular distance between them.
     *
     * 
     * </summary>
     *
     * <param name="numVectors" must be >= 1</param>
     *
     *
     */
    public static List<Vector2> GetCircularVectorPattern(int numVectors, Vector2 initialVector)
    {
        float angularOffset = 360f / numVectors;
        List<Vector2> vectorList = new List<Vector2>();

        for (int i = 0; i < numVectors; i++)
        {
            vectorList.Add(AngularOffset(initialVector, angularOffset * i));
        }
        return vectorList;
    }

    /**
     * <summary> Create new normalized vector <c>degreeOffset</c> degress from vector
     * </summary>
     * 
     *
     */
    public static Vector2 AngularOffset(Vector2 vector, float degreeOffset)
    {
        // get the new vector angular offset from 0 degrees
        float vectorAngle = Vector2.SignedAngle(Vector2.right, vector);
        float originDegreeOffset = vectorAngle + degreeOffset;

        // create x, y componenets
        float newVectorX = Mathf.Cos(Mathf.Deg2Rad * originDegreeOffset);
        float newVectorY = Mathf.Sin(Mathf.Deg2Rad * originDegreeOffset);

        return new Vector2(newVectorX, newVectorY);
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
