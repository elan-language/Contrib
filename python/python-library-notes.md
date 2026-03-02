
## Python library notes

The older notes at the end of this dcument.

### Table of pure standalone functions, as a start.

The functions are in the  order they appear in `src/compiler/standard-library/std-lib.ts`.

| Elan | Python | Note |
|------|--------|------|
| unicode(n) | chr(n) |    |
| sequence(start, end, step) | range(start, end + 1, step) | (1) (7) |
| divAsInt(n1, n2) | math.floor(n1/n2) | (2) (7) |
| divAsFloat(n1, n2) | (n1/n2) | (7) |
| power(n1, n2) | pow(n1, n2) | (3) |
| maxFloat(source) | max(source) |    |
| maxInt(source) | max(source) |    |
| minFloat(source) | min(source) |    |
| minInt(source) | min(source) |    |
| parseAsFloat(s) | elan.parseAsFloat(s) |    |
| parseAsInt(s) | elan.parseAsInt(s) |    |
| createList(x, value) | [value for n in range(x)] |    |
| abs(x) | abs(x) |    |
| acos(x) | math.acos(x) |    |
| acosDeg(n) | math.degrees(math.acos(n)) |    |
| asin(x) | math.asin(x) |    |
| asinDeg(n) | math.degrees(math.asin(n)) |    |
| atan(x) | math.atan(x) |    |
| atanDeg(n) | math.degrees(math.atan(n)) |    |
| cos(x) | math.cos(x) |    |
| cosDeg(n) | math.cos(math.radians(n)) |    |
| exp(x) | math.exp(x) |    |
| logE(x) | math.log(x) |    |
| log10(x) | math.log10(x) |    |
| log2(x) | math.log2(x) |    |
| sin(x) | math.sin(x) |    |
| sinDeg(n) | math.sin(math.radians(n)) |    |
| sqrt(x) | math.sqrt(x) |    |
| tan(x) | math.tan(x) |    |
| tanDeg(n) | math.tan(math.radians(n)) |    |
| degToRad(d) | math.radians(d) |    |
| radToDeg(r) | math.degrees(r) |    |
| bitAnd(a, b) | (a & b) | (7) |
| bitOr(a, b) | (a \| b) | (7) |
| bitXor(a, b) | (a ^ b) | (7)  |
| bitNot(a) | ~a | (7) |
| bitShiftL(value, shift) | (value << shift) |  (7)   |
| bitShiftR(value, places) | (value & (1<<32)-1) >> places | (4)(7) |
| createFileForWriting(fileName) | TBD |    |
| createBlockGraphics(colour) | TBD |    |
| shallowCopy(toCopy) | list(toCopy) or dict(toCopy) | (6) |

#### Notes

(1) sequence: or list(range(start, end + 1, step)) may be necessary if the simpler version doesn't work
in some circumstances.

(2) int() truncates towards zero

(3) math.pow() is subtly different from global pow()

(4) Elan does bitShiftR unsigned, on 32-bit integers.

(5) [note deleted].

(6) It may be possible to emit the correct code depending on the type of the object being copied.

(7) For simplicity I have written eg "a/b".  If a or b are expressions
rather than literals or variables, the generated code should be eg "(a)/(b)".

----

### Notes from 26th February

We have to decide whether the Python version should be:
- (a) Python syntax and Elan libraries, or
- (b) Python syntax and Python libraries, as far as we can.

To help decide, here are some examples of how it would look.
The library functions below are in the order found in `src/compiler/standard-library/std-lib.ts`
which groups similar functions better than having them alphabetic.
It is not sensible to attempt to add methods to the built-in classes in Python
(see https://stackoverflow.com/questions/4698493/can-i-add-custom-methods-attributes-to-built-in-python-types)

```
Elan: set a to true 
a: a = elan.true 
b: a = True 

Elan: set a to pi 
a: a = elan.pi 
b: a = math.pi 

Elan: set a to unicode(n) 
a: a = elan.unicode(n) 
b: a = chr(n) 

Elan: set a to asUnicode(s) 
a: a = elan.asUnicode(s) 
b: a = ord(s) 

Elan: set a to s.indexOf(c) 
  if a is -1 then 
    set done to true
  else 
    ... 
  end if 
a: a = elan.indexOf(s, c) 
  if a == -1: 
    done = elan.true 
  else: 
    ... 
b: try: 
    a = s.index(c) 
  except ValueError: 
    done = True 
  else: 
    ... 

Elan: set a to s.replace('b', 'B') 
a: a = s.replace('b', 'B') 
b: a = s.replace('b', 'B') 

Elan: set a to l.length() 
a: a = elan.length(l) 
b: a = len(l) 

Elan: set a to s.upperCase() 
a: a = elan.upperCase(s) 
b: a = s.upper() 

Elan: if a.isBefore(c): 
a: if elan.isBefore(a, c): 
b: if a < c: 

Elan: set a to s.trim() 
a: a = elan.trim(s) 
b: a = s.strip() 

Elan: set a to s.split('w') 
a: a = elan.split(s, 'w') 
b: a = s.split('w') 

Elan: set a to s.split('') 
a: a = elan.split(s, '') 
b: a = list(s) 
```

and so on.

The mathematical functions like acos() etc mostly map directly onto the Python equivalents.
The ones with "Deg" suffix are special.

```
Elan: set a to acosDeg(n) 
a: a = elan.acosDeg(n) 
b: a = math.degrees(math.acos(n)) 

Elan: set a to cosDeg(n) 
a: a = elan.cosDeg(n) 
b: a = math.cos(math.radians(n)) 
```

We need to decide how to handle strings with interpolated fields.
Python has several historical ways of turning data values into strings,
and some of the naming can be confusing for beginners, eg the built-in function
`format()` and the string method `str.format()` are different things.
The best match for Elan string formatting is f-strings
(https://docs.python.org/3/library/stdtypes.html#stdtypes-fstrings).

Python f-strings recognise more sophisticated formatting introduced by punctuation
but this is unlikely to get in the way, as Elan doesn't allow `=` `!` or `:` in
the fields between the braces.

```
Elan: call print("z = {z}")
a: print("z = {z}")
a: print("z = {z}")
```

I haven't touched on how we handle lists.  It needs more thought.

```
Elan: set a to l.withAppend(5) 
a: a = elan.withAppend(l, 5) 
b: a = l + [5] 
```
