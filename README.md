## Roadmap (26-10-08):

### General

- [x] create basic unity environment for testing
- [x] integrate the unity sim front and backend on the server and site
- [x] websocket setup for positional info
- [ ] API setup for generational info
- [ ] run brain and get raw signals after probing with basic inputs

Fly Senses

- [ ] run a unity sensor body that has
  - Close-Vision (Depth Camera)
  - Olfactory Target Sensing (normalized yaw and pitch triggers)
  - food distance

Fly Actions

- [ ] expose unity actions and movement for python to interact with
  - Move Force
  - Move Vector3 (Normalizes to surface plane while grounded)
  - Grounded bool (and Serialized grounded friction) (can ground to any nearest surface)
  - Turning value
  - Bite action

- [ ] replace rudementary pathfinding with Janelia

## Architecture

_Frontend : React, Vite, TypeScript, Static Chakra Components, Unity+WebGL rendering_

_Backend : Oracle Cloud server hosting a headless Unity simulation and training a Spiking Neural Network_

```
# Simulation Loop
#
#        (--------- backend ---------)       (--- frontend ---)
#
#         Unity               SSNTorch             Browser
#           |                    |                    |
#    [Read Senses]               |                    |
#           |--- Sensory In ---->|                    |
#           |              [Step Brain]               |
#           |<-- Move/Actions ---|                    |
#    [Step Physics]              |                    |
#           |                                         |
#           |-- Push Positions Via Websocket -------->|
#                                           [Render Simulation]
#
```
