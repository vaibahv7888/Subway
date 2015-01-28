using UnityEngine;
using System.Collections;

public class StationScript : MonoBehaviour {
	
		Ray ray;									//The hit ray
		RaycastHit hit;								//The hit raycast
	
	  public int lengthOfLineRenderer = 20;
	
	
	public Color c1 = Color.yellow;
    public Color c2 = Color.red;
	
	public Camera maincamera;

	
	public Transform barPrefab;
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonDown(0))
		{
			
			
			
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out hit, 50 ) )
			{
			
				if( hit.collider.transform == this.transform )
				{
			
					LineRenderer lineRenderer = GetComponent<LineRenderer>();
					lineRenderer.enabled= true;
			
		       		 int i = 10;
		       		 while (i < lengthOfLineRenderer)
					{

					
							Vector3 pos = maincamera.ScreenToWorldPoint(Input.mousePosition);
					
				            lineRenderer.SetPosition( 0, pos  );
					
							print ("Check = "+Input.mousePosition);
				
					
				            i++;
						
					}	
		        }
			}
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			LineRenderer lineRenderer = GetComponent<LineRenderer>();
			lineRenderer.enabled= false;
		}
	}
}
