// Demostration of the javascript Map object.
// For more details, visit https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Map
let numbers = new Map(); // Create an new empty Map.

// Assign new kv pairs
numbers.set(1, 'one'); // Add a new value in the map.
numbers.set(2, 'two').set(3, 'three'); // Add two additional kv pairs using chaining.

// Determine the number of kv pairs using size property
console.log(numbers.size); // 3

// Test if a key exists in the Map using the has() method.
console.log(numbers.has(3)); // true
console.log(numbers.has(5)); // false

// get the element in the map associated with the provided key.
console.log(numbers.get('one')); // undefined
console.log(numbers.get(1)); // "one"

// get the keys for the elements in the Map
const iterKeys = numbers.keys(); // An iterator containing the keys of each element in the Map
console.log(iterKeys.next().value); // 1

// get the values for the lements in the Map
const iterValues = numbers.values();
console.log(iterValues.next().value); // "one"

// update a map element.
numbers.set(1, 1); // Update the value for the key 1

console.log(numbers.get(1)); // 1

// Iterate the kv pairs from the Map
const iterEntries = numbers.entries();
console.log(iterEntries.next().value); // Array [1, 1]

// Remove elements from the Map
console.log(numbers.delete('five')); // false
console.log(numbers.delete(3)); // true

console.log(numbers.size); // 2

numbers.clear(); // empty the map

console.log(numbers.size); // 0
