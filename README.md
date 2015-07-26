# dimensions
Dimensions game in progress

I'm thinking we name all functions and classes with captial letters and all variables internal to a class in all lowercase.
That will help with things like where we have an instance of the Player class called player.

There's now a Character class with basic movement controls for enemies and player.

It would be useful to do the tile check with a target position rather than just a little ahead as the player can jump through floors at the moment.

Check ground checks if character is embedded in ground, it does this in a way that makes Player bounce continually though.

The way player works on rotate needs fixing. Remember all the collisions are done in a non-rotated frame.
Current issues include the fact that the y axis increases in the opposite direction to x. Making horizontal movements go the wrong way once the screen is rotated.