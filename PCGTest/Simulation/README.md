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

### Representation

Individual map cells have two slots: Floor & Fill, which describe the cells
properties. The Floor describes the bottom of the cell and what is below it
(the distance from the bottom of one cell to the top of the cell below is
non-zero). The Fill describes any obstructions above the floor (including
entities). Cells additionally have a contents list, where contents are anything
inside the cell volume which is not large enough to obstruct vision or
movement.

Floor materials are chosen from a relatively small set, so they can be
enumerated once and referenced by an integer index. Examples are: void, water,
rock, dirt, grass, etc.

Fill can be either a material type, indexed to the same data as the floor
materials, or an entity reference. Entities are referenced by their unique id
number, and their properties are determined by their component data.

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

## Persistence

The simulation is built of two parts: places and things. Places in memory are
stored in chunks, which are effectively 3d arrays of integers. Because
multi-dimension arrays can be trivially linearized storing chunk data should be
straight forward. Storage space can be reduced by computing and storing a diff
from the generated chunk. Things, i.e. entities, are generated with a unique
identifier. When unloaded entities are written to a file named with the entity
id. When a chunk is loaded that references an entity, and the id isn't present
in-memory, then the file is read and the entity is reconstituted.

