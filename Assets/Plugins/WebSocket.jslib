JSWebSocket = {

    $connection : {},

    JSConnect: function () {
        console.log("JSCONNECT EXEC");
        connection = new WebSocket(config.ws_url);

        connection.onmessage = function (message) {
            console.log("MSG RECEIVED, MSG: " + message);
            SendMessage('WebSocket', 'OnMessage', message);
        }
    },

    JSSend: function (message) {

        console.log("JSSEND EXEC, MSG:" + message);
        connection.send(message)
    }

};
autoAddDeps(JSWebSocket, '$connection');
mergeInto(LibraryManager.library,JSWebSocket);