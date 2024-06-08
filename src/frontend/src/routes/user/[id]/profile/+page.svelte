<script lang="ts">
	import { token } from "../../../../hooks/auth";

    $: token;
    if(($token || $token.length == 0) && window)
    {
        console.log(window.location.href);
    }


    $: token;
</script>

<div class="flex m-2 gap-4">
    <div class="basis-2/3 rounded bg-gray-400 h-full p-2">&nbsp;a</div>
    <div class="basis-1/3 rounded bg-gray-500 h-full p-2">&nbsp;a</div>
</div>
