# dimensions
Dimensions game in progress

I'm thinking we name all functions and classes with captial letters and all variables internal to a class in all lowercase.
That will help with things like where we have an instance of the Player class called player.

Player.Movehor() or Level.Gettile() is wrong. The character can just walk through walls. It could be that it's GetTile positions aren't correct.
Player can't jump currently because of gravity and something needs to be done to stop a super fast character going through walls in a timestep