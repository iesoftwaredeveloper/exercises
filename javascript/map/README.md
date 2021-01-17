# Javascript Map object

The Map object in Javascript can be though of similar to a dictionary.  It is a collection of key-value pairs.  A feature is that the Map object retains the insertion order of the keys.  Additionally, any value can be used as a key or value.  This includes objects as well as primitive values.

## Creating a Map

Creating a map is simple.

```javascript
let lMap = new Map(); // Create an empty new map and assign it to a block scoped variable.
var vMap = new Map(); // Create an empty new map and assign it to a function scoped variable.
let lMap = new Map([[1, 'first'], [2,'second'],[3,'third']]); // Create a new map and initialize it.
```

## Store (set) data in the Map

To store a value in the map use the set() method.  The set() method adds or updates a key-value pair in the Map object.

```javascript
let numbers = new Map(); // Create a new map

numbers.set(1, 'one'); // Add an new element to the Map
numbers.set(1, 1); // Update the value of an existing element.
```

## Return (get) an element from the Map

Once you have created and stored values in a Map you will most likely want to be able to get those values from the Map.  This can be accomplished using the get() method.

```javascript
numbers.get(1); // "one"
```

## Test if a map contains an element associated with a key

Sometimes you just need to kmow if Map contains an element.  The has() method will return a boolean indicating if an element associated with a key is in the Map.

```javascript
let numbers = new Map([[1, 'one'], [2, 'two'], [3, 'three']]); // Initialize a new map.

numbers.has(1); // true
numbers.has('one'); // false.  'one' is a value, not a key
```

## Map size

If you want to know the current number of elements in a Map you can use the size property.

```javascript
let numbers = new Map([[1, 'one'], [2, 'two'], [3, 'three']]); // Initialize a new map.

numbers.has(1); // true
numbers.has('one'); // false.  'one' is a value, not a key

console.log(numbers.size); // 3
```

## Remove an element

Removing an element from a Map can be done using the delete() method.  This method will eliminate the element associated with a key.

```javascript
let numbers = new Map([[1, 'one'], [2, 'two'], [3, 'three']]); // Initialize a new map.

console.log(numbers.size); // 3

numbers.delete(1);

console.log(numbers.size); // 2
```

## The element keys

If you need to know what keys the elements in the Map have you can use the keys() method.  This will return an iterator object that can be used to obtain the keys.

```javascript
let map1 = new Map();
map1.set(1, 'first').set(2, 'second').set(3, 'third');

const iter = map1.keys();

let element = iter.next();

while(!element.done) {
    console.log(element.value);
    iter.next();
}
```

## The element values

If you are not concerned with what the keys of the elements are and only want to know the values you can use the values() method.  This will return an iterator object that can be used to iterate over the values.

```javascript
let map1 = new Map();
map1.set(1, 'first').set(2, 'second').set(3, 'third');

const iter = map1.values();

let element = iter.next();

while(!element.done) {
    console.log(element.value);
    iter.next();
}
```

## Map key value pairs

If you want to be able to iterate over each element in the Map you can use the entries() method.  This will return they key value pairs.

```javascript
let map1 = new Map();
map1.set(1, 'first').set(2, 'second').set(3, 'third');

const iter = map1.entries();

let element = iter.next();

while(!element.done) {
    console.log(element.value);
    iter.next();
}
```

## Map forEach

It is common that you will want to iterate for each element in a Map and do something.  You can do this by iterating for the keys of the Map and then executing a function for each key-value pair.  The forEach() method is an easier way to do this. The forEach() method will iterate over each element in a Map and execute a function for each key/value pair in the Map object.

```javascript
function callback(value, key, map) {
    console.log('Key: [' + key + '] = [' + value + ']');
};
let map1 = new Map();
map1.set(1, 'first').set(2, 'second').set(3, 'third');

map1.forEach(callback);
```

## Remove all Map elements

When you need to remove all elements from a Map you can use the clear() method.  This will remove all of the elements in a Map object.

```javascript
let map1 = new Map();
map1.set(1, 'first').set(2, 'second').set(3, 'third');

console.log(map1.size); // 3

map1.clear();

console.log(map1.size); // 0
```
