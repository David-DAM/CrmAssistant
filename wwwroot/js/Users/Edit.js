const uri = "http://localhost:5102/api/countries";
function addCountry() {
    const inputName = document.getElementById('name');
    const userId = document.getElementById('userId');

    const country = {
        UserId: parseInt(userId.value),
        Name: inputName.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(country)
    })
    .then(response => response.json())
    .then(data => addCountryInTable(data))
    .catch(error => console.error('Unable to add item.', error));
}

function addCountryInTable(country) {

    let tbodyRef = document.getElementById('tableCountries').getElementsByTagName('tbody')[0];

    let newRow = tbodyRef.insertRow();

    let nameCell = newRow.insertCell();
    let userCell = newRow.insertCell();

    let name = document.createTextNode(country.name);
    nameCell.appendChild(name);

    let inputDelete = document.createElement("input");
    inputDelete.value = "Delete";
    inputDelete.type = "button";
    inputDelete.className = "btn btn-danger";
    inputDelete.onclick = () => deleteCountry(country.id);
    
    userCell.appendChild(inputDelete);
}

function deleteCountry(id) {
    alert("ficnona"+id);
}