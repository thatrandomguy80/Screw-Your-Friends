# Screw- Your- Friends
unity game prototype in C#

##How to Play
Controls are A,D for left-right, W for jump and hold S for ground smash.
Maps can be added by adding their UNCOMPRESSED PNG to maps folder and ENSURING their import setting match the test maps.
Map code: change each pixels blue value to either of the following to get the corrisponding prefab
- 0-Basic Tile
- 25-end map gate
- 50-Breakable tile
- 100-Coin
- 150-Spawner
- 200-Check Point(for respawning)
- 250-end map gate
- 
##Basic concept
Screw- Your- Friends is a 2D percision platformer designed to be multiplayer. Each player will race towards a goal and leading players can risk thier flow and speed to set off traps
such as ground smashing to break the floor. This will cause that player to stumble but may increase their lead if used correctly.
Players movement in this game is key as moving the most efficent way will allow the player to get ahead. jumps will move slowly horizontally so having a well timed jump and controlling
the jump height(like super meat boy) to give the minimum air time will be one of the stronger skills within the game.

##Current State

###features
- Map editor first pass: still need to code in blue color values but is working and allowing map switching within the same scene(may not need offsets anymore since
spawner object is now in the map).
- Basic movement still needs polish
- breakable floor: attempting to have the neighbouring left and right blocks break in a 1by1 fashion but they hold each other up at the moment.(add force?)
- coins added: just used as score adder at the moment.
- Game state manager
- nextMap loading.

###Bugs
- colliders for tiles creating bad floor movement.
- landing feels sluggish see above?
- coins spawning strangly


##TODO:
- coins effecting movement
- polish breakable floor
- polish movement before adding sliding and finite jump controls
- better color allocation for map loader.
- better isGrounded function.
- encaptulation of movement
- split controls from movement?

##Goals
- Movement system that allows finite movement to feel smooth but fit well with the flow system.
- Map Creation tool: Using image editing to layout the map and then loading the image and creating the map using prefabs that corrispond to the color pixels of the map.
- CMV Design and encaptulation.
- Breakable floors with crumble animation
- working character rig will animations for: ground smash, slide, running, jumping, idle.
- quick respawn system for section of the map(game should continue until first player has finished + some timer) this will allow mistakes to be recovered and build tension
- eventual multiplayer
- power up coins: will add to speed or something along those lines
- button based obstical creation: Something like slow down to press a button and a block will fall from the ceiling behind you(high risk/reward with close trailing player
 probably able to slide under it) 
- Sliding under blocks will require speed in order to make the "gap" so more flow required and if you can't make it past you will respawn behind it.
- Camera controller(follows player(s) smoothly)

##Stretch Goals
- Ability to slide on sloped sections to gain speed when timing this mechnic correctly.
- online/networked multiplayer.
