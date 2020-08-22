var activeInfoWindow;

var script = document.createElement("script");
script.src = "https://maps.googleapis.com/maps/api/js?key=AIzaSyA5vd1EQkJH5IxACSkAurle_0G1XqdL9Qk&v=beta&callback=initMap";
script.defer = true;

window.initMap = function () {
    var map = new window.google.maps.Map(document.getElementById("map"), {
        center: { lat: 50.8505, lng: 4.3488 },
        zoom: 14,
        mapId: "dc4f2da3de7d2eda"
    });

    let url = "/ApiProxy/GetMarkers";
    if ($("body").hasClass("authenticated")) {
        url += `?day=${today()}`;
    }

    $.getJSON(url, function (payload) {
        console.log(payload);

        const markers = [];
        $.each(payload, function (i, apiMarker) {
            markers.push(addMarker(apiMarker, map));
        });

        const bounds = new window.google.maps.LatLngBounds();
        for (let i = 0; i < markers.length; i++) {
            bounds.extend(markers[i].getPosition());
        }

        map.fitBounds(bounds);
    });
};

document.head.appendChild(script);


function today() {
    const weekdays = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    return weekdays[new Date().getDay()];
}

function isClosedToday(hours) {
    return hours.openingHour === null || hours.closingHour === null;
}

function addMarker(apiMarker, map) {
    const logo = apiMarker.logo != null ? `<img class="logo" src="${apiMarker.logo}">` : "";
    let openingHour = "-";
    let closingHour = "-";
    if (apiMarker.businessHours.length > 0) {
        const hours = apiMarker.businessHours.filter(x => x.day === today())[0];
        if (!isClosedToday(hours)) {
            openingHour = hours.openingHour.slice(0, -3);
            closingHour = hours.closingHour.slice(0, -3);
        }
    }

    const html = `
        <div class="popup">
            ${logo}
            <div class="content">
                <h1>${apiMarker.name}</h1>
                <p>${apiMarker.description}</p>
            </div>
            <div class="info">
                <p>Type : <strong>${apiMarker.type}</strong></p>
                <p>
                    <strong>${today()}</strong><br>
                    Opening : <strong>${openingHour}</strong><br>
                    Closing : <strong>${closingHour}</strong>
                </p>
                <br>
                <p><strong>${apiMarker.formattedAddress}</strong></p>
                <p><a href="/e/${apiMarker.shortCode}">Details</p>
            </div>
        </div>
    `;

    const infoWindow = new window.google.maps.InfoWindow({
        content: html,
        maxWidth: 500
    });

    const pos = { lat: apiMarker.latitude, lng: apiMarker.longitude };
    const marker = new window.google.maps.Marker({
        position: pos,
        map: map,
        title: apiMarker.name,
        animation: window.google.maps.Animation.DROP
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

    return marker;
}
