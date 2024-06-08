<script lang="ts">
	import type { Booleanish } from "svelte/elements";

    export let href: string;
    export let text: string;
    export let isSelected: Booleanish;
</script>

<a
    href={href}
    class="
        text-gray-300 hover:bg-gray-700 hover:text-white
        rounded-md px-3 py-2 text-sm font-medium"
    aria-current={isSelected}
    >{text}
</a>
