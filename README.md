implementation of grammar:
S-->T+S | T
T-->E*T | E/T | E
E-->-F | F
F-->(S) | a
