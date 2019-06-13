mergeInto(LibraryManager.library, {

    connectionsPool: [],

    WSConnect: function () {
        conn = new WebSocket('ws://localhost:8080');
        connectionsPool.push(conn);
    },

    WSSend: function () {
        window.alert("Hello, world!");
    },


});