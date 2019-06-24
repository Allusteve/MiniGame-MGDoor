using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public float xMargin;
	public float yMargin;
	public float xSmooth;
	public float ySmooth;
	public Vector2 maxXAndY;
	public Vector2 minXAndY;
    private Vector3 offset;
    private float yOffset;

    public Material LOSMaskMaterial;

    private Transform player;

	void Awake ()
	{
        offset = new Vector3(0, 0, -5);
        yOffset = 2.2f;
		player = GameObject.Find("Player").transform;
        transform.position = player.transform.position + offset;
        Vector3 currentP = transform.position;
        currentP.y += yOffset;
        transform.position = currentP;
    }

	bool CheckXMargin()
	{
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}

	bool CheckYMargin()
	{
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}

	void FixedUpdate ()
	{
		TrackPlayer();
	}
	
	void TrackPlayer ()
	{
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		if(CheckXMargin())
			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

		if(CheckYMargin())
			targetY = Mathf.Lerp(transform.position.y, player.position.y + yOffset, ySmooth * Time.deltaTime);

		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, LOSMaskMaterial);
    }
}
