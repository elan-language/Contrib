
## Python library notes

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
