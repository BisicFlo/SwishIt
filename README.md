Unity Version : Unity 6000.058f2  
Template : Universal 3D

------------------------------------------------
MonoBehaviour :

  Ball :  
	- Ball  
 	- BallSpawner  

  Shop :  
 	- ShopManager  
 	- ShopElement  

  Inputs :  
	- BaseInputManager  
	- Grab  
	- PlayerMovement  
	- Interactable  

  UI :  
	- DisplayMoney  
	- DisplayFPS  

  Other :  
	- TriggerEffect  
	- Destructible  
	- RotateSimple (Unused)  
	- Snapper   

  Debug :  
	- CapFPS  
	- QuitApplication	  
  
  ScriptableObject :  
  - BallData
  - PlayerData

------------------------------------------------
Tags Used : 
- Grabbable  
- Target  
- Interactable  
	
------------------------------------------------
New Unity Input System :

- Created "PlayerActions" (ImputActionAssets) -> And Assign as Project-wide Input Action

------------------------------------------------
Notes:

- Removed Screen Space Ambient occlusion (For VR compatibility)

- Using "PlayerImput" Component prevent Mouse Imputs to register events ?

- Unity Version Unstable /!\
