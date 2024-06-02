export default null;

export async function post(url: string, body: object) {
    return await fetch(url, {
        method: "POST",
        mode: "no-cors",
        cache: "no-cache",
        referrerPolicy: "no-referrer",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(body)
    });
}

export async function get(url: string) {
    return fetch(url, { method: "GET" });
}