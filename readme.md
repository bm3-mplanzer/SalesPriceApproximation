# Scenario
Assume you want to buy four items in a supermarket. If you add the prices together, you get CHF 7.77. If you multiply the prices, you also get CHF 7.77. How much did the Items cost?
# How to solve this?
Well, by trying out all possible combinations that add up to CHF 7.77 \
Or if you want to solve this in more mathematical ways, you can assume two numbers and guess the other two, then get as close as possible. \
$2+x_1+x_2 = 7.77$ \
$x_1 \cdot x_2 = 7.77$ \
(a few tries later) \
$L = \{0.8, 1.25, 3.5, 2.22\}$ \
It might be a bit hard to solve this by hand, because the smallest possible price is CHF 0.01 
# Is there a way to automate this process?
Yes, the program I wrote.
It automatically searches for all possible combinations, it prints out the first one it finds.
The program converts all decimals to integers, because we want to work with accurate numbers wherever possible.
# Demo
```
What number do you want to get? (7.77 by default)
7.77
How many items are there? (4 by default)
4
Closest match: 7.77
Possible Combinations:
0.8, 1.25, 2.22, 3.5
0.8, 1.25, 3.5, 2.22
0.8, 2.22, 1.25, 3.5
0.8, 2.22, 3.5, 1.25
0.8, 3.5, 1.25, 2.22
0.8, 3.5, 2.22, 1.25
1.25, 0.8, 2.22, 3.5
1.25, 0.8, 3.5, 2.22
1.25, 2.22, 0.8, 3.5
1.25, 2.22, 3.5, 0.8
1.25, 3.5, 0.8, 2.22
1.25, 3.5, 2.22, 0.8
2.22, 0.8, 1.25, 3.5
2.22, 0.8, 3.5, 1.25
2.22, 1.25, 0.8, 3.5
2.22, 1.25, 3.5, 0.8
2.22, 3.5, 0.8, 1.25
2.22, 3.5, 1.25, 0.8
3.5, 0.8, 1.25, 2.22
3.5, 0.8, 2.22, 1.25
3.5, 1.25, 0.8, 2.22
3.5, 1.25, 2.22, 0.8
3.5, 2.22, 0.8, 1.25
3.5, 2.22, 1.25, 0.8
```
