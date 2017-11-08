BFHLMapListGenerator
====================

This is a utility to generate RANDOM Battlefield Hardline map lists for server
admins. You can generate human readable lists for manual entry into 
admin tools such as procon, or you can generate maplist.txt style lists
to copy and paste directly in to your server's maplist.txt.

NOTE: If you extracted the new version over an older version of the program,
please delete the file BF4MapListGenerator.dat, otherwise you may have
errors when launching it.

Known Issues with current release:
- None

Changelog:
14 May 2015:
 - Initial Release
10 November 2015:
 - Add support for the Criminal Activity and for Robbery expansions
 
NOTE: THIS IS BETA SOFTWARE.
DISCLAIMER OF WARRANTIES AND LIMITATION OF LIABILITY

The software is supplied "as is" and all use it as your own risk. The software
author / distributor disclaims all warranties of any kind, either express or 
implied, as to the software, including but not limited to, implied warranties
of fitness for a particular purpose, merchantability or non-infringement of 
proprietary rights. Neither this agreement nor any documentation furnished 
under it is intended to express or imply any warranty that the operation of the
software will be uninterrupted, timely, or error-free.

Under no circumstances shall the software author / distributor be liable to any
user for direct, indirect, incidental, consequential, special, or exemplary 
damages, arising from or relating to this agreement, the software, or user's 
use or misuse of the software or any other services provided by the software 
author / distributor. Such limitation of liability shall apply whether the 
damages arise from the use or misuse of the software or any other services 
supplied by the software author / distributor (including such damages incurred 
by third parties), or errors of the software.

How to use it:
==============
This may not be immediately obvious from the interface layout, but I'll
try to keep it simple.

Unselect the maps that you do NOT want to appear in the generated list.
Via the Map Selection menu, you can quickly select which expansion packs
you want to include or exclude from selection.
The generated map list is governed by the pattern list (the right hand 
list). Select the game type pattern that you want to appear in the 
output list (as well as the desired number of rounds).

For example, to generate a typical Conquest 1 round, Rescue 2 round map
list, you would simply add a gametype of Conquest, 1 round, and 
Rescue, 2 rounds to the pattern.

Ensure the infinite checkbox (∞) is selected (very bottom checkbox 
between the lists with the infinity symbol), and on the menu, click 
Generate->maplist.txt or Generate->Human Readable. A random map list 
will then be generated based on the defined pattern until all maps are 
used.

Note that if you select a game type that only has a few maps (like 
hotwire), and a conquest game type, the hotwire maps will be continually
repeated until there are no more conquest maps left, unless you select the
top checkbox (≠∞). In that case, if there are no more maps left for a 
given game type, they will not be repeated, and only maps from the 
remaining game types will be used.

You can unselect the Infinite checkbox and specify how many times
you want the pattern to repeat instead, from once, to 100 times.

I've tried to avoid having the same two maps of different game types
following one another, but it is difficult to test every single 
scenario.

If you're unhappy with the distribution of a generated list, just 
generate a new one, or edit the generated list.