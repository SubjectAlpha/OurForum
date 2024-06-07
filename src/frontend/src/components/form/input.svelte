<script lang="ts">
    export let id: string;
    export let type: string;
    export let value: string;
    export let validation: RegExp;
    export let labelText: string;
    export let placeholder: string;
    export let errorText: string;

    const typeWorkaround: any = (node: any) => node.type = type

    let isValid = false;
    $: isValid = validation.test(value);
</script>

<label class="block text-gray-700 dark:text-white text-sm font-bold mb-2" for={id}>
    {labelText}
</label>
<input
    id={id}
    class:border-red-500={!isValid}
    class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
    placeholder={placeholder}
    use:typeWorkaround
    bind:value={value}
/>
<p class:hidden={isValid} class="text-red-500 text-xs italic hidden">
    {errorText}
</p>