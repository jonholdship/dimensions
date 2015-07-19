# dimensions
Dimensions game in progress

I'm thinking we name all functions and classes with captial letters and all variables internal to a class in all lowercase.
That will help with things like where we have an instance of the Player class called player.

Enemy.movehor() has issues. One thing I've done a bit is have level.cs turn tiles red (ie =2) when they get checked for collisions. That way you can check you've tested what you think you have. The Enemy checks always seem to be one tile too many. If you make it check one less, the player is wrong instead. I'm pretty sure player and enemy are the same size so this is baffling. 
IF the checks are right, the logic for movehor is wrong.

It would be useful to have a 50x50 working array which acts as the camera and store the rest of the array for later use. We can rotate/flip the working array and leave the rest until it is required. Alternatively (and maybe better) is to have a camera which only shows 50x50 tiles and can rotate in multiplies of 90 degrees. Player gravity can be a function of camera angle and everyone else can use proper x and y co-ords.