var ViewModel = function () {
    var self = this;
    self.products = ko.observableArray();
    self.shoppingListProducts = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.newProduct = {
        Name: ko.observable(),
        Brand: ko.observable(),
        Description: ko.observable(),
        Image: ko.observable()
    }

    var productsUri = '/api/products/';
    var shoppingListsUri = '/api/shoppinglists/';

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

    //Product functions
    function getAllProducts() {
        ajaxHelper(productsUri, 'GET').done(function (data) {
            self.products(data);
        });
    }

    self.getProductDetail = function (item) {
        ajaxHelper(productsUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    self.addProduct = function() {
        var product = {
            Name: self.newProduct.Name(),
            Brand: self.newProduct.Brand(),
            Description: self.newProduct.Description(),
            Image: self.newProduct.Image()
        };

        ajaxHelper(productsUri, 'POST', product).done(function (item) {
            self.products.push(item);
        });
    }

    // Fetch the initial products data.
    getAllProducts();

    //ShoppingList functions
    function getShoppingList(id)
    {
        ajaxHelper(shoppingListsUri + id, 'GET').done(function (data) {
            self.shoppingListProducts(data.Products);
        });
    }

    //Fetch the initial shoppinglist data
    getShoppingList(1);
};

ko.applyBindings(new ViewModel());