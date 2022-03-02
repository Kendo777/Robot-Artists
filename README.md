# Robot-Artists

Implement a system that simulates a system with 3 ‘robot artists’. In this system we have 3 different
robot artists: robot A, robot B, and robot C. All robots can only use 3 colors to paint their pictures: red,
green and blue, but they don’t use all of them:

- Robot A likes red a lot but doesn’t like blue, so he uses 2 buckets of red and 1 of green to paint
its pictures.
- Robot B likes blue a lot but doesn’t like red, so it uses 2 buckets of blue and 1 of green.
- Robot C uses 1 bucket of each color (red, green and blue) to paint.

To paint, the robots mount the paint buckets in their positions and then they start painting. A robot
won’t mount the buckets unless it has all the buckets it needs. That is, for instance, Robot A won’t start
mounting unless there are 2 red buckets and 1 green. Once they start mounting the buckets other robot
can’t get them. If a robot doesn’t have enough buckets to paint it will wait till there are more (it will recheck every 250 millisecons).

Mounting the buckets takes a random time between 500 and 1000 milliseconds. Then painting the
picture takes a random time between 2 and 4 seconds.

Besides the 3 robots there’s a fourth robot that brings the paint to these artists. It goes and gets a
random number between 3 and 5 buckets of paint for each color. That is, in one go he may bring 3
buckets of red, 5 of green and 4 of blue, and the next one 4 of red, 3 of green and 5 of blue, etc. It’s not
the same amount of buckets for each color. This robot brings new buckets of paint every 10 seconds to
the painter robots.

When the user presses the return key all robots will wait till their current picture is finished and they
will stop working. If they are not working on a picture at that time they’ll quit then. The transporter
robot finishes immediately. The app will print a message when all robots have finished.

After the message saying all robots are finished, if the player presses a key, the app will end. 
