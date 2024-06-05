export default null;

export async function post(url: string, body: object) {
    return await fetch(url, {
        method: "POST",
        mode: "cors",
        cache: "no-cache",
        referrerPolicy: "same-origin",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(body)
    });
}

export async function get(url: string) {
    return await fetch(url, { method: "GET" });
}