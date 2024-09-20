# Scoopers
 A project made for unity game jam 2024 by tooling gg
 
 A game in which you attempt to expand your ice cream tower by piling ice cream

15/9/2024 - brainstormed basic concepts then coded basic physics , will implement game mechanics tomorrow(sticking to other ice cream + ice cream stats)

16/9/2024 - basic gameplay mechanics implemented! Now Just have to apply meta layers! I could also choose whether to iterate upon the meta layers or go for art and polish 
after creating three basic ice cream objects, might wanna go for art and polish tbh

17/9/2024 - my depression is acting up again, only managed to create art and basic ice cream objects...

18/9/2024 -  created even more art and made some basic UI (reused from last project), also added a bit of polish like ui animations and camera panning etc, added timer and scoring system

TODO:
- ~~basic physics (clamping, sticking to bowl etc)~~
- ~~stick to other ice cream based on rerolling an rng every second and checking if number is below a certain value~~
- ~~make ice cream scoops resizabl depending on how long you hold onto a tray and find a way to calculate height of ice cream stack~~
- ~~shift camera upwards when successfully sticking ice cream to a certain height (remember to adjust the clamping method so that it stops clamping if iskinematic is true)~~
- create more mechanics such as stickiness/value/scoopability/creaminess, these values affect how high the tower can stack and create 3 basic ice cream scriptableobjects 
- ~~scoring/money system~~
- art + polish
- ~~shop system!~~
- Shop todo:
   - ~~money system~~
   - ~~lock ice creams that are locked and you have to pay for them~~
   - ~~option to buy more seconds on the timer~~
   - more transitions + more art for the ice creams
   - ~~calculate height and results~~ (just height only, i have no time to fine tune additional mechanics)
   - ~~attributes~~

other stuff:
- change the ice cream sticking algorithm to roll to a set amount of seconds instead of RNG

BUGS (those that i cannot recreate exactly at least)
- ~~ice cream will get flung suddenly when placing it on ice cream~~
- ice cream sometimes reverts to idle state, possibly caused when in contact with another ice cream when in attempttostick state
- ice cream tray sometimes spawn multiple ice creams when mouse down is held
