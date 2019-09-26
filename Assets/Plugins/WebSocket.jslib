JSWebSocket = {

    $connection: {},

    JSConnect: function (token, gameid) {
        config = {
            ws_url: "ws://localhost:8080?access_token=" + Pointer_stringify(token) + "&game=" + Pointer_stringify(gameid)
        }
        console.log("JSCONNECT EXEC");
        connection = new WebSocket(config.ws_url);

        connection.onmessage = function (event) {
            console.log("MSG RECEIVED, MSG: " + event.data);
            SendMessage('WebSocket', 'OnMessage', event.data);
        }
    },

    JSSend: function (message) {
        var strMessage = Pointer_stringify(message);
        console.log("JSSEND EXEC, MSG:" + strMessage);
        connection.send(strMessage)
    }

};
autoAddDeps(JSWebSocket, '$connection');
mergeInto(LibraryManager.library, JSWebSocket);