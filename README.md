# dimensions
Dimensions game in progress

I'm thinking we name all functions and classes with captial letters and all variables internal to a class in all lowercase.
That will help with things like where we have an instance of the Player class called player.

player.checkground() or player.movever() are slightly off. If you hit a wall mid-jump, it is counting the wall pixel as a pixel to stand on. Setting an checked pixel to equal 2 in level was useful before to see which pixels are getting checked. Can see if only the pixels directly under the box inhabited by player are being checked and not one slightly to the side.

level.flip() and/or level.rotate() need to be created. these would allow the player to flip or rotate the level. player.action() would be a useful function to store all the none movement player actions. this can check for button presses that alter level etc.