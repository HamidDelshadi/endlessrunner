using UnityEngine;

public class Deactivate : MonoBehaviour {
    /// <summary>
    /// Deactivates platforms in a certain amount of time after passing the player
    /// </summary>
    bool deactiveScheduled = false;
    public float DeactivateTime = 4.0f;
    public void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Player" && !deactiveScheduled) { //if player passed it
            Invoke("SetInactive", DeactivateTime); // deactivates after a ceratain amount of time
            deactiveScheduled = true;
        }
    }
    void SetInactive() {
        deactiveScheduled = false;
        this.gameObject.SetActive(false);
    }
}
