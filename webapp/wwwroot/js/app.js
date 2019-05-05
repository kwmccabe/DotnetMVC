/*
 @see https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-6
*/

var ViewModel = function () {
    var self = this;
    self.items = ko.observableArray();
    self.error = ko.observable();

    var itemsUri = '/api/items/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllItems() {
        ajaxHelper(itemsUri, 'GET').done(function (data) {
            self.items(data);
        });
    }

    // Fetch the initial data.
    getAllItems();
};

ko.applyBindings(new ViewModel());
