window.eventEase = {
    saveState: function (json) {
        localStorage.setItem('eventEaseState', json);
    },
    loadState: function () {
        return localStorage.getItem('eventEaseState');
    }
};
