# Simulation design

The root of the simulation is the `SimulationManager`. The SimMan contains the
noise sources used to build the world, the simulation's datetime, the loaded
map data, and all the loaded entities. The SimMan provides methods for
interacting with the simulation, and event-streams generated from simulation
activity.

## Map system

The map is an effectively infinite 3d grid of cells divided into cubic chunks.
Individual chunks are loaded/created on demand, and changes are persisted to
disk when chunks are unloaded. Chunk generation is driven by multiple noise
sources which are sampled at different scales to determine climate/biome,
terrain topology, structure/prefab placement, inhabitants and items.

Although the map data structure is 3d the generated maps will be largely flat.
The third dimension will be primarily used similarly to dungeon levels in other
roguelikes. Vertical structures can be generated both above and below the
ground level. Examples are: multi-story buildings with basements, open pits,
mountains, large trees, and natural caves.

## Entity System

Entities are generic containers for collections of data (components) that
describe the entity and direct its behaviour. Entity behaviours are implemented
by systems that operate on component data and interact with the simulation
world. Entities model both "static" items in the simulation as well as
"actors", with the difference being simply which components are added to an
entity.

## Simulation execution

Simulation time is represented in integer units of time called `Tick`s. The
SimMan exposes a step method that advances the simulation time a fixed number
of ticks, or until a predicate is satisfied. Each active entity in the
simulation contains a count-down timer which is used to order entity updates.
Each tick all timers are decremented. When any entity's timer reaches zero it
can then perform some action, which in turn adds some number of ticks back to
the entity's timer.
