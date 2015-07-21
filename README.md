# dimensions
Dimensions game in progress

I'm thinking we name all functions and classes with captial letters and all variables internal to a class in all lowercase.
That will help with things like where we have an instance of the Player class called player.

GetTile() is kind of sensitive to where objects are placed. Ultimately want it to check the pixels down one edge of the object and return the value of the tile they are in. 

It would be useful to have a 50x50 working array which acts as the camera and store the rest of the array for later use. We can rotate/flip the working array and leave the rest until it is required. Alternatively (and maybe better) is to have a camera which only shows 50x50 tiles and can rotate in multiplies of 90 degrees. Player gravity can be a function of camera angle and everyone else can use proper x and y co-ords.