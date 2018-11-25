SystemJS.config({
    baseURL: "/Content/js",
    packages: {
        "/": { defaultExtension: "js" }
    }
});
$(function () {
    $('body').on('click', '.sec', function (e) {
        e.preventDefault();
        console.log(e);
        console.log(this.href);
        $.get(this.href, function (data) {
            $('#delete').html(data);
        });
    });
});
System.import("Notification.js");
System.import("scripts.js");

if (location.protocol + '//' + location.host + '/Sklad/InSklad' === location.href) {
    System.import("Syre.js");
}
if (location.protocol + '//' + location.host + '/Sklad/PackagingView' === location.href) {
    System.import("Pack.js");
}
if (location.protocol + '//' + location.host + '/Sklad/GildView' === location.href) {
    System.import("Gild.js");
}
if (location.protocol + '//' + location.host + '/Sklad/FinishedProduct' === location.href) {
    System.import("FinishedProduct.js");
}
if (location.protocol + '//' + location.host + '/Sklad/Condiments' === location.href) {
    System.import("Condiments.js");
}
if (location.protocol + '//' + location.host + '/Bookkeeping' === location.href || location.protocol + '//' + location.host + '/Bookkeeping/IndexOut' === location.href) {
    System.import("Price.js");
}
if (location.protocol + '//' + location.host + '/Sklad/AddAll' === location.href) {
    System.import("AddAll.js");
}
if (location.protocol + '//' + location.host + '/Claim/NewClaim' === location.href) {
    System.import("NewClaim.js");
}
if (location.protocol + '//' + location.host + '/AutoPark/AutoList' === location.href) {
    System.import("Auto.js");
}
if (location.protocol + '//' + location.host + '/AutoPark/DriverList' === location.href) {
    System.import("Driver.js");
}
if (location.protocol + '//' + location.host + '/Shipment' === location.href) {
    System.import("ShipmentSHIP.js");
    System.import("SendingSHIP.js");
}
if (location.protocol + '//' + location.host + '/Shipment/ArchiveShipment' === location.href) {
    System.import("ArchiveShipment.js");
}