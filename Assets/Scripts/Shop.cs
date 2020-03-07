using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    /****************************************************/
    /***************** Basic Properties *****************/
    /****************************************************/
    // Cost of boba
    [SerializeField]
    private int upAttackCost;

    // Cost of coffee
    [SerializeField]
    private int upMaxFRCost;

    // Cost of care package
    [SerializeField]
    private int upMaxHPCost;

    /****************************************************/
    // Public properties that corresponds to the private
    // properties above
    /****************************************************/
    public int UpAttackCost
    {
        get
        {
            return upAttackCost;
        }
        set
        {
            this.upAttackCost = value;
        }
    }

    public int UpMaxFRCost
    {
        get
        {
            return upMaxFRCost;
        }
        set
        {
            this.upMaxFRCost = value;
        }
    }

    public int UpMaxHPCost
    {
        get
        {
            return upMaxHPCost;
        }
        set
        {
            this.upMaxHPCost = value;
        }
    }

    /****************************************************/
    /***************** Basic Functions ******************/
    /****************************************************/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
