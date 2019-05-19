using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // -------------------------------------------------- // Variables // -------------------------------------------------- //

    public enum CameraMode { ThirdPerson, Overhead, Static};
    private CameraMode mode;
    public CameraMode startingMode;

    private GameObject target;
    private Vector3 targetLastPosition;

    private float pitchSpeed;
    private float yawSpeed;

    private float pitch;
    private float pitchMin;
    private float pitchMax;
    private float yaw;
    private float yawMin;
    private float yawMax;
    private float roll = 0; // FIXED //
    private float distanceFromTarget;
    private float distanceMod;
    private float fov; //default set on camera
    private float fovMod;
    private float verticalOffset;
    private float horizontalOffset;
    private float nearClip = 0.1f;

    private bool autoCam;

    [Header("Invert Controls - Set Before Playing")]
    public bool invertControls;
    private int invertMod;


    // 3RD PERSON //
    [Header("Third Person")]

    private float tp_PitchMin = 0f;
    private float tp_PitchMax = 45f;
    public float tp_PitchSpeed = 1; //around local x
    public float tp_YawSpeed = 1; //around world y
    public float tp_DistanceFromTarget = 5;
    public LayerMask obstacles;


    // OVERHEAD //
    [Header("Overhead")]

    public float o_xMin;
    public float o_xMax;
    public float o_yMin;
    public float o_yMax;
    public float o_xSpeed = 0.2f; //along local x
    public float o_ySpeed = 0.2f; //along local y
    public float o_DistanceFromTarget = 10f;


    // STATIC //

    private GameObject[] locations; //the gameobjects to cycle through
    private int location;
    private bool updateLocation;


    // -------------------------------------------------- // Startup // -------------------------------------------------- //

    private void Awake () {

        //Inverted Controls
        invertMod = (invertControls) ? -1 : 1;

        //Finding all static cameras
        locations = GameObject.FindGameObjectsWithTag("Static Camera");

        //Set the starting mode for the camera
        mode = startingMode;

        target = GameObject.FindGameObjectWithTag("Player");
        targetLastPosition = target.transform.position;
        verticalOffset = target.transform.position.y;

        fov = GetComponent<Camera>().fieldOfView;

        setupCam();

    }

    // -------------------------------------------------- // Frame by Frame // -------------------------------------------------- //

    private void Update () {
        
        switch (mode) {

            case CameraMode.ThirdPerson: // Aiming Phase & Rolling Phase
                
                 
                Vector3 rotationVector, positionVector;

                //dampening controller sensitivity
                float hAxis = Input.GetAxis("Horizontal") * Mathf.Abs(Input.GetAxis("Horizontal"));
                float vAxis = Input.GetAxis("Vertical") * Mathf.Abs(Input.GetAxis("Vertical")) * invertMod;
                
                
                // Deadzone Check
                if (Mathf.Abs(hAxis) < 0.1) {
                    hAxis = 0;
                }
                if (Mathf.Abs(vAxis) < 0.1) {
                    vAxis = 0;
                }

                //vertical dampening speed
                if (vAxis > 0) {
                    vAxis = Mathf.Min((pitchMax - pitch)/2, vAxis);
                } else if (vAxis < 0) {
                    vAxis = Mathf.Min((pitch - pitchMin)/2, vAxis);
                }

                pitch = Mathf.Clamp(pitch + vAxis * pitchSpeed, pitchMin, pitchMax);
                yaw += hAxis * yawSpeed * -1;

                rotationVector = new Vector3(pitch, yaw, roll);

                //set pitch yaw etc and apply them to the camera values (Quaternion.eulerAngles)
                transform.rotation = Quaternion.Euler(rotationVector);

                /* as pitch increases increase distanceFromTarget, vertical offset, and FOV */
                distanceMod = (pitch > 0) ? pitch / 30f : 0;
                verticalOffset = (pitch / 30f);
                fovMod = (pitch > 0) ? pitch / 5f : 0;

                //get the vector from the target to the camera
                positionVector = transform.forward.normalized * -1 * (distanceFromTarget + distanceMod);

                //apply offsets
                positionVector.x += horizontalOffset;
                positionVector.y += verticalOffset;

                positionVector = new Vector3(target.transform.position.x + positionVector.x, target.transform.position.y + positionVector.y, target.transform.position.z + positionVector.z);

                GetComponent<Camera>().fieldOfView = fov + fovMod;

                RaycastHit raycastToCam;

                //transform
                if (Physics.Linecast(target.transform.position, positionVector, out raycastToCam, obstacles)) {
                    Vector3 temp = raycastToCam.point * 0.87f;
                    transform.position = temp;
                } else {
                    transform.position = positionVector;
                }

                //Debug.DrawLine(target.transform.position, positionVector, Color.green);

                break;
            case CameraMode.Overhead: // Aiming Phase (& Player Swap?)
                
                // PLAYER INPUT //
                float xAxis = Input.GetAxis("Horizontal") * Mathf.Abs(Input.GetAxis("Horizontal"));
                float yAxis = Input.GetAxis("Vertical") * Mathf.Abs(Input.GetAxis("Vertical"));

                pitch = Mathf.Clamp( pitch + xAxis * pitchSpeed, pitchMin, pitchMax);
                yaw = Mathf.Clamp(yaw + yAxis * pitchSpeed, yawMin, yawMax);

                transform.position = new Vector3( pitch, distanceFromTarget, yaw);


                break;
            case CameraMode.Static:

                if (updateLocation) {
                    transform.position = locations[location].transform.position;
                    transform.eulerAngles = locations[location].transform.eulerAngles;
                    updateLocation = false;
                }

                break;
        }
    }
    

    // -------------------------------------------------- // Static Methods // -------------------------------------------------- //

    public void switchMode () {
        switch (mode) {
            case CameraMode.ThirdPerson:
                Debug.Log("Switching to Overhead Cam");
                mode = CameraMode.Overhead;
                break;
            case CameraMode.Overhead:
                Debug.Log("Switching to Static Cam");
                mode = CameraMode.Static;
                break;
            case CameraMode.Static:
                Debug.Log("Switching to Third Person Cam");
                mode = CameraMode.ThirdPerson;
                break;
        }
        setupCam();
    }

    public void switchMode (CameraMode m) {
        mode = m;
        setupCam();
    }

    private void setupCam() {
        switch (mode) {
            case CameraMode.ThirdPerson:
                distanceFromTarget = tp_DistanceFromTarget;
                pitchSpeed = tp_PitchSpeed;
                yawSpeed = tp_YawSpeed;
                pitchMin = tp_PitchMin;
                pitchMax = tp_PitchMax;
                targetLastPosition = target.transform.position;
                verticalOffset = target.transform.position.y;

                pitch = 0;
                yaw = 0;

                break;
            case CameraMode.Overhead:
                distanceFromTarget = o_DistanceFromTarget;
                pitchSpeed = o_xSpeed;
                yawSpeed = o_ySpeed;
                pitchMin = o_xMin;
                pitchMax = o_xMax;
                yawMin = o_yMin;
                yawMax = o_yMax;

                //position above target (player) and aim straight down
                transform.position = new Vector3(target.transform.position.x, distanceFromTarget, target.transform.position.z);
                transform.eulerAngles = new Vector3( 90, 0, 0);

                pitch = transform.position.x;
                yaw = transform.position.z;

                GetComponent<Camera>().fieldOfView = fov;

                break;
            case CameraMode.Static:
                location = 0;
                updateLocation = true;

                GetComponent<Camera>().fieldOfView = fov;

                break;
        }
    }
    
    public void cycleCamera (string s) {
        switch (mode) {
            case CameraMode.ThirdPerson:

                //cycle between the players

                break;
            case CameraMode.Static:

                if (locations.Length < 2) {
                    return;
                }

                if (s == "right") {

                    location++;
                    if (location == locations.Length) {
                        location = 0;
                    }

                } else if (s == "left") {

                    location--;
                    if (location == -1) {
                        location = locations.Length - 1;
                    }

                }

                updateLocation = true;

                break;
        }

    }

    public void focusCamOnPlayer (GameObject g) {
        target = g;
    }


    // -------------------------------------------------- // ~ END ~ // -------------------------------------------------- //
}
