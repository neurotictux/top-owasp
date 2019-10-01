function Socket(url, name, userId, type) {
    var connection = null;
    var user = { nickname: name, id: userId, type: type };

    this.connect = function (callback) {
        connection = io.connect(url);
        connection.on('connect', function () {
            if (callback)
                callback();
            connection.emit('subscribe', user);
        });
    }

    this.on = function (name, func) {
        connection.on(name, func);
    }

    this.sendMessage = function (message, destiny) {
        var data = {
            message: message,
            description: message,
            type: 'T',
            destiny: destiny
        };
        connection.emit('sendMessage', data);
    }
}