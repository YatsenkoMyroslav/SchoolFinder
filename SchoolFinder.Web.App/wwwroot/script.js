window.getGeolocation = function (dotNetObjRef) {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            function (position) {
                dotNetObjRef.invokeMethodAsync('ReceiveCoordinates', position.coords.latitude, position.coords.longitude);
            })
    } else {
        console.log("Geolocation is not supported by this browser.");
    }
};
