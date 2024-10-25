document.addEventListener('DOMContentLoaded', function () {
    // Hämta crops och fyll dropdown-menyn
    fetch('/CropManagement/Index')
        .then(response => response.json())
        .then(data => {
            const dropdown = document.getElementById("crop-name");
            data.forEach(crop => {
                let option = document.createElement("option");
                option.value = crop.cropId;
                option.text = crop.cropName;
                dropdown.appendChild(option);
            });
        })
        .catch(error => console.error('Error fetching crops:', error));

    // Kontrollera att knappen "Create crop" triggas
    const createCropButton = document.getElementById('create-crop-btn');
    if (createCropButton) {
        createCropButton.addEventListener('click', function () {
            console.log("Create crop button clicked!");

            // Samla in värden från formuläret
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
                        // Här kan du lägga till kod för att visa ett meddelande eller ladda om sidan
                    } else {
                        console.error('Failed to create crop:', response.statusText);
                    }
                })
                .catch(error => console.error('Error creating crop:', error));
        });
    }
});
