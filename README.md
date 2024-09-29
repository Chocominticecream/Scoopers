# Scoopers
 A project made for unity game jam 2024 by tooling gg
 
 A game in which you attempt to expand your ice cream tower by piling ice cream

15/9/2024 - brainstormed basic concepts then coded basic physics , will implement game mechanics tomorrow(sticking to other ice cream + ice cream stats)

16/9/2024 - basic gameplay mechanics implemented! Now Just have to apply meta layers! I could also choose whether to iterate upon the meta layers or go for art and polish 
after creating three basic ice cream objects, might wanna go for art and polish tbh

17/9/2024 - my depression is acting up again, only managed to create art and basic ice cream objects...

18/9/2024 -  created even more art and made some basic UI (reused from last project), also added a bit of polish like ui animations and camera panning etc, added timer and scoring system

19/9/2024 -20/9/2024 -  fully implemented shop system + other mechanics, just need some tweaks here and there and a bit of art and i should be done!

21/9/2024 - finally finished the game!

Game finished in 6th place!
https://itch.io/jam/unity-jam-2024-cash-prizes/rate/2985441

This game may be updated just to include changes proposed below, but I may not update it consistently as I am moving to another project

---
### Task List:

TODO:
- ~~basic physics (clamping, sticking to bowl etc)~~
- ~~stick to other ice cream based on rerolling an rng every second and checking if number is below a certain value~~
- ~~make ice cream scoops resizabl depending on how long you hold onto a tray and find a way to calculate height of ice cream stack~~
- ~~shift camera upwards when successfully sticking ice cream to a certain height (remember to adjust the clamping method so that it stops clamping if iskinematic is true)~~
- ~~create more mechanics such as stickiness/value/scoopability/creaminess, these values affect how high the tower can stack and create 3 basic ice cream scriptableobjects~~ 
- ~~scoring/money system~~
- ~~delete all ice creams that are not sticking at the end of the timer~~
- ~~main menu~~
- ~~art + polish~~
- ~~shop system!~~
- Shop todo:
   - ~~money system~~
   - ~~lock ice creams that are locked and you have to pay for them~~
   - ~~option to buy more seconds on the timer~~
   - ~~more transitions + more art for the ice creams~~
   - ~~calculate height and results~~ (just height only, i have no time to fine tune additional mechanics)
   - ~~attributes~~

other stuff:
- change the ice cream sticking algorithm to roll to a set amount of seconds instead of RNG

BUGS (those that i cannot recreate exactly at least)
- ~~ice cream will get flung suddenly when placing it on ice cream~~
- ice cream sometimes reverts to idle state, possibly caused when in contact with another ice cream when in attempttostick state
- ice cream tray sometimes spawn multiple ice creams when mouse down is held(cant tell if its hardware problem or software problem)
- ~~Performance intensive bug causes scooping ice cream to be much slower in the web build, to fix this , maybe invoke repeating can be used (IMPORTANT)~~

post jam tasks(maybe i will do it, maybe i won't)
- refactor the code so I can actually reuse it for future projects (im looking at the shopUI code)
- ~~second note on refactoring shop code, make selecting items in the shop decided by a boolean statement instead of a state machine~~
- rebalance mechanics as some playtesting with family members revealed that the game isn't fun in some areas (too hard, no goals)
- small UI/UX tweaks based on recommendations from game jam playtesting
(delay between switching ice cream trays, reveal cost on ice cream logos in shop)

---
### Screenshots/other art:

![promotionimage3](https://github.com/user-attachments/assets/5565de4d-d7c2-4784-935d-230374b66da5)

![promotion1](https://github.com/user-attachments/assets/6860fb0e-2ad0-421c-9835-6dc05f6a4d64)

![promogif3](https://github.com/user-attachments/assets/d7723fda-a26a-4551-9086-a331c8c06125)

![promotionimage](https://github.com/user-attachments/assets/28d4caba-1684-40b8-a6dd-64b8139640af)





