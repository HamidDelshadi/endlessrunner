using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    /// <summary>
    /// responsible to hide the walls and then returns them to their original state after reactivation.
    /// </summary>
    public GameObject[] bricks; //list of bricks in the wall
    List<Rigidbody> bricksRBs = new List<Rigidbody>(); //list of Rigidbody component of bricks
    List<Vector3> positions = new List<Vector3>(); //list of position of each brick at the begining
    List<Quaternion> rotations = new List<Quaternion>(); // list of the rotation of each brick in the begining
    public GameObject Explosion; // the particle object for the explosion effect
    public string SpellTag; //the wall could be destroyed only with a spell with this tag
    Collider col; // the collider of the wall

    void OnEnable()
    {
        col.enabled = true;
        for (int i = 0; i < bricks.Length; i++) // it returns bricks to their primary positions 
        {
            bricks[i].transform.localPosition = positions[i];
            bricks[i].transform.localRotation = rotations[i];
            bricksRBs[i].isKinematic = true;
        }
    }

    void Awake()
    {
        col = this.GetComponent<Collider>();
        foreach (GameObject b in bricks)//saving the bricks primary positions
        {
            bricksRBs.Add(b.GetComponent<Rigidbody>());
            positions.Add(b.transform.localPosition);
            rotations.Add(b.transform.localRotation);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == SpellTag) // if it is the right kind of magic
        {
            //explosion effect
            var obj = Instantiate(Explosion, other.contacts[0].point, Quaternion.identity);
            Destroy(obj, 2);
            col.enabled = false;
            //make the brick non kinematic
            foreach (Rigidbody r in bricksRBs)
                r.isKinematic = false;
            //music effect
            SoundManager.Instance.PlaySFX(SFX.explosion);
        }
        else if (other.gameObject.tag=="SpellMetal" || other.gameObject.tag == "SpellPaper" || other.gameObject.tag == "SpellPlastic")
            Context.Manager.WrongSpell();
    }
}