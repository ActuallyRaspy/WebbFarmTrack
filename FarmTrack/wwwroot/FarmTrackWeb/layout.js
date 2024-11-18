document.addEventListener('DOMContentLoaded', function () {

    // Kontrollera att knappen "Create crop" triggas
    const createCropButton = document.getElementById('create-crop-btn');
    if (createCropButton) {
        createCropButton.addEventListener('click', function () {
            console.log("Create crop button clicked!");

            // Samla värden från formuläret
            const cropName = document.getElementById('crop-name').value;
            const plantingSeasonWarm = document.getElementById('planting-season-warm').value;
            const plantingSeasonCold = document.getElementById('planting-season-cold').value;
            const harvestingSeasonWarm = document.getElementById('harvesting-season-warm').value;
            const harvestingSeasonCold = document.getElementById('harvesting-season-cold').value;
            const daysToGrow = document.getElementById('days-to-grow').value;
            const description = document.getElementById('description').value;

            // Skapa ett objekt med all data
            const cropData = {
                cropName: cropName,
                plantingSeasonWarm: plantingSeasonWarm,
                plantingSeasonCold: plantingSeasonCold,
                harvestingSeasonWarm: harvestingSeasonWarm,
                harvestingSeasonCold: harvestingSeasonCold,
                daysToGrow: parseInt(daysToGrow),
                cropDescription: description
            };

            console.log("Sending crop data:", JSON.stringify(cropData));
            // Skicka POST-förfrågan till servern för att skapa en crop
            fetch('/CropManagement/Create', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(cropData)
            })
                .then(response => {
                    if (response.ok) {
                        console.log("Crop created successfully!");
                    } else {
                        console.error('Failed to create crop:', response.statusText);
                    }
                })
                .catch(error => console.error('Error creating crop:', error));
        });
    }
});

function hideRegister() {
    console.log("Hide register button clicked!");
    document.getElementById('register-wrapper').style.display = "none";
    document.getElementById('login-wrapper').style.display = "block";
}

function hideLogin() {
    console.log("Hide login button clicked!");
    document.getElementById('login-wrapper').style.display = "none";
    document.getElementById('register-wrapper').style.display = "block";
}