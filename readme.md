## A New Post

## Types of Number Systems

A number system is a combination of the following:

### ordered set of symbols:

Alphabet: {s<sub>1</sub>,s<sub>2</sub>,...s<sub>n</sub>}

### Zero-Collapse Property

In typical number systems the zero symbol, s<sub>1</sub>, on the left of a number does not effect its value. For instance, in a decimal based system, 00010 = 10. However, in other number system types, think the column headers in Excel or stadium seating rows, the zero symbol on the left does effect the number. For instance A != AA.

We can break this down by analyzing a given string in a language based on this zero-collapse constraint. In a zero collapse number system with a radix of r<sub>n</sub>, the rightmost digit has _n_ possible values, but subsequent columns only have _r<sub>n</sub>-1_ possible values.

#### At a given string length:

For non-zero collapse we get the following for a binary alphabet:  
1-digit = 2<sup>1</sup> = 2  
2-digit = 2<sup>2</sup> = 4  
3-digit = 2<sup>3</sup> = 8

For a zero collapse binary alphabet system:  
1-digit = 2<sup>1</sup> = 2  
2-digit = (2-1)<sup>1</sup> * 2<sup>1</sup> = 2  
3-digit = (2-1)<sup>1</sup> * 2<sup>2</sup> = 4  
4-digit = (2-1)<sup>1</sup> * 2<sup>3</sup> = 8  

For a no-collapse trinary alphabet:
1-digit = 3<sup>1</sup> = 3
2-digit = 3<sup>2</sup> = 9
3-digit = 3<sup>3</sup> = 27

For a zero collapse trinary alphabet system:
1-digit = 3<sup>1</sup> = 3
2-digit = (3-1)<sup>1</sup> * 3<sup>1</sup> = 6
3-digit = (3-1)<sup>1</sup> * 3<sup>2</sup> = 18
4-digit = (3-1)<sup>1</sup> * 3<sup>3</sup> = 54

#### General Rules of Thumb

##### Number of unique symbols at a given length:
Given an alphabet with radix r<sub>n</sub> with length _x_:

###### For zero-collapse, typical number systems: 

For length _x_ = 1: r<sub>n</sub>

For length _x_ > 1: (r<sub>n</sub>-1) * r<sub>n</sub><sup>_x_-1</sup>

###### For no-collapse systems (AA-style):

For any length _x_: r<sub>n</sub>


##### Total Number of unique symbols at a given length and lower:
Given an alphabet with radix r<sub>n</sub> with length _x_:

###### For zero-collapse, typical number systems: 

For any length _x_: r<sub>n</sub>

###### For no-collapse systems (AA-style):

For any length _x_: Sum(r<sub>n</sub><sup>q</sup>) as q from 1..x

### 




