document.addEventListener('DOMContentLoaded', function () {
    // Check if the element exists before adding the event listener
    const cropTrackForm = document.getElementById('crop-track-form');
    if (cropTrackForm) {
        cropTrackForm.addEventListener('click', function () {
            // Hämta värden från formuläret
            const farmName = document.getElementById('farm-name').value;
            const cropName = document.getElementById('crop-name').value;
            const amount = document.getElementById('amount').value;
            const plantDate = document.getElementById('plant-date').value;
            const harvestDate = document.getElementById('harvest-date').value;

            // Kontrollera att alla fält är ifyllda
            if (!farmName || !cropName || !amount || !plantDate || !harvestDate) {
                alert('Please fill out all fields.');
                return;
            }

            // Skapa en ny rad i tabellen
            const table = document.getElementById('farm-data-table');
            const newRow = table.insertRow();

            // Fyll den nya raden med data
            newRow.innerHTML = `
                <td>${farmName}</td>
                <td>${cropName}</td>
                <td>${amount}</td>
                <td>${plantDate}</td>
                <td>${harvestDate}</td>
                <td>Time left (calculate later)</td>
                <td><input type="checkbox" class="tracker-checkbox"></td>
                <td><button class="tracker-remove-btn">✖</button></td>
            `;

            // Lägg till en funktion för att ta bort raden
            newRow.querySelector('.tracker-remove-btn').addEventListener('click', function () {
                newRow.remove();
            });

            // Rensa formuläret efter att uppgifterna har lagts till
            document.getElementById('cropForm').reset();
        });
    }
});

function showRegister() {
    document.getElementById("login-wrapper").style.display = "none";
    document.getElementById("register-wrapper").style.display = "block";
}

function showLogin() {
    document.getElementById("login-wrapper").style.display = "block";
    document.getElementById("register-wrapper").style.display = "none";
}

