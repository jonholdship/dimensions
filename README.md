# dimensions
Dimensions game in progress

I'm thinking we name all functions and classes with captial letters and all variables internal to a class in all lowercase.
That will help with things like where we have an instance of the Player class called player.

We need to introduce a Level class.
	Level would hold an array (3D) of integers which represent tiles that make the level. 0 for empty space, 1 for solid blocks, 2 for lava etc. It would also have a 1D array of tile images.
	Level could then do two things:
	-Draw the tiles the are required. so take the 2d slice we can currently see of level and draw it. This could be done by simply selecting the correct tile image based on the integer in the level array. For example if it found a 2 in it's level array, it would draw tiles[2] which is lava. We'd need to work out if it should draw all of the current slice and then have a separate camera decide what's on screen or if it should only draw what is required. one will be more efficient i'm sure.

	-Secondly, level could have a public function which, given a rectangle or point, could return the integer of the tile in that position. so if the level is 500x500 pixels with 50x50 tiles, it could divide the co-ordinates of the point it is given by 10 to get the current tile. EG 370,200 become 37,20 and if that element of the level array was lava, it would  return a 2. Then we can build into the update part of player to ask level where it is and to do things based on what number is returned: die on 2, stop on 1 (so you don't go through walls) and do nothing on 0.