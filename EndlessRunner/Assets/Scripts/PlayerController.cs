using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// this is a componenet connected to the player and controls its behaviour
    /// </summary>
    Animator anim; // the animation controller connected to the player
    public GameObject currentPlatform; // the platform that the player stands on it
    public int SpellForce = 15000; // the force of shooting the spell
    public bool CanTurn = false; // if the player is allowed to turn it will be true
    public GameObject MetalSpell; // the spell game object made of metal
    public GameObject PaperSpell; // the spell game object made of paper
    public GameObject PlasticSpell; // the spell game object made of plastic
    Vector3 startPosition; // the starting position of the player
    Rigidbody rb; // the rigidbody component connected to the player
    public Transform SpellStartPos; // the position that the spell should start its movement
    public bool IsDead = false;
    private GameObject SelectedSpell; // the game object (spell) that is ready to be shoot
    public GameObject LookingPoint; // it indicated the point that the camera should look at
    void OnCollisionEnter(Collision other)
    {
        if (IsDead)
            return;
        if (Context.Data.DeadlyTags.Contains(other.gameObject.tag)) //if the other object is wall or fire
        {
            Context.Manager.Die();
        }
        else
            currentPlatform = other.gameObject; // if it is a platform
    }

    public void Die()
    {
        anim.SetTrigger("isDead"); //playing the death animation
        SoundManager.Instance.PlaySFX(SFX.dying);
        IsDead = true;
    }
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        startPosition = this.transform.position;
    }

    internal void StartMagic(Garbages spellType)
    {
        // first specifies the spell game object based on the user selection
        SelectedSpell = PlasticSpell;
        if (spellType == Garbages.Metal)
            SelectedSpell = MetalSpell;
        else if (spellType == Garbages.Paper)
            SelectedSpell = PaperSpell;
        if (!anim.GetBool("isJumping"))
            anim.SetBool("isMagic", true); //showing the magic animation
    }

    internal void StartJumping()
    {
        anim.SetBool("isJumping", true); // shows the jumping animation
        rb.AddForce(Vector3.up * 200); //sends the player up
        SoundManager.Instance.PlaySFX(SFX.jump); // sound effect
    }

    // Start is called before the first frame update
    void Start()
    {
        Context.Manager.Run();//starts making platforms
    }

    void CastMagic()
    {
        if (!currentPlatform)
            return;
        // shoots the selected spell in the forward direction
        SelectedSpell.transform.position = SpellStartPos.position;
        SelectedSpell.SetActive(true);
        SelectedSpell.GetComponent<Rigidbody>().AddForce(transform.forward * SpellForce);
        SoundManager.Instance.PlaySFX(SFX.magic);
        // hides the spell object after one second
        Invoke("KillMagic", 1);
    }
    public void FootStepLeft()
    {
        SoundManager.Instance.PlaySFX(SFX.footstep1); // sound effect
    }
    public void FootStepRight()
    {
        SoundManager.Instance.PlaySFX(SFX.footstep2); // sound effect
    }
    void KillMagic()
    {
        //hides the spell object
        KillMagic(PlasticSpell);
        KillMagic(MetalSpell);
        KillMagic(PaperSpell);
    }
    void KillMagic(GameObject obj)
    {
        //deactivates and stops a spell object 
        obj.SetActive(false);
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    void OnTriggerEnter(Collider other)
    {

        if (other is BoxCollider && GenerateWorld.lastPlatform.tag != "platformTSection" && (other.gameObject.tag.StartsWith("platform") || other.gameObject.tag.StartsWith("stair")))
            GenerateWorld.RunDummy(); // if the road is straight then keep generating it

        if (other is SphereCollider)
            CanTurn = true; // the sphere collider is located in the platform with two roads, so it means we can turn
    }

    void OnTriggerExit(Collider other)
    {
        if (other is SphereCollider)
            CanTurn = false;//when the way is straight turning is not allowed
    }

    void StopJump()
    {
        //stops jumping, it is called by an animation event
        anim.SetBool("isJumping", false);
    }

    void StopMagic()
    {
        //stops the magic animation, it is called by an animation event
        anim.SetBool("isMagic", false);
    }
    public void Turn(string direction)
    {
        //turns the player 90 degree to left or right
        int angle = (direction.CompareTo("right") == 0) ? 90 : -90;
        this.transform.Rotate(Vector3.up * angle);
        this.transform.position = new Vector3(startPosition.x,
                                            this.transform.position.y,
                                            startPosition.z);
        CanTurn = false;
    }

    public bool IsMagic { get { return anim.GetBool("isMagic"); } } //true if the player is casting a spell
    public bool IsJumping { get { return anim.GetBool("isJumping"); } } // true if the player is jumping

}