const uri = "http://localhost:5102";
const userId = document.getElementById('userId').value;
let tableCountries = document.getElementById('tableCountries');
let tableHobbies = document.getElementById('tableHobbies');

//COUNTRIES SECTION

function addCountry() {
    const inputName = document.getElementById('name');

    const country = {
        UserId: userId,
        Name: inputName.value.trim()
    };

    const url = uri + "/api/countries";

    fetch(url, {
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

    let tbodyRef = tableCountries.getElementsByTagName('tbody')[0];

    let newRow = tbodyRef.insertRow();

    let nameCell = newRow.insertCell();
    let userCell = newRow.insertCell();

    let name = document.createTextNode(country.name);
    nameCell.appendChild(name);

    let inputDelete = document.createElement("input");
    inputDelete.value = "Delete";
    inputDelete.type = "button";
    inputDelete.className = "btn btn-danger";
    inputDelete.onclick = () => deleteCountry(country.id, inputDelete);
    
    userCell.appendChild(inputDelete);
}

function deleteCountry(id, row) {

    const url = uri + "/api/countries/" + id;
    
    fetch(url, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
    .then(() => deleteCountryInTable(row))
    .catch(error => console.error('Unable to delete item.', error));

}

function deleteCountryInTable(row) {

    let rowIndex = row.parentNode.parentNode.rowIndex;

    tableCountries.deleteRow(rowIndex);
}

//HOBBIES SECTION

function addHobbie() {

    const hobbieId = document.getElementById('hobbies').value;

    const url = uri + "/api/users/" + userId + "/hobbies/" + hobbieId;

    fetch(url, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
    .then(response => {
        if (!response.ok) {
            throw new Error("Error adding hobbie");
        } else {
            return response.json();
        }
    })
    .then((hobbie) => addHobbieInTable(hobbie))
    .catch(error => console.error('Unable to add item.', error));

}

function addHobbieInTable(hobbie) {

    let tbodyRef = tableHobbies.getElementsByTagName('tbody')[0];

    let newRow = tbodyRef.insertRow();

    let nameCell = newRow.insertCell();
    let userCell = newRow.insertCell();

    let name = document.createTextNode(hobbie.name);
    nameCell.appendChild(name);

    let inputDelete = document.createElement("input");
    inputDelete.value = "Delete";
    inputDelete.type = "button";
    inputDelete.className = "btn btn-danger";
    inputDelete.onclick = () => deleteHobbie(userId, hobbie.id, inputDelete);

    userCell.appendChild(inputDelete);
}
 
function deleteHobbie(idUser, idHobbie, row) {

    const url = uri + "/api/users/" + idUser + "/hobbies/" + idHobbie;

    fetch(url, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
        .then(() => deleteHobbieInTable(row))
        .catch(error => console.error('Unable to delete item.', error));

}

function deleteHobbieInTable(row) {

    let rowIndex = row.parentNode.parentNode.rowIndex;

    tableHobbies.deleteRow(rowIndex);
}