import { writable } from "svelte/store";
import { PUBLIC_BACKEND_URL } from "$env/static/public";
import { post } from "$lib/request";

export const token = writable("");

export const login = async (email: string, password: string) => {
    post(`${PUBLIC_BACKEND_URL}/user/authenticate`, {
        "email": email,
        "password": password
    }).then(async r => {
        if(r.status === 200){
            token.set(await r.text());
        } else {
            console.log(r.status);
        }
    });
}
