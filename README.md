# Game Experiment

This is a project built to experiment with game development in C# with a focus
on simulation and Procedural Content Generation (PCG). It is built on the
MonoGame framework, and is intended to run on Windows and be portable to at
least Android. Secondary goals are to have reasonably decent pixel graphics
and an interesting simulation system.

## Architecture

The initial design is meant to be something between Model View Controller (MVC)
and Model View View-Model (MVVM). The names of the modules have been changed to
better reflect each parts intended function:

+ Simulation - This is the model, in charge of game objects and their interactions.
+ Display - This is the view, in charge of rendering graphics to the screen.
+ Director - This module connects the simulation to the display.

### Simulation

The simulation models the world and everything in it, and contains all logic
for updating the world. It keeps track of all the maps, tiles, and entities,
managing their creation, deletion, and update. Actors in the simulation are
built using a Component Entity System, and the simulation handles all system
executions.

### Display

The display handles the actual drawing of everything to the screen, and
processes raw user input. The display contains a handle on the physical display
(the screen), and maintains a stack of "views" which define what is shown on
the screen. Views are drawn from bottom to top, and the topmost view defines
the active input mapping.

### Director

The director translates between the simulation models and the display data. The
director also mediates all modifications to the simulation model and informs
the display of changes to the simulation model.

## Content

The sprites used in this project are primarily [Andrew Rios' DawnLike](http://opengameart.org/users/dragondeplatino)
using DawnBringer's 16 color palette. Some supplemental tiles were taken from
[Lanea Zimmerman's Tiny 16X16](http://opengameart.org/users/sharm).

## License

This project is licensed under the 3 Clause BSD License - see the
[LICENSE.txt](LICENSE.txt) file for details

