    #pragma strict
     
    var pos1 : Vector3;
    var pos2 : Vector3;
    var objectHeight = 2.0; // 2.0 for a cylinder, 1.0 for a cube
    
    
    	var ray : Ray ;									//The hit ray
		var hit : RaycastHit;								//The hit raycast
		var hitRelease: RaycastHit;
	var goOnce : boolean;
     
    function Update () 
    {
     
	    if (Input.GetMouseButtonDown(0)) 
	    {
	    
	    	ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, hit, 50 ) )
			{
			
				if( hit.transform.tag=="station")
				{
						pos1 = Vector3(Input.mousePosition.x,(Input.mousePosition.y) , 12 );
					    pos1 = Camera.main.ScreenToWorldPoint(pos1);
					    pos2 = pos1;
				}
			}		    
	     
	    }
	     
	    if (Input.GetMouseButton(0)) //controll goes in when mouse is hold
	    {
	    	
	    	if (Physics.Raycast(ray, hit, 50 ) )
			{
		    	if( hit.transform.tag=="station")
					{
						pos2 = Vector3(Input.mousePosition.x,(Input.mousePosition.y) , 12 );
					    pos2 = Camera.main.ScreenToWorldPoint(pos2);
		     		}
		     }		
	    }
	    
	     

	     
	    if (pos2 != pos1) 
	    {
		    var v3 = pos2 - pos1;
		    transform.position = pos1 + (v3) / 2.0;
		    transform.localScale.y = v3.magnitude/objectHeight;
		    transform.rotation = Quaternion.FromToRotation(Vector3.up, v3);
		    
		    if(!goOnce)
		    {	
//		    	GameObject.Find("NewController").GetComponent(typeof(NewController)).isMoveTrain =true;
		    }
	    }
    
    
    	
    
    
    }
    
   function OnTriggerEnter (other : Collider) 
    {
    	print("trigger "+other.transform.name);
 	}   
    