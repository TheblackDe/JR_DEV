// Listener para mensajes, podría ser parte de un sistema de eventos
window.addEventListener("message", (event) => {
    if (event.data.action === "someAsyncTask") {
        doSomethingAsync()
            .then(response => {
                event.source.postMessage({ success: true, data: response }, event.origin);
            })
            .catch(error => {
                event.source.postMessage({ success: false, error: error.message }, event.origin);
            });
    }
});

function doSomethingAsync() {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve("Operación completada.");
        }, 2000);
    });
}
