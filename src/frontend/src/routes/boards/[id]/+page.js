import request from '$lib/request';

/** @type {import('./$types').PageLoad} */
export function load({params}) {
    let ret = {
        id: params.id,
        html: ""
    }
    return ret
}