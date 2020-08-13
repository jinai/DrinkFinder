var activeInfoWindow;

var script = document.createElement("script");
script.src = "https://maps.googleapis.com/maps/api/js?key=AIzaSyA5vd1EQkJH5IxACSkAurle_0G1XqdL9Qk&v=beta&callback=initMap";
script.defer = true;

window.initMap = function () {
    var map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: 50.8505, lng: 4.3488 },
        zoom: 14,
        mapId: "dc4f2da3de7d2eda"
    });

    let url = "/ApiProxy/GetMarkers";
    if ($("body").hasClass("authenticated")) {
        url += "?day=" + today()
    }

    $.getJSON(url, function (payload) {
        console.log(payload);
        $.each(payload, function (i, apiMarker) {
            addMarker(apiMarker, map);
        });
    });
};

document.head.appendChild(script);


function today() {
    let weekdays = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    return weekdays[new Date().getDay()];
}

function hoursNull(hours) {
    return hours.openingHour == "00:00:00" && hours.closingHour == "00:00:00";
}

function addMarker(apiMarker, map) {
    let logo = apiMarker.logo != null ? `<img class="logo" src="${apiMarker.logo}">` : ""
    let openingHour = "-";
    let closingHour = "-";
    if (apiMarker.businessHours.length > 0) {
        let hours = apiMarker.businessHours.filter(x => x.day === today())[0];
        if (!hoursNull(hours)) {
            openingHour = hours.openingHour.slice(0, -3);
            closingHour = hours.closingHour.slice(0, -3);
        }
    }

    let html = `
        <div class="popup">
            ${logo}
            <div class="content">
                <h1>${apiMarker.name}</h1>
                <p>${apiMarker.description}</p>
            </div>
            <div class="info">
                <p>Type : <strong>${apiMarker.type}</strong></p>
                <p>Ouverture : <strong>${openingHour}</strong><br>Fermeture : <strong>${closingHour}</strong></p>
                <br>
                <p><strong>${apiMarker.formattedAddress}</strong></p>
            </div>
        </div>
    `

    let infoWindow = new google.maps.InfoWindow({
        content: html,
        maxWidth: 500
    });

    let pos = { lat: apiMarker.latitude, lng: apiMarker.longitude };
    let marker = new google.maps.Marker({
        position: pos,
        map: map,
        title: apiMarker.name,
        animation: google.maps.Animation.DROP
    });

    marker.addListener("click", function () {
        if (activeInfoWindow) {
            activeInfoWindow.close();
        }
        map.panTo(marker.position);
        infoWindow.open(map, marker);
        activeInfoWindow = infoWindow;
    });
    map.addListener("click", function () {
        if (activeInfoWindow) {
            activeInfoWindow.close();
        }
    });
}
