Roadmap: 

- [x] gh action for push to gh pages
- [ ] Set up the render streaming with a basic live animation. (Full unity --> webpage pipeline)
- [ ] run brain and get raw signals
- [ ] create basic unity environment for testing
- [ ] run a unity sensor body that has
	- depth vision
	- active boolean
	- food angle yaw
	- food angle pitch
	- food distance
	- a simple brain using these values to collect food via motor func: 
		- Walk force
		- Fly force
		- Turning value
		- Grounded bool
	(Serialized grounded friction) 


- [ ] replace unity body with fly brain
      
  (The hard part is going to be defining a sensible mapping between sensory inputs and motor outputs.)

	Unity (C#) <--> TCP Socket <--> Python Fly Brain

	Unity Sensors --> Fly Brain (Python) --> Motor Decoder --> Unity Rigidbody


Frontend: React + TypeScript + Vite

Backend: ??? Running a unity project with [UnityRenderStreaming package](https://github.com/Unity-Technologies/UnityRenderStreaming/blob/main/com.unity.renderstreaming/Documentation~/images/browser_hdrpscene.png
) to stream from backend server to webpage 
