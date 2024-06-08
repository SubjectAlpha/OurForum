import { browser } from "$app/environment";
import { PUBLIC_BACKEND_URL } from "$env/static/public";
import { TOKEN_NAME } from "$lib/constants";
import { post } from "$lib/request";
import { writable } from "svelte/store";

export const token = writable(browser ? localStorage.getItem(TOKEN_NAME) || "" : "")

token.subscribe(tkn => browser ?? localStorage.setItem(TOKEN_NAME, tkn));

export const login = async (email: string, password: string) => {
    const response = await post(`${PUBLIC_BACKEND_URL}/user/authenticate`, {
        "email": email,
        "password": password
    });

    if(response.status === 200){
        const foundToken = await response.text();
        token.set(foundToken);
        return foundToken;
    } else {
        console.log(response.status);
        return null;
    }
}
