var currentobject = null;
// Function to store a value in localStorage
function storeValue(value) {
    var val = value.cellElement.find("input");
    value.isEditing = true;
    console.log(val.val(457))
    currentobject = { colone: value.column, valeur: value.value }
    localStorage.setItem('valuecopied', JSON.stringify(currentobject));
}

// Function to retrieve the stored value from localStorage
function getStoredValue() {
    var storedValue = localStorage.getItem('valuecopied');
    try {
        return JSON.parse(storedValue);
    }
    catch(e) {
        console.log(e)
        return storedValue;
    }
    
}

function clickjs(e){
    console.log(e);
    if (currentobject) {
        var before = e.value;
        e.value = 1542
        //console.log(before, e.value);
    }
    storeValue(e);
}

/*
// Example usage
storeValue('Hello, World!'); // Store a value
var retrievedValue = getStoredValue(); // Retrieve the stored value
console.log(retrievedValue); // Output: Hello, World!
*/