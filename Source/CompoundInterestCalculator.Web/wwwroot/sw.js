const CACHE_NAME = "1.0.5";
const precacheResources = [
    "/_framework/blazor.webassembly.js",
    "/_framework/blazor.boot.json",
    "/_framework/wasm/dotnet.js",
    "/_framework/wasm/dotnet.wasm",
    "/_framework/_bin/CompoundInterestCalculator.Web.dll",
    "/_framework/_bin/Microsoft.AspNetCore.Blazor.dll",
    "/_framework/_bin/Microsoft.AspNetCore.Components.Web.dll",
    "/_framework/_bin/Microsoft.AspNetCore.Components.dll",
    "/_framework/_bin/Microsoft.Bcl.AsyncInterfaces.dll",
    "/_framework/_bin/Microsoft.Extensions.Configuration.Abstractions.dll",
    "/_framework/_bin/Microsoft.Extensions.Configuration.dll",
    "/_framework/_bin/Microsoft.Extensions.DependencyInjection.Abstractions.dll",
    "/_framework/_bin/Microsoft.Extensions.DependencyInjection.dll",
    "/_framework/_bin/Microsoft.Extensions.Logging.Abstractions.dll",
    "/_framework/_bin/Microsoft.Extensions.Primitives.dll",
    "/_framework/_bin/Microsoft.JSInterop.dll",
    "/_framework/_bin/Mono.WebAssembly.Interop.dll",
    "/_framework/_bin/System.Core.dll",
    "/_framework/_bin/System.Net.Http.dll",
    "/_framework/_bin/System.Runtime.CompilerServices.Unsafe.dll",
    "/_framework/_bin/System.Text.Encodings.Web.dll",
    "/_framework/_bin/System.Text.Json.dll",
    "/_framework/_bin/System.dll",
    "/_framework/_bin/WebAssembly.Bindings.dll",
    "/_framework/_bin/WebAssembly.Net.Http.dll",
    "/_framework/_bin/mscorlib.dll",
    "/css/style.min.css",
    "https://fonts.googleapis.com/css?family=PT+Sans&display=swap",
    "/manifest.json",
    "/robots.txt",
    "/favicon.png",
    "/images/icons/app-icon-48x48.png",
    "/images/icons/app-icon-96x96.png",
    "/images/icons/app-icon-144x144.png",
    "/images/icons/app-icon-192x192.png",
    "/images/icons/app-icon-256x256.png",
    "/images/icons/app-icon-384x384.png",
    "/images/icons/app-icon-512x512.png",
    "/"
];

self.addEventListener("install", event => {
    event.waitUntil(
        caches.open(CACHE_NAME).then(cache => {
            cache.addAll(precacheResources);
        })
    );
});

self.addEventListener("activate", event => {
    // Remove old caches
    event.waitUntil(
        caches
            .keys()
            .then(keyList => {
                return Promise.all(
                    keyList.map(key => {
                        if (key !== CACHE_NAME) {
                            return caches.delete(key);
                        }
                    })
                );
            })
    );
    return self.clients.claim();
});

self.addEventListener("fetch", event => {
    event.respondWith(
        caches.match(event.request).then(cachedResponse => {
            return cachedResponse || fetch(event.request);
        })
    );
});
