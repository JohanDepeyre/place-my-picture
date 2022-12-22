
function VisibleOrNotVisible() {

    let div = document.querySelector(".formHide");
    if (div.style.display == 'flex') {
        div.style.display = 'none';
    }
    else {

        div.style.display = 'flex';
    }
}

function GenererCarte(json1) {


                    let obj = json1[0];
                    let latFirst = obj.Latitude;
                    let lonFirst = obj.Longitude;
                    //  create map object, tell it to live in 'map' div and give initial latitude, longitude, zoom values
                    var map = L.map('map', { scrollWheelZoom: true }).setView([latFirst, lonFirst], 8);

                    //  add base map tiles from OpenStreetMap and attribution info to 'map' div
                    L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
                        attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                    }).addTo(map);


                    for (let i = 0; i < json1.length; i++) {
                        let obj = json1[i];
                        let urlIcon = "";
                        let idCategorie = obj.CategorieId;
                        switch (idCategorie) {
                            case 1:
                                urlIcon = 'img/icons8-champ-48.png';
                                break;

                            case 2:
                                urlIcon = 'img/icons8-we-48.png';
                                break;
                            case 3:
                                urlIcon = 'img/icons8-sports-48.png';
                                break;
                            case 4:
                                urlIcon = 'img/icons8-selfie-48.png';
                                break;
                            case 5:
                                urlIcon = 'img/icons8-renard-48.png';
                                break;
                            case 6:
                                urlIcon = 'img/icons8-bâtiments-de-la-ville-48.png';
                                break;
                            case 7:
                                urlIcon = 'img/icons8-trail-48.png';
                                break;
                            default:
                                urlIcon = 'img/icons8-champ-48.png'
                        }


                        var imageIcon = L.icon({
                            iconUrl: urlIcon,
                            iconSize: [30, 30], // size of the icon
                            popupAnchor: [0, -15]
                        });

                        let imageLink = obj.ImageUrl;
                        // create popup contents
                        var customPopup = obj.Description + "<br/><img src='" + imageLink + " ' alt='maptime logo gif' width='350px'/>";
                        // specify popup options
                        var customOptions =
                        {
                            'maxWidth': '500',
                            'className': 'custom'
                        }

                        let lat = obj.Latitude;
                        let lon = obj.Longitude;
                        // create marker object, pass custom icon as option, pass content and options to popup, add to map
                        L.marker([lat, lon], { icon: imageIcon }).bindPopup(customPopup, customOptions).addTo(map);

                        console.log(obj.ImageUrl);
                    }

}