<script>
	/** @type {{ data: import('./$types').PageData }} */
	let { data } = $props();
    console.log(data)
</script>

<h1>{data.id}</h1>
<div>{@html data.html}</div>