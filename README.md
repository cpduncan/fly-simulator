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

```
fly-project/
|
|__ frontend/
|   |__ React
|
|__ backend/
|   |__ FastAPI
|
|__ unity/
|   |__ Unity project
|
|__ brain/
|   |__ Python fly brain
|
|__ README.md
```

### Backend Choice

Top choice: Oracle Cloud Free Tier


### Vertical Architecture
```
GitHub Pages + React
     |	
FastAPI & Websocket info + WebGL Viewer ->Rendering	}
     |		                                        } via Oracle Cloud "Always Free" Tier 
Python & Unity Headless                             }
```

#### FastAPI 

The FastAPI exposes these for the react app to call the following info. Basically an info grabber.  
https://myflysite.com/api/status
https://myflysite.com/api/current-generation
https://myflysite.com/api/top-fly
https://myflysite.com/api/statistics
