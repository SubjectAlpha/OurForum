<script lang="ts">
	import { token } from "../../../hooks/auth";
    export let data: string;

    console.log(data);

    $: token;
</script>

<div class="flex m-2 gap-4">
    <div class="basis-2/3 rounded bg-gray-400 h-full p-2">&nbsp;a</div>
    <div class="basis-1/3 rounded bg-gray-500 h-full p-2">&nbsp;a</div>
</div>
