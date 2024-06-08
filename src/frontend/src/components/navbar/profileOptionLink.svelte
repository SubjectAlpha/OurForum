<script lang="ts">
    export let id: string;
    export let href: string;
    export let text: string;
</script>

<a
    href={href}
    class="block px-4 py-2 text-sm text-gray-700 rounded-md hover:bg-gray-200"
    role="menuitem"
    tabindex="-1"
    id={id}>{text}
</a>
